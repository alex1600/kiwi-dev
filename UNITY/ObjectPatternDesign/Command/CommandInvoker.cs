using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Command invoker. Collects command into buffer to execute them at once.
/// </summary>
public class CommandInvoker : MonoBehaviour
{
    // Collected commands.
    private Queue<Command> commandBuffer = new Queue<Command>();

    /// <summary>
    /// Method used to add new command to the buffer.
    /// </summary>
    /// <param name="command">New command.</param>
    public void AddCommand(Command command)
    {
        commandBuffer.Enqueue(command);
    }

    /// <summary>
    /// Method used to execute all commands from the buffer.
    /// </summary>
    public void ExecuteBuffer()
    {
        foreach (var c in commandBuffer)
        {
            c.Execute();
        }

        commandBuffer.Clear();
    }
}
