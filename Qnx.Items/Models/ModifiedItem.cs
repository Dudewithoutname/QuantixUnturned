using Qnx.Core.Components;
using SDG.Unturned;
using YamlDotNet.Serialization;

namespace Qnx.Items.Models;

// ReSharper disable once UnassignedReadonlyField
public record ModifiedItem : ModifiedBase
{
    
    [YamlMember(Order = 0)]
    public readonly ushort Id;
    
    public virtual bool Equals(ModifiedItem? other)
        => other is not null && Id == other.Id;

    public override int GetHashCode() => Id.GetHashCode();
}