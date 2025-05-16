using Qnx.Core.Attributes;
using Qnx.Core.Components;
using Qnx.Core.Enums;
using Qnx.Core.Interfaces;

namespace Qnx.Core.Models.Buffs;

[Buff(EBuff.SLOW)]
public class SlowBuff : IBuff
{
    public void Apply(QnxPlayer attacker, QnxPlayer target)
    {
        target.Buffs.Slow(attacker.Buffs.BuffModifier[EBuff.SLOW]);
    }
}