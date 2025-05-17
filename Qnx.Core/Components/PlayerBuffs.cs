using System.Collections;
using System.Collections.Generic;
using Qnx.Core.Attributes;
using Qnx.Core.Enums;
using Qnx.Core.Interfaces;
using Qnx.Core.Utils;
using SDG.Unturned;
using UnityEngine;
using Action = System.Action;

namespace Qnx.Core.Components;

public class PlayerBuffs : MonoBehaviour, IPlayerComponent
{
    [LoadAsset(EAssetType.EFFECT, 139)]
    private static EffectAsset _effectIgnite = null!;
    
    private QnxPlayer _qnx;
    
    public Dictionary<EBuff, int> BuffModifier = EnumDictionary.Create<EBuff, int>();
    public Dictionary<EBuff, float> BuffChance = EnumDictionary.Create<EBuff, float>();
    public Dictionary<EBuff, byte> BuffProtection = EnumDictionary.Create<EBuff, byte>();
    
    public float SpeedMult = 1f;
    public float JumpMult = 1f;
    public float GravityMult = 1f;

    public bool DisableMovementUpdate;
    
    private int _igniteTicks;
    private int _freezeTicks;
    private int _slowTicks;
    
    
    public void Initialize(QnxPlayer player)
    {
        _qnx = player;
    }

    public void UpdateMovementMultipliers()
    {
        if (DisableMovementUpdate)
            return;
        
        _qnx.Player.movement.sendPluginSpeedMultiplier(SpeedMult);
        _qnx.Player.movement.sendPluginJumpMultiplier(JumpMult);
        _qnx.Player.movement.sendPluginGravityMultiplier(GravityMult);
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

    public void Ignite(int duration)
    {
        if (BuffProtection[EBuff.IGNITE] != 0)
            return;
        
        if (_qnx.Player.stance.stance == EPlayerStance.SWIM)
            return;
        
        if (_igniteTicks > 0)
        {
            if (_igniteTicks >= duration) 
                return;
            
            _igniteTicks = duration;
            return;
        }

        _igniteTicks = duration;
        StartCoroutine(igniteTick());
    }

    private IEnumerator igniteTick()
    {
        while (_igniteTicks > 0)
        {
            if (_qnx.Player.stance.stance == EPlayerStance.SWIM)
            {
                _igniteTicks = 0;
                yield break;
            }
            EffectManager.triggerEffect(new TriggerEffectParameters(_effectIgnite)
            {
                position = _qnx.Player.transform.position
            });
            _qnx.Life.Damage(1);
            _igniteTicks--;
            yield return new WaitForSeconds(1f);
        }
    }
    
    public void Freeze(int duration)
    {
        if (BuffProtection[EBuff.FREEZE] != 0)
            return;
        
        if (_freezeTicks > 0)
        {
            if (_freezeTicks >= duration) 
                return;
            
            _freezeTicks = duration;
            return;
        }

        _freezeTicks = duration;
        
        DisableMovementUpdate = true;
        _qnx.Player.movement.sendPluginSpeedMultiplier(0.01f);
        _qnx.Player.movement.sendPluginJumpMultiplier(0.01f);
        _qnx.Player.movement.sendPluginGravityMultiplier(1.0f);
        
        StartCoroutine(freezeTick());
    }
    
    private IEnumerator freezeTick()
    {
        while (_freezeTicks > 0)
        {
            _freezeTicks--;
            yield return new WaitForSeconds(1f);
        }

        DisableMovementUpdate = false;
        UpdateMovementMultipliers();
    }

    public void Slow(int duration)
    {
        if (BuffProtection[EBuff.SLOW] != 0)
            return;
        
        if (_slowTicks > 0)
        {
            if (_slowTicks >= duration) 
                return;
            
            _slowTicks = duration;
            return;
        }

        _slowTicks = duration;
        
        DisableMovementUpdate = true;
        _qnx.Player.movement.sendPluginSpeedMultiplier(SpeedMult * 0.4f);
        _qnx.Player.movement.sendPluginJumpMultiplier(JumpMult);
        _qnx.Player.movement.sendPluginGravityMultiplier(GravityMult * 2.0f);
        StartCoroutine(slowTicks());
    }
    
    private IEnumerator slowTicks()
    {
        while (_slowTicks > 0)
        {
            _slowTicks--;
            yield return new WaitForSeconds(1f);
        }
 
        DisableMovementUpdate = false;
        UpdateMovementMultipliers();
    }

}