using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Reflection;
using Il2CppSLZ.Interaction;
using MelonLoader;
using HarmonyLib;
using UnityEngine.Events; 

public static class LevelButtons
{
    public static void ApplyPatches(MelonMod mod)
    {
        var harmony = mod.HarmonyInstance;

        var methodToPatch = typeof(ButtonToggle).GetMethod("Update", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        var patchMethod = typeof(LevelButtons).GetMethod(nameof(PatchButtonToggleUpdate), BindingFlags.Static | BindingFlags.Public);

        if (methodToPatch != null && patchMethod != null)
        {
            harmony.Patch(methodToPatch, new HarmonyMethod(patchMethod));
#if DEBUG
            MelonLogger.Msg("Patched ButtonToggle.Update method.");
#endif
        }
        else
        {
#if DEBUG
            MelonLogger.Error("Failed to patch ButtonToggle.Update method: method or patchMethod is null.");
#endif
        }
    }

    public static void RevertPatches(MelonMod mod)
    {
        var harmony = mod.HarmonyInstance;

        var methodToPatch = typeof(ButtonToggle).GetMethod("Update", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        if (methodToPatch != null)
        {
            harmony.Unpatch(methodToPatch, HarmonyPatchType.Prefix);
#if DEBUG
            MelonLogger.Msg("Unpatched ButtonToggle.Update method.");
#endif
        }
        else
        {
#if DEBUG
            MelonLogger.Error("Failed to unpatch ButtonToggle.Update method: method is null.");
#endif
        }
    }

    public static bool PatchButtonToggleUpdate(ButtonToggle __instance)
    {
        if (__instance != null)
        {
            if (__instance.onPress != null)
            {
#if DEBUG
                MelonLogger.Msg("Disabling ButtonToggle.onPress event.");
#endif
                __instance.onPress.RemoveAllListeners(); // Removes all listeners from the onPress event.
            }
        }

        return true;
    }
}
