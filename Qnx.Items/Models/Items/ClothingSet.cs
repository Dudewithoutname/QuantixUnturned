using Qnx.Items.Models.Misc;
using Rocket.Core.Logging;

namespace Qnx.Items.Models.Items;

public record ClothingSet : ModifiedBase
{
    public ClothingSetParts? Parts;
}