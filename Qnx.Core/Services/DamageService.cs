using Qnx.Core.Extensions;
using Qnx.Core.Utils;
using SDG.Unturned;
using UnityEngine;

namespace Qnx.Core.Services;

public class DamageService : SingletonService<DamageService>
{
    public DamageService()
    {
        DamageTool.damagePlayerRequested += onDamageRequest;
    }
    protected override void OnDispose()
    {
    }

    private void onDamageRequest(ref DamagePlayerParameters parameters, ref bool shouldAllow)
    {
        var dmg = calculateDamage(parameters);
        var qnx = parameters.player.QnxComponent();

        shouldAllow = qnx.Life.Health - dmg < 100;
        qnx.Life.Health -= (ushort)dmg;
    } 

    private int calculateDamage(DamagePlayerParameters parameters)
    {
        if (parameters.respectArmor)
        {
            parameters.times *= DamageTool.getPlayerArmor(parameters.limb, parameters.player);
        }
        
        if (parameters.applyGlobalArmorMultiplier)
        {
            parameters.times *= Provider.modeConfigData.Players.Armor_Multiplier;
        }
        
        var total = Mathf.FloorToInt(parameters.damage * parameters.times);
        
        return Mathf.Min(total, 255);
    }
}