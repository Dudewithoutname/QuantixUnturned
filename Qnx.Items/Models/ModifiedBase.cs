using System.Collections.Generic;
using Qnx.Items.Components;
using YamlDotNet.Serialization;

namespace Qnx.Items.Models;

public record ModifiedBase
{
    [YamlMember(Order = 99)]
    public List<Buff>? Buffs;

    public virtual void AddToPlayer(PlayerBuffItems player)
    {
        foreach (var buff in Buffs!)
        {
            player.Qnx.Buffs.AddBuff(buff.Origin, buff.Chance, buff.Modifier);
        }
    }

    public virtual void RemoveFromPlayer(PlayerBuffItems player)
    {
        foreach (var buff in Buffs!)
        {
            player.Qnx.Buffs.RemoveBuff(buff.Origin, buff.Chance, buff.Modifier);
        }
    }
}