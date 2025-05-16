using System.Collections;
using Qnx.Core.Attributes;
using Qnx.Core.Components;
using Qnx.Core.Enums;
using Qnx.Core.Interfaces;
using SDG.Unturned;

namespace Qnx.Core.Models.Buffs;

[Buff(EBuff.IGNITE)]
public class IgniteBuff : IBuff
{
    public void Apply(QnxPlayer attacker, QnxPlayer target)
    {
         target.Buffs.Ignite(attacker.Buffs.BuffModifier[EBuff.IGNITE]);
    }
}