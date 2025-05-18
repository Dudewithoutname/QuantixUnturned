using Qnx.Unturned.Players.Components;
using SDG.Unturned;
using UnityEngine;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Qnx.Unturned.Players;

public class QnxPlayer : MonoBehaviour
{
    public Player Player { get; private set; }
    public PlayerHud Hud { get; private set; }
    public PlayerHealth Life { get; private set; }
    public PlayerBuffs Buffs { get; private set; }
    public PlayerBuffItems BuffItems { get; private set; }

    
    internal void Initiliaze(Player player)
    {
        Player = player;
        Hud = gameObject.AddComponent<PlayerHud>();
        Life = gameObject.AddComponent<PlayerHealth>();
        Buffs = gameObject.AddComponent<PlayerBuffs>();
        BuffItems = gameObject.AddComponent<PlayerBuffItems>();
        
        Buffs.Initialize(this);
        Life.Initialize(this);
        Hud.Initialize(this);
        BuffItems.Initialize(this);
    }
    
    internal void Remove()
    {
        Destroy(this);
    }
}