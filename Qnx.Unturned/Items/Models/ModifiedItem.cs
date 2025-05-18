using YamlDotNet.Serialization;

namespace Qnx.Unturned.Items.Models;

// ReSharper disable once NonReadonlyMemberInGetHashCode
public record ModifiedItem : ModifiedBase
{
    [YamlMember(Order = 0)]
    public ushort Id;
    
    public virtual bool Equals(ModifiedItem? other)
        => other is not null && Id == other.Id;

    public override int GetHashCode() => Id.GetHashCode();
}