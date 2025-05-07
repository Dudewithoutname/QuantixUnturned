using Qnx.Items.Models.Misc;
using Rocket.Core.Logging;

namespace Qnx.Items.Models.Items;

public record ClothingSet : ModifiedBase
{
    public string? Name;
    
    public float Speed;
    public float Jump;
    public float Gravity;
    
    public ClothingSetParts? Parts;
}