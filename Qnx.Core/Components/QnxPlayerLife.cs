using System;
using Qnx.Core.Interfaces;
using Rocket.Unturned.Events;
using Rocket.Unturned.Player;
using SDG.Unturned;
using UnityEngine;
using Logger = Rocket.Core.Logging.Logger;

namespace Qnx.Core.Components;

public class QnxPlayerLife : MonoBehaviour, IPlayerComponent
{
    private QnxPlayer _qnx;
    
    public int Health
    {
        get => _health;
        set
        {
            var newHealth = value;

            if (newHealth > MaxHealth)
                return;
            

            _health = newHealth;
            _qnx.Hud.UpdateHealth();
        }
    }

    public ushort Regen = 0;
    public ushort MaxHealth = 150;
    private int _health;

    public void Initialize(QnxPlayer player)
    {
        _qnx = player;
        _health = 150;
    }
    
    public void Revive()
    {
        Health = MaxHealth;
    }
}