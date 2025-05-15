using Qnx.Core.Components;

namespace Qnx.Core.Interfaces;

public interface IBuff
{
    public void Apply(QnxPlayer attacker, QnxPlayer target);
}