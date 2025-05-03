using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace Qnx.Items.Models;

public record ModifiedBase
{
    [YamlMember(Order = 99)]
    public List<Buff>? Buffs;
}