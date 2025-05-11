using System.Collections;
using System.Collections.Generic;
using Qnx.Core.Enums;
using Qnx.Core.Interfaces;
using Qnx.Core.Utils;
using SDG.Unturned;
using UnityEngine;

namespace Qnx.Core.Components;

public class PlayerBuffs : MonoBehaviour, IPlayerComponent
{
    private QnxPlayer _qnx;
    
    public Dictionary<EBuff, int> BuffModifier = EnumDictionary.Create<EBuff, int>();
    public Dictionary<EBuff, float> BuffChance = EnumDictionary.Create<EBuff, float>();
    public Dictionary<EBuff, byte> BuffProtection = EnumDictionary.Create<EBuff, byte>();
    
    public float SpeedMult = 1f;
    public float JumpMult = 1f;
    public float GravityMult = 1f;
    
    public void Initialize(QnxPlayer player)
    {
        _qnx = player;
    }

    public void UpdateMovementMultipliers()
    {
        _qnx.Player.movement.sendPluginSpeedMultiplier(SpeedMult);
        _qnx.Player.movement.sendPluginJumpMultiplier(JumpMult);
        _qnx.Player.movement.sendPluginSpeedMultiplier(GravityMult);
    }
    
    
    public void AddBuff(EBuff buff, float chance, int value = 0)
    {
        BuffModifier[buff] += value;
        BuffChance[buff] += chance;
    }
    
    public void RemoveBuff(EBuff buff, float chance, int value = 0)
    {
        BuffModifier[buff] -= value;
        BuffChance[buff] -= chance;
        
        if (BuffModifier[buff] < 0.001) 
            BuffChance[buff] = 0;
    }

    public void ResetBuffs()
    {
        foreach (var buff in BuffModifier)
        {
            BuffModifier[buff.Key] = 0;
            BuffChance[buff.Key] = 0;
            BuffProtection[buff.Key] = 0;
        }
    }

}