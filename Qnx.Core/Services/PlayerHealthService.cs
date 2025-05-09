using HarmonyLib;
using Qnx.Core.Components;
using Qnx.Core.Extensions;
using Qnx.Core.Utils;
using Rocket.Core.Logging;
using Rocket.Unturned.Events;
using Rocket.Unturned.Player;
using SDG.NetTransport;
using SDG.Unturned;
using Steamworks;
using PlayerLife = SDG.Unturned.PlayerLife;

namespace Qnx.Core.Services;

public class PlayerHealthService : SingletonService<PlayerHealthService>
{
    private readonly ClientInstanceMethod<byte, Vector3> _damageEvent;
    public PlayerHealthService()
    {
        _damageEvent = ClientInstanceMethod<byte, Vector3>.Get(typeof(PlayerLife), "ReceiveDamagedEvent");
        
        Logger.Log(_damageEvent.ToString());
        
        UnturnedPlayerEvents.OnPlayerRevive += onRevive;
        UnturnedPlayerEvents.OnPlayerUpdateVirus += onVirus;
    }
    
    protected override void OnDispose()
    {
        UnturnedPlayerEvents.OnPlayerRevive -= onRevive;
        UnturnedPlayerEvents.OnPlayerUpdateVirus -= onVirus;

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