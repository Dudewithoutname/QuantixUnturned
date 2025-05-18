using System;
using Qnx.Unturned.Players;

namespace Qnx.Unturned.Buffs;

[AttributeUsage(AttributeTargets.Class)]
internal class OnHitBuff(EBuff origin) : Attribute
{
    public readonly EBuff Origin = origin;
}