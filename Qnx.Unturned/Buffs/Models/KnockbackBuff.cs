using Qnx.Unturned.Players;

namespace Qnx.Unturned.Buffs.Models;


[OnHitBuff(EBuff.KNOCKBACK)]
public class KnockbackBuff : IOnHitBuff
{
    public void Apply(QnxPlayer attacker, QnxPlayer target)
    {
        var direction = attacker.Player.transform.forward;
        direction.y = 0f;
        direction.Normalize();
        
        target.Player.movement.pendingLaunchVelocity += direction * attacker.Buffs.BuffModifier[EBuff.KNOCKBACK];
    }
}