using HarmonyLib;
using Qnx.Core.Services;
using SDG.Unturned;
using Steamworks;

namespace Qnx.Core.Patches;

[HarmonyPatch(typeof(PlayerLife), "doDamage")]
public class PlayerLifePatch
{
    [HarmonyPrefix]
    private static bool doDamage(PlayerLife __instance, ref byte amount, Vector3 newRagdoll, 
        EDeathCause newCause, ELimb newLimb, CSteamID newKiller, EPlayerKill kill, 
        bool trackKill, ERagdollEffect newRagdollEffect, bool canCauseBleeding)
    {
        amount = PlayerHealthService.Singleton.HandleDamage(__instance.player, amount, newRagdoll, out var result);
        
        return result;
    }
    
}