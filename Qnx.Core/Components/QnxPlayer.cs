using System;
using System.Collections.Generic;
using Qnx.Core.Services;
using SDG.Unturned;
using UnityEngine;
using Action = System.Action;

namespace Qnx.Core.Models;

public class QnxPlayer : MonoBehaviour
{
    public Action OnHealthChanged;

    public Player Player { get; private set; }

    public ushort Health
    {
        get => _health;
        set
        {
            _health = value;
            
            if (_health > MaxHealth) 
                _health = MaxHealth;

            if (_health <= 100)
            {
                Player.life.serverModifyHealth(-100 + _health);
            }
            
            OnHealthChanged.Invoke();
        }
    }
    public ushort MaxHealth = 100;
    private ushort _health;
    
    
    
    internal void Initiliaze(Player player)
    {
        Player = player;
    }

    internal void Remove()
    {
        Destroy(this);
    }
}