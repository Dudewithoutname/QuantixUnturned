using Qnx.Unturned.Items.Models.Misc;

namespace Qnx.Unturned.Items.Models.Types;

public record GunSet : ModifiedBase
{
    public ushort Id;
    public GunSetParts? Attachments;
}