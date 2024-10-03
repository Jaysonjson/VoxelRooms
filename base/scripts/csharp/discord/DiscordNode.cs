using Godot;
using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;

public partial class DiscordNode : Node {

    public partial class DiscordCache {
        public String Avatar { get; set; } = "";
        
        public void Save() {
            File.WriteAllText(Voxelrooms.ToUserPath("discord.json"), JsonConvert.SerializeObject(this, Formatting.Indented));
        }

        public static DiscordCache Load() {
            if(File.Exists(Voxelrooms.ToUserPath("discord.json"))) {
                return JsonConvert.DeserializeObject<DiscordCache>(File.ReadAllText(Voxelrooms.ToUserPath("discord.json")));
            } else {
                DiscordCache cache = new();
                cache.Save();
                return cache;
            }
        }
    }

    public override void _Ready() {
        if(Voxelrooms.DC.RPC == null) {
            Voxelrooms.DC.Cache = DiscordCache.Load();
            Voxelrooms.DC.RPC = new Discord.Discord(1257550296641568909L, (ulong)Discord.CreateFlags.NoRequireDiscord);
            Voxelrooms.DC.RPC.SetLogHook(Discord.LogLevel.Debug, DiscordLog);
            Discord.Activity activity = new()
            {
                Details = "Chilling in the Main Menu",
                Assets = new() {
                    LargeImage = "icon",
                    LargeText = "Zeitriss 0.1"
                }            
            };
            Voxelrooms.DC.RPC.GetUserManager().OnCurrentUserUpdate += () => {
                Voxelrooms.DC.User =  Voxelrooms.DC.RPC.GetUserManager().GetCurrentUser();
                DownloadNewUserAvatar();
            };
            UpdateActivity(activity);
        }
	}

    public static void DiscordLog(Discord.LogLevel level, string message) {
        GD.Print("Discord:{0} - {1}", level, message);
    }

    public static bool DownloadUserAvatar() {
        WebClient client = new();
        client.DownloadFile(new Uri("https://cdn.discordapp.com/avatars/" + Voxelrooms.DC.User.Id + "/" + Voxelrooms.DC.User.Avatar + ".png?size=512"), Voxelrooms.ToUserPath("discord_avatar.png"));
        Voxelrooms.DC.Cache.Avatar = Voxelrooms.DC.User.Avatar;
        Voxelrooms.DC.Cache.Save();
        return true;
    }

    public static void DownloadNewUserAvatar() {
        if (Voxelrooms.DC.User.Id != -1) {
            if (!File.Exists(Voxelrooms.ToUserPath("discord_avatar.png")) ||
                Voxelrooms.DC.Cache.Avatar != Voxelrooms.DC.User.Avatar) {
                DownloadUserAvatar();
            }
        }
    }
    
    public static void UpdateActivity(Discord.Activity activity) {
        Voxelrooms.DC.RPC?.GetActivityManager().UpdateActivity(activity, (res) => {});
    }

	public override void _Process(double delta) {
        Voxelrooms.DC.RPC?.RunCallbacks();
	}
}