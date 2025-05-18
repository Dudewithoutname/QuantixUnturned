using Qnx.Unturned.Items.Models.Misc;
using Qnx.Unturned.Players.Components;

namespace Qnx.Unturned.Items.Models.Types;

public record ClothingSet : ModifiedBase
{
    public string? Name;

    public int Health;

    public float Speed;
    public float Jump;
    public float Gravity;
    
    public ClothingSetParts? Parts;
    
    
    public override void AddToPlayer(PlayerBuffItems player)
    {
        base.AddToPlayer(player);
        
        player.Qnx.Life.SetMaxHealth(player.Qnx.Life.MaxHealth + Health);
            
        if (Speed != 0) 
            player.Qnx.Buffs.SpeedMult += Speed;
        
        if (Jump != 0) 
            player.Qnx.Buffs.JumpMult += Jump;
        
        if (Gravity != 0) 
            player.Qnx.Buffs.GravityMult += Gravity;
        
        player.Qnx.Buffs.UpdateMovementMultipliers();
    }

    public override void RemoveFromPlayer(PlayerBuffItems player)
    {
        base.RemoveFromPlayer(player);
        
        player.Qnx.Life.SetMaxHealth(player.Qnx.Life.MaxHealth - Health);

        if (Speed != 0) 
            player.Qnx.Buffs.SpeedMult -= Speed;
        
        if (Jump != 0) 
            player.Qnx.Buffs.JumpMult -= Jump;
        
        if (Gravity != 0) 
            player.Qnx.Buffs.GravityMult -= Gravity;
        
        player.Qnx.Buffs.UpdateMovementMultipliers();
    }

}