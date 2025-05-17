using System.Collections.Generic;
using Qnx.Core.Components;
using Qnx.Core.Enums;
using Qnx.Core.Extensions;
using Qnx.Core.Interfaces;
using Qnx.Core.Utils;
using SDG.Unturned;
using Steamworks;
using UnityEngine;
using Logger = Rocket.Core.Logging.Logger;

namespace Qnx.Core.Services;

public class BuffService : SingletonService<BuffService>
{
    private Dictionary<EBuff, IBuff> _buffs;
    
    public BuffService()
    {
        _buffs = AttributeLoader.GetBuffProviders();
        
        DamageTool.damagePlayerRequested += onDamage;
    }

    protected override void OnDispose()
    {
        DamageTool.damagePlayerRequested -= onDamage;
    }

    private void onDamage(ref DamagePlayerParameters parameters, ref bool shouldAllow)
    {
        if (parameters.killer == CSteamID.Nil || !parameters.player)
            return;

        var attackerPlayer = PlayerTool.getPlayer(parameters.killer);
        if (!attackerPlayer) 
            return;
        
        var attacker = attackerPlayer.QnxComponent();
        var victim = parameters.player.QnxComponent();

        if (victim.Buffs.BuffModifier[EBuff.DODGE] >= 1 && victim.Buffs.BuffChance[EBuff.DODGE] >= Random.Range(0.0f, 100.0f))
        {
            shouldAllow = false;
            return;
        }
        
        applyDebuffs(attacker, victim);
    }

    private void applyDebuffs(QnxPlayer attacker, QnxPlayer target)
    {
        if (target.Buffs.BuffModifier[EBuff.THORNS] >= 1 && target.Buffs.BuffChance[EBuff.THORNS] >= Random.Range(0.0f, 100.0f))
        {
            attacker.Life.Damage(target.Buffs.BuffModifier[EBuff.THORNS], EDeathCause.ACID, ELimb.SPINE, target.Player.SteamID());
        }

        foreach (var b in _buffs)
        {
            if (attacker.Buffs.BuffModifier[b.Key] <= 0) continue;
            if (attacker.Buffs.BuffChance[b.Key] < Random.Range(0.0f, 100.0f)) continue;
            b.Value.Apply(attacker, target);
        }
    }
}