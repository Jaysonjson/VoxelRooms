using Godot;

public partial class ClearCommand : IConsoleCommand {
	
    public void Execute(DevConsole console, string[] arguments) {
        if(arguments.Length == 1) {
            console.LogNode.Clear();
            console.History.Clear();
        } else {
            if(arguments[1] == "log") {
                console.LogNode.Clear();
            } else if(arguments[1] == "history") {
                console.History.Clear();
            }
        }
    }

    public void PrintHelpUsage(DevConsole console, bool advanced) {
        console.PrintConsoleSubInformation("clear - clears console", true);
        console.PrintConsoleSubInformation("clear log - clears the console log", true);
        console.PrintConsoleSubInformation("clear history - clears the command history");
    }


}