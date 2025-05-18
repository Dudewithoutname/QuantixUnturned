using Qnx.Unturned.Players;

namespace Qnx.Unturned.Buffs.Models;

[OnHitBuff(EBuff.DRUGS)]
public class DrugsBuff : IOnHitBuff
{
    public void Apply(QnxPlayer attacker, QnxPlayer target)
    {
        target.Player.life.serverModifyHallucination(attacker.Buffs.BuffModifier[EBuff.DRUGS]);
    }
}