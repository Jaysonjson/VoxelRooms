using Godot;
using System;
using Steamworks;

public partial class SteamworksTest : Node {
    public override void _Ready() {
        GD.Print("Steam is running: " + SteamAPI.IsSteamRunning());
        try
        {
            if (SteamAPI.Init())
            {
                GD.Print("Geht: " + SteamFriends.GetPersonaName());
                for (int i = 0; i < SteamFriends.GetFriendCount(EFriendFlags.k_EFriendFlagImmediate); ++i)
                {
                    GD.Print(SteamFriends.GetFriendPersonaName(SteamFriends.GetFriendByIndex(i, EFriendFlags.k_EFriendFlagImmediate)));
                }
            }
            else
            {
                GD.Print("Nyet");
            }
        }
        catch (Exception e)
        {
            GD.Print(e);
        }
    }

    public override void _ExitTree()
    {
        try
        {
            SteamAPI.Shutdown();
        }
        catch (Exception e)
        {
            GD.PrintErr("Steamworks error" + e);
        }
    }
}
