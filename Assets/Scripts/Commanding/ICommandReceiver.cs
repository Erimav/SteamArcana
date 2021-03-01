using System.IO;

public interface ICommandReceiver
{
    void ReceiveMessage(CommandCode messageType, Stream messageStream);
    void ReceiveMessage<T>(T command) where T : ICommand;
}

