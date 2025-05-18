using Qnx.Unturned.Buffs;
using YamlDotNet.Serialization;

namespace Qnx.Unturned.Items.Models;

public abstract record ItemBuff
{
    [YamlIgnore] public abstract EBuff Origin { get; }
    
    public float Chance;
    public int Modifier;
}