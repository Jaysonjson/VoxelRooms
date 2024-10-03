using Godot;
using System;
using System.Collections.Generic;

public partial class DevConsole : Control
{
    public const string INPUT_OPEN_CLOSE_CONSOLE = "dev_console_open_close";
    public const string INPUT_ENTER_COMMAND = "dev_console_enter_command";
    public const string INPUT_HISTORY_UP = "dev_console_history_up";
    public const string INPUT_HISTORY_DOWN = "dev_console_history_down";

    [Export]
    public LineEdit InputNode { get; private set; }
    [Export]
    public MarginContainer ContainerNode { get; private set; }
    [Export]
    public RichTextLabel LogNode { get; private set; }

    public int CurrentHistoryIndex { get; private set; }
    public List<string> History { get; private set; } = new List<string>();

    public override void _Ready() {
        CommandManager.RegisterCommands();
        //ContainerNode = GetChild<MarginContainer>(0);
        //InputNode = ContainerNode.GetChild<LineEdit>(2);
        //LogNode = ContainerNode.GetChild<RichTextLabel>(1);
        //InputNode = GetNode<LineEdit>("DevConsole/CommandInput");
        PrintConsoleLine("Zeitriss Dev Console");
        PrintConsoleInformation("Information Line");
        PrintConsoleSubInformation("Subinformation Line Row 0");
        PrintConsoleSubInformation("Subinformation Line Row 1", true);
        PrintConsoleSuccess("Success Line");
        PrintConsoleWarning("Warning Line");
        PrintConsoleError("Error Line");
    }

    public override void _Process(double delta) {
        if(Input.IsActionJustPressed(INPUT_OPEN_CLOSE_CONSOLE)) OpenOrClose();
        if(Visible) {
            if(Input.IsActionJustPressed(INPUT_ENTER_COMMAND)) ProcessCommand();
            if(Input.IsActionJustPressed(INPUT_HISTORY_UP)) {
                if(History.Count - 1 > CurrentHistoryIndex) {
                    ++CurrentHistoryIndex;
                    InputNode.Text = History[CurrentHistoryIndex];
                }
            } else if(Input.IsActionJustPressed(INPUT_HISTORY_DOWN)) {
                if(CurrentHistoryIndex > 0) {
                    --CurrentHistoryIndex;
                    InputNode.Text = History[CurrentHistoryIndex];
                }
            }
        }

        /* FULLSCREN TOGGLE */
        if(Input.IsActionJustPressed("toggle_fullscreen")) {
            DisplayServer.WindowSetMode(DisplayServer.WindowGetMode() == DisplayServer.WindowMode.Windowed ? DisplayServer.WindowMode.ExclusiveFullscreen : DisplayServer.WindowMode.Windowed);
        }
    }

    public void ProcessCommand() {
        string[] arguments = InputNode.Text.Split(" ");
        if(CommandManager.Commands.ContainsKey(arguments[0].ToLower())) {
            History.Add(InputNode.Text);
            PrintConsoleLine(InputNode.Text, false);
            ++CurrentHistoryIndex;
            CommandManager.Commands[arguments[0].ToLower()].Execute(this, arguments);
            InputNode.Clear();
        } else {
            PrintConsoleError("Unknown Command");
        }
    }

    public void OpenOrClose(bool show) {
        Visible = show;
        foreach (Node item in GetChildren()) {
            if (item is CanvasItem canvasItem) {
                canvasItem.Visible = Visible;
            }
        }
    }

    public void OpenOrClose() {
        OpenOrClose(!Visible);
    }

    public void PrintConsoleLine(string line, bool forward = true) {
        if(forward) LogNode.AppendText(" ");
        LogNode.AppendText(line + "\n");
        /*
        * Never showed the last message when scroll was active, so this fixed this
        */
        if(LogNode.ScrollActive) {
            LogNode.AppendText(" ");
        }
        LogNode.ScrollToLine(LogNode.GetLineCount());
    }
    public void PrintConsoleLine(string line, Color textColor) {
        LogNode.PushColor(textColor);
        PrintConsoleLine(line);
        LogNode.Pop();
    }

    public void PrintConsoleError(string line) {
        PrintConsoleLine(line, new Color(0xff5555ff));
    }

    public void PrintConsoleSuccess(string line) {
        PrintConsoleLine(line, new Color(0x55ff55ff));
    }

    public void PrintConsoleInformation(string line) {
        PrintConsoleLine(line, new Color(0xb0d5d6ff));
    }


    public void PrintConsoleSubInformation(string line, bool rowChange = false) {
        PrintConsoleLine(line, rowChange ? new Color(0x8ecae6ff) : new Color(0x219ebcff));
    }

    public void PrintConsoleWarning(string line) {
        PrintConsoleLine(line, new Color(0xe8bb66ff));
    }
}
