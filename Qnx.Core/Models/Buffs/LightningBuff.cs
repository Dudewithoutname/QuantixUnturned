using Qnx.Core.Attributes;
using Qnx.Core.Components;
using Qnx.Core.Enums;
using Qnx.Core.Extensions;
using Qnx.Core.Interfaces;
using SDG.Unturned;

namespace Qnx.Core.Models.Buffs;

[Buff(EBuff.LIGHTNING)]
public class LightningBuff : IBuff
{
    [LoadAsset(EAssetType.EFFECT, 127)]
    private static EffectAsset _effect = null!;

    public void Apply(QnxPlayer attacker, QnxPlayer target)
    {
        target.Life.Damage(
            attacker.Buffs.BuffModifier[EBuff.LIGHTNING], 
            EDeathCause.BURNING, 
            ELimb.SPINE, 
            attacker.Player.SteamID()
            );
        
        var e = new TriggerEffectParameters(_effect)
        {
            position = target.Player.transform.position,
        };
        EffectManager.triggerEffect(e);
    }
}