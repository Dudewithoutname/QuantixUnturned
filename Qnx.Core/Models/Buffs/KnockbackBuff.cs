using Qnx.Core.Attributes;
using Qnx.Core.Components;
using Qnx.Core.Enums;
using Qnx.Core.Interfaces;

namespace Qnx.Core.Models.Buffs;

[Buff(EBuff.KNOCKBACK)]
public class KnockbackBuff : IBuff
{
    public void Apply(QnxPlayer attacker, QnxPlayer target)
    {
        var direction = attacker.Player.transform.forward;
        direction.y = 0f;
        direction.Normalize();
        
        target.Player.movement.pendingLaunchVelocity += direction * attacker.Buffs.BuffModifier[EBuff.KNOCKBACK];
    }
}