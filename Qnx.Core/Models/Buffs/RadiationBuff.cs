using Qnx.Core.Attributes;
using Qnx.Core.Components;
using Qnx.Core.Enums;
using Qnx.Core.Interfaces;

namespace Qnx.Core.Models.Buffs;

[Buff(EBuff.RADIATION)]
public class RadiationBuff : IBuff
{
    public void Apply(QnxPlayer attacker, QnxPlayer target)
    {
        target.Player.life.serverModifyVirus(-attacker.Buffs.BuffModifier[EBuff.RADIATION]);       
    }
}