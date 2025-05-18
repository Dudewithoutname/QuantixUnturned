using HarmonyLib;
using Qnx.Unturned.Extensions;
using Qnx.Unturned.Players;
using SDG.Unturned;
using Steamworks;

namespace Qnx.Unturned.Patches;

[HarmonyPatch]
public class PlayerLifePatch
{
    [HarmonyPrefix]
    [HarmonyPatch(typeof(PlayerLife), "doDamage")]
    private static bool doDamage(PlayerLife __instance, ref byte amount, Vector3 newRagdoll, 
        EDeathCause newCause, ELimb newLimb, CSteamID newKiller, EPlayerKill kill, 
        bool trackKill, ERagdollEffect newRagdollEffect, bool canCauseBleeding)
    {
        amount = PlayerHealthService.Instance!.HandleDamage(__instance.player, amount, newRagdoll, out var result);
        
        return result;
    }

    [HarmonyPatch(typeof(PlayerLife), "askHeal")]
    [HarmonyPrefix]
    private static bool askHeal(PlayerLife __instance, ref byte amount, bool healBleeding, bool healBroken)
    {
        if (amount == 0)
            return false;

        amount = PlayerHealthService.Instance!.HandleHeal(__instance, amount, healBleeding, healBroken, out var allow);
        return allow;
    }
    
    [HarmonyPatch(typeof(PlayerLife), nameof(PlayerLife.ReceiveSuicideRequest))]
    [HarmonyPrefix]
    private static void receiveSuicideRequest(PlayerLife __instance)
    {
        __instance.player.QnxComponent().Life.Health = 99;
    }

}