using Qnx.Unturned.Extensions;
using Qnx.Unturned.Players;
using Qnx.Unturned.Utils.Assets;
using SDG.Unturned;

namespace Qnx.Unturned.Buffs.Models;


[OnHitBuff(EBuff.LIFESTEAL)]
public class LifestealBuff : IOnHitBuff
{
    [LoadAsset(EAssetType.EFFECT, 124)]
    private static EffectAsset _effect = null!;

    public void Apply(QnxPlayer attacker, QnxPlayer target)
    {
        attacker.Life.Heal(attacker.Buffs.BuffModifier[EBuff.LIFESTEAL]);
        var pos = target.Player.transform.position;
        pos.y += 0.65f;
        
        var e = new TriggerEffectParameters(_effect)
        {
            position = pos,
            reliable = false
        };
        e.SetUniformScale(0.3f);
        e.SetRelevantPlayer(attacker.Player.TC());
        
        EffectManager.triggerEffect(e);
    }
}