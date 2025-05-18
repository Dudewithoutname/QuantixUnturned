using Qnx.Unturned.Players;

namespace Qnx.Unturned.Buffs;

public interface IOnHitBuff
{
    public void Apply(QnxPlayer attacker, QnxPlayer target);

}