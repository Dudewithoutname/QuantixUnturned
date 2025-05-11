using Qnx.Core.Components;
using Qnx.Core.Extensions;
using Qnx.Core.Utils;
using SDG.Unturned;
using Steamworks;

namespace Qnx.Core.Services;

public class BuffService : SingletonService<BuffService>
{

    public BuffService()
    {
        DamageTool.damagePlayerRequested += onDamage;
    }

    protected override void OnDispose()
    {
        DamageTool.damagePlayerRequested -= onDamage;
    }

    private void onDamage(ref DamagePlayerParameters parameters, ref bool shouldAllow)
    {
        if (parameters.killer == CSteamID.Nil || parameters.player == null)
            return;

        var attacker = PlayerTool.getPlayer(parameters.killer);
        if (attacker == null) 
            return;
        
        applyDebuffs(attacker.QnxComponent().Buffs, parameters.player.QnxComponent().Buffs);
    }

    private void applyDebuffs(PlayerBuffs attacker, PlayerBuffs target)
    {
        
    }
}