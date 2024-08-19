using HarmonyLib;
using MelonLoader;
using Il2CppSLZ.Marrow.Combat;
using Il2CppSLZ.Combat;
using Il2CppSLZ.Marrow;
using System.Reflection;


public static class NoBlood
{
    public static void ApplyPatches(MelonMod mod)
    {
        var harmony = mod.HarmonyInstance;

        var method1 = typeof(VisualDamageReceiver).GetMethod("ReceiveAttack");
        var method2 = typeof(ImpactProperties).GetMethod("ReceiveAttack");
        var patchMethod = typeof(NoBlood).GetMethod(nameof(DontRunThis), BindingFlags.Static | BindingFlags.Public);

        if (method1 != null && patchMethod != null)
        {
            harmony.Patch(method1, new HarmonyMethod(patchMethod));
#if DEBUG
            MelonLogger.Msg("Patched VisualDamageReceiver.ReceiveAttack method.");
#endif
        }
        else
        {
#if DEBUG
            MelonLogger.Error("Failed to patch VisualDamageReceiver.ReceiveAttack method: method or patchMethod is null.");
#endif
        }

        if (method2 != null && patchMethod != null)
        {
            harmony.Patch(method2, new HarmonyMethod(patchMethod));
#if DEBUG
            MelonLogger.Msg("Patched ImpactProperties.ReceiveAttack method.");
#endif
        }
        else
        {
#if DEBUG
            MelonLogger.Error("Failed to patch ImpactProperties.ReceiveAttack method: method or patchMethod is null.");
#endif
        }
    }

    public static void RevertPatches(MelonMod mod)
    {
        var harmony = mod.HarmonyInstance;

        var method1 = typeof(VisualDamageReceiver).GetMethod("ReceiveAttack");
        var method2 = typeof(ImpactProperties).GetMethod("ReceiveAttack");

        if (method1 != null)
        {
            harmony.Unpatch(method1, HarmonyPatchType.Prefix);
#if DEBUG
            MelonLogger.Msg("Unpatched VisualDamageReceiver.ReceiveAttack method.");
#endif
        }
        else
        {
#if DEBUG
            MelonLogger.Error("Failed to unpatch VisualDamageReceiver.ReceiveAttack method: method is null.");
#endif
        }

        if (method2 != null)
        {
            harmony.Unpatch(method2, HarmonyPatchType.Prefix);
#if DEBUG
            MelonLogger.Msg("Unpatched ImpactProperties.ReceiveAttack method.");
#endif
        }
        else
        {
#if DEBUG
            MelonLogger.Error("Failed to unpatch ImpactProperties.ReceiveAttack method: method is null.");
#endif
        }
    }

    public static bool DontRunThis(MethodBase __originalMethod)
    {
#if DEBUG
        MelonLogger.Msg($"DontRunThis method called. Preventing execution of {__originalMethod.DeclaringType.Name}.{__originalMethod.Name}.");
#endif
        return false;
    }
}