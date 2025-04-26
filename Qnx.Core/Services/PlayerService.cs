using System.Collections.Generic;
using Qnx.Core.Extensions;
using Qnx.Core.Components;
using Qnx.Core.Utils;
using SDG.Unturned;
using Steamworks;

namespace Qnx.Core.Services;

public class PlayerService : SingletonService<PlayerService>
{
    public Dictionary<CSteamID, QnxPlayer> Players;
    
    public PlayerService()
    {
        Players = [];
        
        Player.onPlayerCreated += onPlayerConnect;
        Provider.onEnemyDisconnected += onPlayerDisconnect;
    }

    protected override void OnDispose()
    {
        Players = null;
        Player.onPlayerCreated -= onPlayerConnect;
        Provider.onEnemyDisconnected -= onPlayerDisconnect;
    }
    
    private void onPlayerConnect(Player player)
    {
        var component = player.gameObject.AddComponent<QnxPlayer>();
        Players.Add(player.SteamID(), component);
        component.Initiliaze(player);
    }

    private void onPlayerDisconnect(SteamPlayer player)
    {
        if (!Players.TryGetValue(player.SteamID(), out var p))
            return;
        
        p.Remove();
        Players.Remove(player.SteamID());
    }
}