using System.Collections.Generic;
using Qnx.Unturned.Players.Components;
using YamlDotNet.Serialization;

namespace Qnx.Unturned.Items.Models;

public record ModifiedBase
{
    [YamlMember(Order = 99)]
    public List<ItemBuff>? Buffs;

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