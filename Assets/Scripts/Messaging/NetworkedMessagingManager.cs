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
    [SerializeField]
    public MessagingManager messagingManager;

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
                messagingManager.OnNetworkMessage((MessageCode)messageCode, stream);
            }
        }
        catch
        {
            throw;
        }
    }

    public void SendMessage<T>(T message) where T : IMessageData, new()
    {
        using (var stream = PooledBitStream.Get())
        {
            using (var writer = PooledBitWriter.Get(stream))
            {
                writer.WriteInt32((int)message.MessageCode);
                message.Write(stream);
            }

            CustomMessagingManager.SendUnnamedMessage(null, stream);
        }
    }
}

