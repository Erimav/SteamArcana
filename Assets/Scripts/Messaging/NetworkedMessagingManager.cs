using MLAPI.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using MLAPI.Messaging;
using System.IO;
using MLAPI.Serialization.Pooled;

public class NetworkedMessagingManager : ScriptableObject, IMessagingManager
{
    [SerializeReference]
    public IMessagingManager messagingManager;

    private void Awake()
    {
        CustomMessagingManager.OnUnnamedMessage += OnMessage;
    }

    public void OnMessage(ulong clientId, Stream stream)
    {
        throw new NotImplementedException();
    }

    public void SendMessage<T>(T message) where T : IBitWritable, new()
    {

    }
}

