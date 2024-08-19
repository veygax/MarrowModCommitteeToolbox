using MelonLoader;
using HarmonyLib;
using Il2CppSLZ.Marrow;
using System.Reflection;

public static class Immortality
{
    public static void ApplyPatches(MelonMod mod)
    {
        var harmony = mod.HarmonyInstance;

        var method1 = typeof(PlayerDamageReceiver).GetMethod("ReceiveAttack");
        var patchMethod = typeof(Immortality).GetMethod(nameof(DontRunThis), BindingFlags.Static | BindingFlags.Public);

        if (method1 != null && patchMethod != null)
        {
            harmony.Patch(method1, new HarmonyMethod(patchMethod));
#if DEBUG
            MelonLogger.Msg("Patched PlayerDamageReceiver.ReceiveAttack method.");
#endif
        }
        else
        {
#if DEBUG
            MelonLogger.Error("Failed to patch PlayerDamageReceiver.ReceiveAttack method: method or patchMethod is null.");
#endif
        }
    }

    public static void RevertPatches(MelonMod mod)
    {
        var harmony = mod.HarmonyInstance;

        var method1 = typeof(PlayerDamageReceiver).GetMethod("ReceiveAttack");

        if (method1 != null)
        {
            harmony.Unpatch(method1, HarmonyPatchType.Prefix);
#if DEBUG
            MelonLogger.Msg("Unpatched PlayerDamageReceiver.ReceiveAttack method.");
#endif
        }
        else
        {
#if DEBUG
            MelonLogger.Error("Failed to unpatch PlayerDamageReceiver.ReceiveAttack method: method is null.");
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

