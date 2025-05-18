using Qnx.Unturned.Players;

namespace Qnx.Unturned.Buffs.Models;

[OnHitBuff(EBuff.IGNITE)]
public class IgniteBuff : IOnHitBuff
{
    public void Apply(QnxPlayer attacker, QnxPlayer target)
    {
         target.Buffs.Ignite(attacker.Buffs.BuffModifier[EBuff.IGNITE]);
    }
}