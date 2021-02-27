﻿using MLAPI.Serialization;

public interface IMessagingManager
{
    void SendMessage<T>(T message) where T : IMessageData, new();
}