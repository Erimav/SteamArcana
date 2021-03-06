﻿using MLAPI.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using MLAPI.Messaging;
using System.IO;
using MLAPI.Serialization.Pooled;

public class NetworkedCommandsManager : ScriptableObject, ICommandsManager
{
    [SerializeField]
    public CommandsManager messagingManager;

    private void Awake()
    {
        CustomMessagingManager.OnUnnamedMessage += OnMessage;
    }

    public void OnMessage(ulong clientId, Stream stream)
    {
        try
        {
            using (var reader = PooledBitReader.Get(stream))
            {
                //var bytes = new byte[4];
                //stream.Read(bytes, 4, 0);
                var messageCode = reader.ReadInt32();
                messagingManager.OnNetworkMessage((CommandCode)messageCode, stream);
            }
        }
        catch
        {
            throw;
        }
    }

    public void SendCommand<T>(T message) where T : ICommand, new()
    {
        using (var stream = PooledBitStream.Get())
        {
            using (var writer = PooledBitWriter.Get(stream))
            {
                writer.WriteInt32((int)message.CommandCode);
                message.Write(stream);
            }

            CustomMessagingManager.SendUnnamedMessage(null, stream);
        }
    }
}

