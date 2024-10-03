using Godot;
using System.IO;

public partial class Voxelrooms {
    public static class DC {
        public static DiscordNode.DiscordCache Cache { get; set; } = new();

        public static Discord.Discord RPC { get; set; } = null;
        public static Discord.User User { get; set; } = new() {
            Id = -1
        };

        public static ImageTexture AvatarTexture {get; set; } = null;
    }
    
    public static void CreateNonExistingDir(string path) {
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);
    }

    public static string ToUserPath(string file) {
        return ProjectSettings.GlobalizePath("user://" + file);
    }
    
}
