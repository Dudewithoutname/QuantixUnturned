using Qnx.Core.Attributes;
using Qnx.Core.Components;
using Qnx.Core.Enums;
using Qnx.Core.Interfaces;

namespace Qnx.Core.Models.Buffs;

[Buff(EBuff.DRUGS)]
public class DrugsBuff : IBuff
{
    public void Apply(QnxPlayer attacker, QnxPlayer target)
    {
        target.Player.life.serverModifyHallucination(attacker.Buffs.BuffModifier[EBuff.DRUGS]);
    }
}