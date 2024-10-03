using Godot;

public partial class ListCommand : IConsoleCommand {
    public void Execute(DevConsole console, string[] arguments) {
        if(arguments.Length == 1) {
            console.PrintConsoleError("No Arguments given, use \"help list\" to show usage of the list command");
            return;
        }
        if(arguments[1] == "current_scene") {
            int i = 0;
            if(arguments.Length == 2) {
                console.PrintConsoleInformation("Nodes in Scene \"" + console.GetTree().CurrentScene.Name + "\"");
                foreach(Node node in console.GetTree().CurrentScene.GetChildren()) {
                    console.PrintConsoleSubInformation(node.Name, i % 2 == 0);
                    ++i;
               }
            } else if (arguments.Length == 3) {
                Node node = console.GetTree().CurrentScene.GetNode(arguments[2]);
                if(node == null) {
                    console.PrintConsoleError("Node " + arguments[2] + " not found in Scene");
                    return;
                } 

                console.PrintConsoleInformation("Children in Node \"" + node.Name + "\"");       
                foreach(Node child in node.GetChildren()) {
                    console.PrintConsoleSubInformation(child.Name, i % 2 == 0);
                    ++i;
                }
                console.PrintConsoleInformation("Total Count: " + console.GetTree().CurrentScene.GetChildren().Count);
            }
        } else if(arguments[1] == "scenes") {
            ListSceneDirs(console, "res://base/scenes/game", "Game Scenes");
            ListSceneDirs(console, "res://base/scenes/objects", "Object Scenes");
            console.PrintConsoleInformation("Done");
        }
    }

    public void ListSceneDirs(DevConsole console, string path, string name) {
        console.LogNode.PushBold();
        console.PrintConsoleInformation(name);
        console.LogNode.Pop();
        DirAccess directory = DirAccess.Open(path);
        if(directory != null) {
            directory.ListDirBegin();
            string file = directory.GetNext();
            int i = 0;
            console.LogNode.PushBold();
            console.LogNode.Pop();
            while(file != "") {
                if(!directory.CurrentIsDir()) {
                        console.PrintConsoleSubInformation(path + "/" + file, i % 2 == 0);
                } else {
                    ListSceneDirs(console, path + "/" + file, file);
                }
                file = directory.GetNext();
                ++i;
            }
            directory.ListDirEnd();
        } else {
            console.PrintConsoleError("Could not find " + name + " Folder, internal error");
        }
    }

    public void PrintHelpUsage(DevConsole console, bool advanced) {
        console.PrintConsoleSubInformation("list current_scene - lists all children of the current scene");
        console.PrintConsoleSubInformation("list current_scene [node_name:string]- lists all children of the given nodes name (Node1/Node2/Node3 to go trough a tree)");
        console.PrintConsoleSubInformation("list scenes - lists all Nodes/Scenes available to instantiate");
    }
}
