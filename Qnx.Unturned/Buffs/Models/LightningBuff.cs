using Qnx.Unturned.Extensions;
using Qnx.Unturned.Players;
using Qnx.Unturned.Utils.Assets;
using SDG.Unturned;

namespace Qnx.Unturned.Buffs.Models;

[OnHitBuff(EBuff.LIGHTNING)]
public class LightningBuff : IOnHitBuff
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