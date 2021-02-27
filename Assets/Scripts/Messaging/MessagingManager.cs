using MLAPI.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "MessagingManager",menuName = "Messaging/Manager")]
public class MessagingManager : ScriptableObject, IMessagingManager
{
    [SerializeField]
    private GameObject networkedMessagingManagerPrefab;
    public bool isMultiplayer;
    [SerializeField, SerializeReference]
    public IMessagingManager networkedManager;

    private Dictionary<MessageCode, IMessageReceiver> receivers = new Dictionary<MessageCode, IMessageReceiver>();

    public void SendMessage<T>(T message)
        where T : IMessageData, new()
    {
        if (isMultiplayer)
        {
            networkedManager.SendMessage(message);
            return;
        }

        var code = message.MessageCode;
        if (!receivers.ContainsKey(code))
        {
            Debug.LogError($"No receiver registered for {code}");
            return;
        }

        receivers[code].ReceiveMessage(message);
    }

    public virtual void OnNetworkMessage(MessageCode messageCode, Stream messageStream)
    {
        if (!receivers.ContainsKey(messageCode))
        {
            Debug.LogError($"No receiver registered for {messageCode}");
            return;
        }

        receivers[messageCode].ReceiveMessage(messageCode, messageStream);
    }

    public void RegisterMessageReceiver(MessageCode code, IMessageReceiver receiver)
    {
        var typeName = code;
        if (receivers.ContainsKey(typeName))
        {
            Debug.LogError($"Attempt to register 2 receivers for {code}, aborting");
            return;
        }

        receivers[typeName] = receiver;
    }
}

