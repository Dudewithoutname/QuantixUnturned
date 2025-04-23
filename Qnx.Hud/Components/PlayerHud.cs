using Qnx.Core.Models;

namespace Qnx.Hud.Components;

public class PlayerHud : QnxPlayerComponent
{
    private const ushort ID = 53872;
    private const short KEY = 6921;

    private QnxPlayer _player;
    
    public override void Init(QnxPlayer player)
    {
        _player = player;
    }
    
}