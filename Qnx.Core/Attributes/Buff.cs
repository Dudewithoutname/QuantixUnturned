using System;
using Qnx.Core.Components;
using Qnx.Core.Enums;

namespace Qnx.Core.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class Buff(EBuff origin) : Attribute
{
    public readonly EBuff Origin = origin;
    
    public virtual void Initialize() {}
    public virtual void Apply(QnxPlayer attacker, QnxPlayer target) {}
}