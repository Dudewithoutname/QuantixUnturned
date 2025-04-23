using SDG.Unturned;
using UnityEngine;
using Action = System.Action;

namespace Qnx.Core.Components;

public class QnxPlayer : MonoBehaviour
{

    public Player Player { get; private set; }
    public QnxPlayerHud Hud { get; private set; }
    public QnxPlayerLife Life { get; private set; }

    
    internal void Initiliaze(Player player)
    {
        Player = player;
        Hud = gameObject.AddComponent<QnxPlayerHud>();
        Life = new QnxPlayerLife();

        Life.Initialize(this);
        Hud.Initialize(this);
    }

    internal void Remove()
    {
        Destroy(this);
    }
}