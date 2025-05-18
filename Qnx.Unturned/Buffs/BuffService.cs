using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Qnx.Unturned.Extensions;
using Qnx.Unturned.Players;
using Qnx.Unturned.Services;
using SDG.Unturned;
using Steamworks;
using Random = UnityEngine.Random;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Qnx.Unturned.Buffs;

[Service(EServiceType.NORMAL)]
public class BuffService : IService
{
    private Dictionary<EBuff, IOnHitBuff> _buffs;
    
    public void Initialize()
    {
        _buffs = getOnHitBuffs();

        DamageTool.damagePlayerRequested += onDamage;
    }

    public void Dispose()
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
        
        if (victim!.Buffs.BuffModifier[EBuff.DODGE] >= 1 && victim.Buffs.BuffChance[EBuff.DODGE] >= Random.Range(0.0f, 100.0f))
        {
            shouldAllow = false;
            return;
        }
        
        applyDebuffs(attacker!, victim);
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

    private Dictionary<EBuff, IOnHitBuff> getOnHitBuffs()
    {
        var result = new Dictionary<EBuff, IOnHitBuff>();

        var types = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t.GetCustomAttribute<OnHitBuff>() != null);

        foreach (var type in types)
        {
            var attr = type.GetCustomAttribute<OnHitBuff>();
            if (attr == null) continue;
            
            #pragma warning disable CS8604 // Possible null reference assignment.
            result.Add(attr.Origin, Activator.CreateInstance(type) as IOnHitBuff);
            #pragma warning restore CS8604 // Possible null reference assignment.
        }

        return result; 
    }
}