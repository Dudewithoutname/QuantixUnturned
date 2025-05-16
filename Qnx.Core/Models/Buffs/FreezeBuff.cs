using Qnx.Core.Attributes;
using Qnx.Core.Components;
using Qnx.Core.Enums;
using Qnx.Core.Interfaces;

namespace Qnx.Core.Models.Buffs;

[Buff(EBuff.FREEZE)]
public class FreezeBuff : IBuff
{
    public void Apply(QnxPlayer attacker, QnxPlayer target)
    {
        target.Buffs.Freeze(attacker.Buffs.BuffModifier[EBuff.FREEZE]);
    }
}