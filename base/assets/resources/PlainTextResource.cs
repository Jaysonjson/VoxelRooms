using Godot;
using System;

public partial class PlainTextResource : Resource {
    [Export]
    public string Text { get; set; } = "";
}
