using Qnx.Unturned.Players.Components;
using SDG.Unturned;
using UnityEngine;
using PlayerLife = Qnx.Unturned.Players.Components.PlayerLife;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Qnx.Unturned.Players;

public class QnxPlayer : MonoBehaviour
{
    public Player Player { get; private set; }
    public PlayerHud Hud { get; private set; }
    public PlayerLife Life { get; private set; }
    public PlayerBuffs Buffs { get; private set; }

    
    internal void Initiliaze(Player player)
    {
        Player = player;
        Hud = gameObject.AddComponent<PlayerHud>();
        Life = gameObject.AddComponent<PlayerLife>();
        Buffs = gameObject.AddComponent<PlayerBuffs>();
        
        Buffs.Initialize(this);
        Life.Initialize(this);
        Hud.Initialize(this);
    }
    
    internal void Remove()
    {
        Destroy(this);
    }
}