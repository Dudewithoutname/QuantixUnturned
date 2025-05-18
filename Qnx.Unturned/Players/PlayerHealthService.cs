using Qnx.Unturned.Extensions;
using Qnx.Unturned.Services;
using Qnx.Unturned.Utils;
using Rocket.Unturned.Events;
using Rocket.Unturned.Player;
using SDG.NetTransport;
using SDG.Unturned;

namespace Qnx.Unturned.Players;

[Service(EServiceType.NORMAL)]
public class PlayerHealthService : Singleton<PlayerHealthService>, IService
{
    private readonly ClientInstanceMethod<byte, Vector3> _damageEvent;

    public PlayerHealthService()
    {
        _damageEvent = ClientInstanceMethod<byte, Vector3>.Get(typeof(PlayerLife), "ReceiveDamagedEvent");
        
        Logger.Log(_damageEvent.ToString());
        UnturnedPlayerEvents.OnPlayerRevive += onRevive;
        UnturnedPlayerEvents.OnPlayerUpdateVirus += onVirus;
        
    }
    public void Dispose()
    {
        UnturnedPlayerEvents.OnPlayerRevive -= onRevive;
        UnturnedPlayerEvents.OnPlayerUpdateVirus -= onVirus;
        RemoveInstance();
    }
    
    private void onRevive(UnturnedPlayer player, Vector3 _v, byte _e)
        => player.Player.QnxComponent().Life.Revive();

    private void onVirus(UnturnedPlayer player, byte virus)
        => player.Player.QnxComponent().Hud.UpdateVirus(virus);
    
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

    public byte HandleHeal(PlayerLife life, byte amount, bool healBleeding, bool healBroken, out bool allow)
    {
        var qnx = life.player.QnxComponent();

        if (qnx.Life.Health > 100)
        {
            if (life.isBleeding && healBleeding)
            {
                life.serverSetBleeding(false);
            }

            if (life.isBroken && healBroken)
            {
                life.serverSetLegsBroken(false);
            }

            qnx.Life.Health += amount;
            allow = false;
            return 0;
        }

        qnx.Life.Health += amount;
        allow = true;
        return amount;
    }

}