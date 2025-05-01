using Qnx.Core.Components;
using Qnx.Core.Enums;
using YamlDotNet.Serialization;

namespace Qnx.Items.Models;

public abstract record Buff
{
    [YamlIgnore] public abstract EBuff Origin { get; }
    
    public float Chance;
    public int Modifier;
}