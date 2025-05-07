using SDG.Unturned;
using UnityEngine;
using Action = System.Action;

namespace Qnx.Core.Components;

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
        Life = new PlayerLife();
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