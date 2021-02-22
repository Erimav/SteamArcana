using MLAPI.Serialization;

public interface IMessageReceiver
{
    void ReceiveMessage(string messageType, byte[] data);
    void ReceiveMessage<T>(T message);
}

