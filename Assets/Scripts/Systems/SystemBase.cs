using System.IO;
using System.Linq;
using UnityEngine;

public abstract class SystemBase : ScriptableObject, ICommandReceiver
{
    public CommandsManager commands;

    public abstract void ReceiveMessage(CommandCode messageType, Stream messageStream);

    public virtual void ReceiveMessage<T>(T command) where T : ICommand
    {
        command.Execute(this);
    }

    protected virtual void OnEnable()
    {
        RegisterForCommands();
    }

    protected virtual void RegisterForCommands()
    {
        var type = GetType();
        var attributes = type.GetCustomAttributes(typeof(RecievesCommandAttribute), true).Cast<RecievesCommandAttribute>();

        foreach (var attr in attributes)
            commands.RegisterCommandReceiver(attr.CommandCode, this);
    }
}

