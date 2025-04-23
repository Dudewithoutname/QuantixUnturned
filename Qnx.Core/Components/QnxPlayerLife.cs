using System;
using Qnx.Core.Interfaces;

namespace Qnx.Core.Components;

public class QnxPlayerLife : IPlayerComponent 
{
    private QnxPlayer _qnx;
    
    public Action OnHealthChanged;

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
                _qnx.Player.life.serverModifyHealth(-100 + _health);
            }
            
            _qnx.Hud.UpdateHealth();
        }
    }
    
    public ushort MaxHealth = 100;
    private ushort _health;

    public void Initialize(QnxPlayer player)
    {
        _qnx = player;
    }
}