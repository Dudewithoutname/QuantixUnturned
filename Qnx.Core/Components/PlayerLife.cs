using System;
using Qnx.Core.Interfaces;
using Rocket.Unturned.Events;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using UnityEngine;
using Logger = Rocket.Core.Logging.Logger;

namespace Qnx.Core.Components;

public class PlayerLife : MonoBehaviour, IPlayerComponent
{
    private QnxPlayer _qnx;
    
    /// <summary>
    ///  Do not set manually, use Heal() / Damage()!
    /// </summary>
    public int Health
    {
        get => _health;
        set
        {
            var newHealth = value;

            if (newHealth > MaxHealth)
                newHealth = MaxHealth;

            if (newHealth < 0)
                newHealth = 0;
            
            
            _health = newHealth;
            _qnx.Hud.UpdateHealth();
        }
    }
    
    private int _health;
    
    public int Regen = 0;
    public int MaxHealth { get; private set; }
    

    public void Initialize(QnxPlayer player)
    {
        _qnx = player;
        _health = 150;
        MaxHealth = 150;
    }
    
    public void Revive()
    {
        Health = MaxHealth;
    }

    public void SetMaxHealth(int maxHealth, bool heal = false)
    {
        MaxHealth = maxHealth;
        
        if (heal)
            _health = MaxHealth;
        
        _qnx.Hud.UpdateHealth();
    }
    
    public void Damage(int damage)
    {
        if (Health - damage >= 100)
        {
            Health -= damage;
            return;
        }

        if (damage > 255)
        {
            _health -= damage - 255;
            if (_health < 0) 
                _health = 1;
            
            _qnx.Player.life.askDamage(255, Vector3.zero, EDeathCause.SUICIDE, ELimb.SKULL, CSteamID.Nil, out _);
            return;
        }
        
        _qnx.Player.life.askDamage((byte)Mathf.Max(damage, 255), Vector3.zero, EDeathCause.SUICIDE, ELimb.SKULL, CSteamID.Nil, out _);
    }

    public void Heal(int heal)
    {
        if (Health >= 100)
        {
            Health += heal;
            return;
        }

        if (heal > 255)
        {
            _qnx.Life._health += heal - 255;
            // this should also autocorrect hp
            _qnx.Player.life.askHeal(255, false, false);
            return;

        }
        
        _qnx.Player.life.askHeal((byte)heal, false, false);
    }
}