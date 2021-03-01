using System;

[AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
public class RecievesCommandAttribute : Attribute
{
    public RecievesCommandAttribute(CommandCode code)
    {
        CommandCode = code;
    }

    public CommandCode CommandCode { get; }
}

