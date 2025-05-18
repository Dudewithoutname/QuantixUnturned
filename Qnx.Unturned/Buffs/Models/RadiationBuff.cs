using Qnx.Unturned.Players;

namespace Qnx.Unturned.Buffs.Models;


[OnHitBuff(EBuff.RADIATION)]
public class RadiationBuff : IOnHitBuff
{
    public void Apply(QnxPlayer attacker, QnxPlayer target)
    {
        target.Player.life.serverModifyVirus(-attacker.Buffs.BuffModifier[EBuff.RADIATION]);       
    }
}