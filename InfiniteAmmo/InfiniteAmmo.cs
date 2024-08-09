using MelonLoader;
using HarmonyLib;
using System.Reflection;
using Il2CppSLZ.Marrow;
using UnityEngine;
using Object = UnityEngine.Object;

public static class InfiniteAmmo
{
    public static class AmmoInventory_Awake_Patch
    {
        public static void Awake(AmmoInventory __instance)
        {
            // Add 999 cartridges to each ammo group
            __instance.AddCartridge(__instance.lightAmmoGroup, 999);
            __instance.AddCartridge(__instance.mediumAmmoGroup, 999);
            __instance.AddCartridge(__instance.heavyAmmoGroup, 999);

            MelonLogger.Msg("InfiniteAmmo applied: 999 cartridges added to each ammo group.");
        }
    }

    public static void ApplyPatches(MelonMod mod)
    {
        var harmony = mod.HarmonyInstance;

        var original = typeof(AmmoInventory).GetMethod(nameof(AmmoInventory.Awake));
        var postfix = typeof(AmmoInventory_Awake_Patch).GetMethod(nameof(AmmoInventory_Awake_Patch.Awake));

        if (original != null && postfix != null)
        {
            harmony.Patch(original, postfix: new HarmonyMethod(postfix));
            MelonLogger.Msg("Patch applied to AmmoInventory.Awake method.");
        }
        else
        {
            MelonLogger.Error("Failed to apply patch: original or postfix method is null.");
        }
    }

    public static void RevertPatches(MelonMod mod)
    {
        var harmony = mod.HarmonyInstance;

        var original = typeof(AmmoInventory).GetMethod(nameof(AmmoInventory.Awake));

        if (original != null)
        {
            harmony.Unpatch(original, HarmonyPatchType.Postfix);
            MelonLogger.Msg("Patch removed from AmmoInventory.Awake method.");
        }
        else
        {
            MelonLogger.Error("Failed to remove patch: original method is null.");
        }
    }
}
