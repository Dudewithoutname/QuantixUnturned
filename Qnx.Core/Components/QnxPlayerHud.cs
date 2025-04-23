using Qnx.Core.Interfaces;
using SDG.Unturned;
using UnityEngine;

namespace Qnx.Core.Components;

public class PlayerHud : MonoBehaviour, IPlayerComponent
{
    private QnxPlayer _player;
    
    public void OnAssigned(QnxPlayer player)
    {
        _player = player;
    }
    
}