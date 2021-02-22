using MLAPI.Serialization;
using System;
using System.Collections.Generic;
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
    [SerializeReference]
    public IMessagingManager networkedManager;

    public void SendMessage<T>(T message)
        where T : IBitWritable, new()
    {
        throw new NotImplementedException();
    }

    public void SendMessage(string messageType, byte[] data)
    {
        throw new NotImplementedException();
    }

    public void RegisterMessageReceiver<T>(IMessageReceiver receiver)
        where T : IBitWritable
        => RegisterMessageReceiver(typeof(T), receiver);

    public void RegisterMessageReceiver(Type type, IMessageReceiver receiver)
    {
        
    }
}

