using System;

namespace Qnx.Unturned.Services;

[AttributeUsage(AttributeTargets.Class)]
public class Service(EServiceType type) : Attribute
{
    public readonly EServiceType Type = type;
}