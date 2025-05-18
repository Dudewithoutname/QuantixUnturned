using Qnx.Unturned.Players;

namespace Qnx.Unturned.Buffs.Models;


[OnHitBuff(EBuff.SLOW)]
public class SlowBuff : IOnHitBuff
{
    public void Apply(QnxPlayer attacker, QnxPlayer target)
    {
        target.Buffs.Slow(attacker.Buffs.BuffModifier[EBuff.SLOW]);
    }
}