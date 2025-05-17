using System;
using System.Collections.Generic;
using Qnx.Core.Extensions;
using Qnx.Core.Components;
using Qnx.Core.Interfaces;
using Qnx.Core.Utils;
using SDG.Unturned;
using Steamworks;
using UnityEngine;

namespace Qnx.Core.Services;

public class PlayerService : SingletonService<PlayerService>
{
    public Dictionary<CSteamID, QnxPlayer> Players;

    private List<Type> _customComponents;
    
    public PlayerService()
    {
        Players = [];
        _customComponents = [];
        
        Player.onPlayerCreated += onPlayerConnect;
        Provider.onEnemyDisconnected += onPlayerDisconnect;
    }

    protected override void OnDispose()
    {
        Players = null;
        _customComponents = null;
        
        Player.onPlayerCreated -= onPlayerConnect;
        Provider.onEnemyDisconnected -= onPlayerDisconnect;
    }

    public void RegisterComponent<T>() where T : MonoBehaviour, IPlayerComponent
        => _customComponents.Add(typeof(T));
    
    private void onPlayerConnect(Player player)
    {
        var playerComponent = player.gameObject.AddComponent<QnxPlayer>();
        Players.Add(player.SteamID(), playerComponent);
        playerComponent.Initiliaze(player);

        foreach (var cType in _customComponents)
        {
            var c = player.gameObject.AddComponent(cType) as IPlayerComponent;
            c?.Initialize(playerComponent);
        }
    }

    private void onPlayerDisconnect(SteamPlayer player)
    {
        if (!Players.TryGetValue(player.SteamID(), out var p))
            return;
        
        p.Remove();
        Players.Remove(player.SteamID());
    }
}