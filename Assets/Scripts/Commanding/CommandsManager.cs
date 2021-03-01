using MLAPI.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "MessagingManager",menuName = "Messaging/Manager")]
public class CommandsManager : ScriptableObject, ICommandsManager
{
    [SerializeField]
    private GameObject networkedMessagingManagerPrefab;
    public bool isMultiplayer;
    [SerializeField, SerializeReference]
    public ICommandsManager networkedManager;

    private Dictionary<CommandCode, ICommandReceiver> receivers = new Dictionary<CommandCode, ICommandReceiver>();

    public void SendMessage<T>(T message)
        where T : ICommand, new()
    {
        if (isMultiplayer)
        {
            networkedManager.SendMessage(message);
            return;
        }

        var code = message.CommandCode;
        if (!receivers.ContainsKey(code))
        {
            Debug.LogError($"No receiver registered for {code}");
            return;
        }

        receivers[code].ReceiveMessage(message);
    }

    public virtual void OnNetworkMessage(CommandCode messageCode, Stream messageStream)
    {
        if (!receivers.ContainsKey(messageCode))
        {
            Debug.LogError($"No receiver registered for {messageCode}");
            return;
        }

        receivers[messageCode].ReceiveMessage(messageCode, messageStream);
    }

    public void RegisterMessageReceiver(CommandCode code, ICommandReceiver receiver)
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

