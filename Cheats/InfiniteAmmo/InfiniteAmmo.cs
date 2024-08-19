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
            __instance.AddCartridge(__instance.lightAmmoGroup, 999999);
            __instance.AddCartridge(__instance.mediumAmmoGroup, 999999);
            __instance.AddCartridge(__instance.heavyAmmoGroup, 999999);

#if DEBUG
            MelonLogger.Msg("InfiniteAmmo applied: 999999 cartridges added to each ammo group.");
#endif
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
#if DEBUG
            MelonLogger.Msg("Patch applied to AmmoInventory.Awake method.");
#endif
        }
        else
        {
#if DEBUG
            MelonLogger.Error("Failed to apply patch: original or postfix method is null.");
#endif
        }
    }

    public static void RevertPatches(MelonMod mod)
    {
        var harmony = mod.HarmonyInstance;

        var original = typeof(AmmoInventory).GetMethod(nameof(AmmoInventory.Awake));

        if (original != null)
        {
            harmony.Unpatch(original, HarmonyPatchType.Postfix);
#if DEBUG
            MelonLogger.Msg("Patch removed from AmmoInventory.Awake method.");
#endif
        }
        else
        {
#if DEBUG
            MelonLogger.Error("Failed to remove patch: original method is null.");
#endif
        }
    }
}
