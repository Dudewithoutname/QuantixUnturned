using System.Collections.Generic;
using Qnx.Core.Enums;
using Qnx.Core.Interfaces;
using Qnx.Core.Utils;
using UnityEngine;

namespace Qnx.Core.Components;

public class QnxPlayerBuffs : MonoBehaviour, IPlayerComponent
{
    private QnxPlayer _qnx;
    
    public Dictionary<EBuff, int> BuffValue = EnumDictionary.Create<int>();
    public Dictionary<EBuff, float> BuffChance = EnumDictionary.Create<float>();
    
    
    public void Initialize(QnxPlayer player)
    {
        
    }
}