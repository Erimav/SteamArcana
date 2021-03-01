using MLAPI.Serialization;

public interface ICommandsManager
{
    void SendCommand<T>(T message) where T : ICommand, new();
}