using MLAPI.Serialization.Pooled;
using System;
using System.IO;
using UnityEngine;

public struct PlayerStartMoveMessage : IMessageData
{
    public MessageCode MessageCode => MessageCode.PlayerStartMove;

    public int playerId;
    public Vector2 motion;

    public void Read(Stream stream)
    {
        using (var reader = PooledBitReader.Get(stream))
        {
            playerId = reader.ReadInt32Packed();
            motion = reader.ReadVector2Packed();
        }
    }

    public void Write(Stream stream)
    {
        using (var writer = PooledBitWriter.Get(stream))
        {
            writer.WriteInt32Packed(playerId);
            writer.WriteVector2Packed(motion);
        }
    }
}

