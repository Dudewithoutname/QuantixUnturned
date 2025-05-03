using Qnx.Core.Components;
using SDG.Unturned;
using YamlDotNet.Serialization;

namespace Qnx.Items.Models;

public record ModifiedItem : ModifiedBase
{
    [YamlMember(Order = 0)]
    public ushort Id;
}