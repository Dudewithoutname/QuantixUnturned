using Qnx.Unturned.Players;

namespace Qnx.Unturned.Buffs.Models;


[OnHitBuff(EBuff.FREEZE)]
public class FreezeBuff : IOnHitBuff
{
    public void Apply(QnxPlayer attacker, QnxPlayer target)
    {
        target.Buffs.Freeze(attacker.Buffs.BuffModifier[EBuff.FREEZE]);
    }
}