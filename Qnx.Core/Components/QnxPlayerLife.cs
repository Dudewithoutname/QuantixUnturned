using System;
using Qnx.Core.Interfaces;
using Rocket.Unturned.Events;
using Rocket.Unturned.Player;
using UnityEngine;
using Logger = Rocket.Core.Logging.Logger;

namespace Qnx.Core.Components;

public class QnxPlayerLife : IPlayerComponent 
{
    private QnxPlayer _qnx;
    
    public ushort Health
    {
        get => _health;
        set
        {
            var newHealth = value;

            Logger.Log("1");
            if (newHealth > MaxHealth)
                return;
            Logger.Log("2");

            if (newHealth <= 100)
            {
                _qnx.Player.life.serverModifyHealth(newHealth - _health);
                Logger.Log("3");

            }
            Logger.Log("4");

            _health = newHealth;
            _qnx.Hud.UpdateHealth();
        }
    }
    
    public ushort MaxHealth = 100;
    private ushort _health;

    public void Initialize(QnxPlayer player)
    {
        _qnx = player;
        _health = player.Player.life.health;
        UnturnedPlayerEvents.OnPlayerUpdateHealth += onHealth;
    }

    private void onHealth(UnturnedPlayer p, byte newHealth)
    {
        if (p.Player != _qnx.Player) 
            return;
        
        _health = newHealth;
        _qnx.Hud.UpdateHealth();
    }
}