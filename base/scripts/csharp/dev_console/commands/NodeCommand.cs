using Godot;
using System;
using System.Linq;
using System.Linq.Expressions;

public partial class NodeCommand : IConsoleCommand {
	
    public void Execute(DevConsole console, string[] arguments) {
		if(arguments.Length == 1) {
            console.PrintConsoleError("No Arguments given, use \"help node\" to show usage of the node command");
            return;
        }
		if(arguments.Length >= 3) {
			if(arguments[1] != "add" && arguments[1] != "switch") {
				Node node = console.GetTree().CurrentScene.GetNode(arguments[2]);
				if(node == null) {
					console.PrintConsoleError("Node " + arguments[2] + " not found in Scene");
					return;
				}
				switch(arguments[1]) {
					case "remove": {
						node.QueueFree(); 
						console.PrintConsoleInformation("Queued Node \"" + node.Name + "\" for removal");
						break;
					}

					case "set": {
						if(arguments.Length == 5) {
							if(arguments[4].Contains("vec2")) {
								Vector2 vector2 = Vec2Argument.INSTANCE.Serialize(arguments[4]);
								console.PrintConsoleSubInformation("Found Vector2 with x: " + vector2.X.ToString() + " and y: " + vector2.Y.ToString());
								node.Set(arguments[3], vector2);
							} else {
								node.Set(arguments[3], arguments[4]);
							}
							console.PrintConsoleInformation("Changed Property \"" + arguments[3] +  "\" of Node \"" + node.Name + "\" to " + arguments[4]);
						} else {
							console.PrintConsoleError("Insufficient Arguments given, use \"help node\" to show usage of the node command");
						}
						break;
					}
				}
			} else {
                if (arguments[1] == "add") {
                    try {
                        Node currentScene = console.GetTree().CurrentScene;
                        int index = currentScene.GetChildren().Count;
                        if (arguments.Length == 4) {
                            index = arguments[3].ToInt();
                        }
                        console.PrintConsoleSubInformation("Loading Scene " + arguments[2]);
                        PackedScene packedScene = GD.Load<PackedScene>(arguments[2]);
                        console.PrintConsoleSubInformation("Instantiating Scene " + arguments[2]);
                        Node sceneNode = packedScene.Instantiate();
                        console.PrintConsoleSubInformation("Removing Dev Console from Scene " + arguments[2]);
                        sceneNode.GetNode("DevConsole").QueueFree();
                        currentScene.AddChild(sceneNode);
                        currentScene.MoveChild(console, -1);
                    } catch (Exception e) {
                        console.PrintConsoleError(e.Message);
                    }
                } else {
					PackedScene scene = GD.Load<PackedScene>(arguments[2]);
					console.GetTree().ChangeSceneToPacked(scene);
                }
            }
		} else {
			console.PrintConsoleError("Insufficient Arguments given, use \"help node\" to show usage of the node command");
		}
    }

    public void PrintHelpUsage(DevConsole console, bool advanced) {
		console.PrintConsoleSubInformation("node set [node_name:string] [property_name:string] [property_value:any] - changes the Property of a Node");
		console.PrintConsoleSubInformation("Use vec2(x,y) for a vec2 Property", true);
		console.PrintConsoleSubInformation("node remove [node_name:string] - deletes the node");
		console.PrintConsoleSubInformation("node add [scene_name:string] - adds a Scene to the Current Scene", true);
		console.PrintConsoleSubInformation("node add [scene_name:string] [index:int] - adds a Scene to the Current Scene in the current index");
		console.PrintConsoleWarning("Using \"node add\" could freeze the game for a moment");
		console.PrintConsoleSubInformation("node switch [scene_name:string] - switches to the given scene", true);
    }


}
