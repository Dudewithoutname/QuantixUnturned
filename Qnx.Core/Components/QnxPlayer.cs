using SDG.Unturned;
using UnityEngine;
using Action = System.Action;

namespace Qnx.Core.Components;

public class QnxPlayer : MonoBehaviour
{

    public Player Player { get; private set; }
    public PlayerHud Hud { get; private set; }
    public PlayerLife Life { get; private set; }

    
    internal void Initiliaze(Player player)
    {
        Player = player;
        Hud = gameObject.AddComponent<PlayerHud>();
        Life = new PlayerLife();

        Life.Initialize(this);
        Hud.Initialize(this);
    }
    
    internal void Remove()
    {
        Destroy(this);
    }
}