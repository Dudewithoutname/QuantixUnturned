using System.Collections.Generic;
using Qnx.Items.Models.Misc;

namespace Qnx.Items.Models.Items;

public record GunSet : ModifiedBase
{
    public ushort Id;
    public GunSetParts? Attachments;
}