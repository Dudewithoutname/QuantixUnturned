using Qnx.Core.Attributes;
using Qnx.Core.Components;
using Qnx.Core.Enums;
using Qnx.Core.Extensions;
using Qnx.Core.Interfaces;
using SDG.Unturned;

namespace Qnx.Core.Models.Buffs;

[Buff(EBuff.LIFESTEAL)]
public class LifestealBuff : IBuff
{
    [LoadAsset(EAssetType.EFFECT, 125)]
    private static EffectAsset _effect;

    public void Apply(QnxPlayer attacker, QnxPlayer target)
    {
        attacker.Life.Heal(attacker.Buffs.BuffModifier[EBuff.LIFESTEAL]);
        var e = new TriggerEffectParameters(_effect)
        {
            position = target.Player.transform.position
        };
        e.SetRelevantPlayer(attacker.Player.TC());
        EffectManager.triggerEffect(e);
    }
}