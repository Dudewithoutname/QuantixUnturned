using System.Collections.Generic;
using Qnx.Core.Enums;
using Qnx.Core.Interfaces;
using Qnx.Core.Utils;
using UnityEngine;

namespace Qnx.Core.Components;

public class QnxPlayerBuffs : MonoBehaviour, IPlayerComponent
{
    private QnxPlayer _qnx;
    
    public Dictionary<EBuff, int> BuffModifier = EnumDictionary.Create<int>();
    public Dictionary<EBuff, float> BuffChance = EnumDictionary.Create<float>();
    public Dictionary<EBuff, byte> BuffProtection = EnumDictionary.Create<byte>();
    
    public void Initialize(QnxPlayer player)
    {
        _qnx = player;
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