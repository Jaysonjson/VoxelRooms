using System;
using System.Collections;
using System.Collections.Generic;

public partial class CommandManager {
    public static Dictionary<string, IConsoleCommand> Commands { get; set; } = new Dictionary<string, IConsoleCommand>();

    public static void RegisterCommands() {
        Commands["help"] = new HelpCommand();
        Commands["list"] = new ListCommand();
        Commands["clear"] = new ClearCommand();
        Commands["node"] = new NodeCommand();
    }
}