using System;
using System.Collections.Generic;
using Qnx.Core.Extensions;
using Qnx.Core.Interfaces;
using Qnx.Core.Components;
using SDG.Unturned;
using Steamworks;
using Action = System.Action;

namespace Qnx.Core.Services;

public class PlayerService : IService
{
    public static PlayerService Singleton { get; private set; }
    public Dictionary<CSteamID, QnxPlayer> Players;
    
    public PlayerService()
    {
        Players = [];
        Provider.onEnemyConnected += onEnemyConnected;
        Provider.onEnemyDisconnected += onEnemyDisconnect;
    }
    
    public void Dispose()
    {
    }
    
    private void onEnemyConnected(SteamPlayer player)
    {
        var component = player.player.gameObject.AddComponent<QnxPlayer>();
        Players.Add(player.SteamID(), component);
        component.Initiliaze(player.player);
    }

    private void onEnemyDisconnect(SteamPlayer player)
    {
        if (!Players.TryGetValue(player.SteamID(), out var p))
            return;
        
        p.Remove();
        Players.Remove(player.SteamID());
    }
}