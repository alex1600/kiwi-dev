using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCMD
{
    private string commandId;
    private string commandDescription;
    private string commandFormat;

    public string Id { get { return commandId; } }
    public string Description { get { return commandDescription; } }
    public string Format { get { return commandFormat; } }

    public BaseCMD(string commandId, string commandDescription, string commandFormat)
    {
        this.commandId = commandId;
        this.commandDescription = commandDescription;
        this.commandFormat = commandFormat;
    }

    public abstract void Invoke(string[] args);
}

public class ConsoleCMD : BaseCMD
{
    Action<string[]> action;
    public ConsoleCMD(string commandId, string commandDescription, string commandFormat, Action<string[]> action) 
        : base(commandId, commandDescription, commandFormat)
    {
        this.action = action;
    }

    public override void Invoke(string[] args)
    {
        action.Invoke(args);
    }
}
