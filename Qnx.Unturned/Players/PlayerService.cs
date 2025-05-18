using System.Collections.Generic;
using Qnx.Unturned.Extensions;
using Qnx.Unturned.Services;
using Qnx.Unturned.Utils;
using SDG.Unturned;
using Steamworks;

namespace Qnx.Unturned.Players;

[Service(EServiceType.NORMAL)]
public class PlayerService : Singleton<PlayerService>, IService
{
    public Dictionary<CSteamID, QnxPlayer> Players = [];

    public void Initialize()
    {
        Player.onPlayerCreated += onPlayerConnect;
        Provider.onEnemyDisconnected += onPlayerDisconnect;
    }

    public void Dispose()
    {
        Player.onPlayerCreated -= onPlayerConnect;
        Provider.onEnemyDisconnected -= onPlayerDisconnect;
    }
    
    private void onPlayerConnect(Player player)
    {
        var playerComponent = player.gameObject.AddComponent<QnxPlayer>();
        Players.Add(player.SteamID(), playerComponent);
        playerComponent.Initiliaze(player);
    }

    private void onPlayerDisconnect(SteamPlayer player)
    {
        if (!Players.TryGetValue(player.SteamID(), out var p))
            return;
        
        p.Remove();
        Players.Remove(player.SteamID());
    }
}