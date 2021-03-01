using MLAPI.Serialization;

public interface ICommandsManager
{
    void SendMessage<T>(T message) where T : ICommand, new();
}