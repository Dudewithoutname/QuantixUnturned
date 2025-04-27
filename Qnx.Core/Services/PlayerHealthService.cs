using Qnx.Core.Extensions;
using Qnx.Core.Utils;
using Rocket.Core.Logging;
using Rocket.Unturned.Events;
using Rocket.Unturned.Player;
using SDG.NetTransport;
using SDG.Unturned;
using Steamworks;

namespace Qnx.Core.Services;

public class PlayerHealthService : SingletonService<PlayerHealthService>
{
    private readonly ClientInstanceMethod<byte, Vector3> _damageEvent;
    public PlayerHealthService()
    {
        _damageEvent = ClientInstanceMethod<byte, Vector3>.Get(typeof(PlayerLife), "ReceiveDamagedEvent");
        Logger.Log(_damageEvent.ToString());
        
        UnturnedPlayerEvents.OnPlayerRevive += onRevive;
    }
    protected override void OnDispose()
    {
    }

    private void onRevive(UnturnedPlayer player, Vector3 _v, byte _e)
    {
        player.Player.QnxComponent().Life.Revive();
    }
    public byte HandleDamage(Player player, byte damage, Vector3 newRagdoll, out bool allow)
    {
        var qnx = player.QnxComponent();
        var prev = qnx.Life.Health;
        qnx.Life.Health -= damage;
        
        
        if (qnx.Life.Health >= 100)
        {					
            _damageEvent.Invoke(player.life.GetNetId(), ENetReliability.Reliable, player.channel.GetOwnerTransportConnection(), damage, newRagdoll.normalized);
            allow = false;
            return 0;
        }
        
        // delta when transitioning damage to normal
        if (prev > 100 && qnx.Life.Health < 100)
        {
            allow = true;
            return (byte)(100 - qnx.Life.Health);
        }
        
        allow = true;
        return damage;
    }
}