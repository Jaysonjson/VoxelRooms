using Godot;
using System;

public partial interface IConsoleCommand {
    void Execute(DevConsole console, string[] arguments);
    void PrintHelpUsage(DevConsole console, bool advanced);
}