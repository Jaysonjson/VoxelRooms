using Godot;
using System;
using System.IO;

public partial class DiscordAvatarNode : Control {
    [Export]
    public TextureRect AvatarRect { get; set; }

    [Export]
    public Label DiscordName { get; set; }

    private bool Done { get; set; } = false;

    public override void _Ready() {
    }

    public override void _Process(double delta) {
        if(!Done || Voxelrooms.DC.User.Id != -1) {
            if(Voxelrooms.DC.AvatarTexture == null) {
                Image avatarImage = new();
                if(File.Exists(Voxelrooms.ToUserPath("discord_avatar.png"))) {
                    avatarImage.Load(Voxelrooms.ToUserPath("discord_avatar.png"));
                    Voxelrooms.DC.AvatarTexture = new();
                    Voxelrooms.DC.AvatarTexture.SetImage(avatarImage);
                } else {
                    DiscordNode.DownloadNewUserAvatar();
                }
            }
            AvatarRect.Texture = Voxelrooms.DC.AvatarTexture;
            DiscordName.Text = Voxelrooms.DC.User.Username;
            Done = true;
        }

    }
}