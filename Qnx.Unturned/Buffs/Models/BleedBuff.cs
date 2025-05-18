using Qnx.Unturned.Players;

namespace Qnx.Unturned.Buffs.Models;

[OnHitBuff(EBuff.BLEED)]
public class BleedBuff : IOnHitBuff
{
    public void Apply(QnxPlayer attacker, QnxPlayer target)
    {
        target.Player.life.serverSetBleeding(true);
    }
}