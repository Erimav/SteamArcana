using MLAPI.Serialization;
using System.IO;

public interface IMessageReceiver
{
    void ReceiveMessage(MessageCode messageType, Stream messageStream);
    void ReceiveMessage<T>(T message);
}

