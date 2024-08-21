using HarmonyLib;
using Il2CppSLZ.Marrow;
using System.Reflection;
using MelonLoader;
using Il2CppSLZ.Marrow.PuppetMasta;
using Il2CppSLZ.Bonelab;

namespace MarrowModCommitteeToolbox.Cheats;
public static class AnyMagazine
{
    public static void ApplyPatches(MelonMod mod)
    {
        var harmony = mod.HarmonyInstance;

        var onPlugInsertCompleteMethod = typeof(AmmoPlug).GetMethod("OnPlugInsertComplete", BindingFlags.Instance | BindingFlags.NonPublic);
        var awakeMethod = typeof(AmmoPlug).GetMethod("Awake", BindingFlags.Instance | BindingFlags.NonPublic);

        var onPlugInsertCompletePrefix = typeof(AnyMagazine).GetMethod(nameof(OnPlugInsertCompletePatchPrefix), BindingFlags.Static | BindingFlags.NonPublic);
        var awakePrefix = typeof(AnyMagazine).GetMethod(nameof(AmmoPlugProxyGripFixPrefix), BindingFlags.Static | BindingFlags.NonPublic);

        if (onPlugInsertCompleteMethod != null && onPlugInsertCompletePrefix != null)
        {
            harmony.Patch(onPlugInsertCompleteMethod, new HarmonyMethod(onPlugInsertCompletePrefix));
#if DEBUG
            MelonLogger.Msg("Patched AmmoPlug.OnPlugInsertComplete method.");
#endif
        }
        else
        {
#if DEBUG
            MelonLogger.Error("Failed to patch AmmoPlug.OnPlugInsertComplete method: method or patchMethod is null.");
#endif
        }

        if (awakeMethod != null && awakePrefix != null)
        {
            harmony.Patch(awakeMethod, new HarmonyMethod(awakePrefix));
#if DEBUG
            MelonLogger.Msg("Patched AmmoPlug.Awake method.");
#endif
        }
        else
        {
#if DEBUG
            MelonLogger.Error("Failed to patch AmmoPlug.Awake method: method or patchMethod is null.");
#endif
        }
    }

    public static void RevertPatches(MelonMod mod)
    {
        var harmony = mod.HarmonyInstance;

        var onPlugInsertCompleteMethod = typeof(AmmoPlug).GetMethod("OnPlugInsertComplete", BindingFlags.Instance | BindingFlags.NonPublic);
        var awakeMethod = typeof(AmmoPlug).GetMethod("Awake", BindingFlags.Instance | BindingFlags.NonPublic);

        if (onPlugInsertCompleteMethod != null)
        {
            harmony.Unpatch(onPlugInsertCompleteMethod, HarmonyPatchType.Prefix);
#if DEBUG
            MelonLogger.Msg("Unpatched AmmoPlug.OnPlugInsertComplete method.");
#endif
        }
        else
        {
#if DEBUG
            MelonLogger.Error("Failed to unpatch AmmoPlug.OnPlugInsertComplete method: method is null.");
#endif
        }

        if (awakeMethod != null)
        {
            harmony.Unpatch(awakeMethod, HarmonyPatchType.Prefix);
#if DEBUG
            MelonLogger.Msg("Unpatched AmmoPlug.Awake method.");
#endif
        }
        else
        {
#if DEBUG
            MelonLogger.Error("Failed to unpatch AmmoPlug.Awake method: method is null.");
#endif
        }
    }

    private static Hand FindLocalHand(InteractableHost host)
    {
        foreach (Hand current in host._hands)
        {
            if (current.manager.name == "[RigManager (Blank)]")
            {
                return current;
            }
        }
        return null;
    }

    private static void OnPlugInsertCompletePatchPrefix(AmmoPlug __instance)
    {
        Hand lastHand = __instance.host.GetLastHand();
        AmmoSocket val = __instance._lastSocket.TryCast<AmmoSocket>();
        if (val != null && __instance.magazine != null && __instance.magazine.magazineState != null && val.gun != null && val.host != null)
        {
            Hand val2 = FindLocalHand(((Socket)val).host);
            Gun gun = val.gun;
            InventoryAmmoReceiver ammoReceiver = PlayerRefs.Instance.PlayerInvAmmoReceiver;
            if (val2 != null && val2.slot != null)
            {
                ammoReceiver.OnHandItemSlotRemoved(val2.slot);
            }
            gun.defaultMagazine = __instance.magazine.magazineState.magazineData;
            gun.defaultCartridge = __instance.magazine.magazineState.cartridgeData;
            if (val2 != null && val2.slot != null)
            {
                ammoReceiver.OnHandItemSlot(val2.slot);
            }
        }
    }

    private static void AmmoPlugProxyGripFixPrefix(AmmoPlug __instance)
    {
        if (__instance.proxyGrip == null && __instance.magazine.grip != null)
        {
            __instance.proxyGrip = __instance.magazine.grip;
        }
    }
}
