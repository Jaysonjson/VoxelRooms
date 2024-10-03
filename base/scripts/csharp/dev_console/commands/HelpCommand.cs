using System.Linq;

public partial class HelpCommand : IConsoleCommand {
    public void Execute(DevConsole console, string[] arguments) {
        if(arguments.Length == 1) {
            console.LogNode.PushBold();
            console.PrintConsoleInformation("Available Commands:");
            console.LogNode.Pop();
            console.PrintConsoleInformation("help [command_id:string] - Shows help docs of specified command");
        }
        int i = 0;
        foreach(IConsoleCommand command in CommandManager.Commands.Values) {
            if(arguments.Length == 1) {
                //command.PrintHelpUsage(console, false);
                string commandId = CommandManager.Commands.Keys.ElementAt(i);
                if(commandId != "help") console.PrintConsoleSubInformation(CommandManager.Commands.Keys.ElementAt(i), i % 2 == 0);
            } else {
                string sub = arguments[1];
                if(CommandManager.Commands.Keys.ElementAt(i) == sub) {
                    command.PrintHelpUsage(console, true);
                }
            }
            ++i;
        }
    }

    public void PrintHelpUsage(DevConsole console, bool advanced) {
        
    }

}