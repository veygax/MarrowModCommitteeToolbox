using HarmonyLib;
using MelonLoader;
using System;
using System.Reflection;
using Il2CppSLZ.Marrow;
using Il2CppSLZ.Marrow.Combat;
using Il2CppSLZ.Combat;

public static class NoBlood
{
    public static void ApplyPatches(MelonMod mod)
    {
        var harmony = mod.HarmonyInstance;

        harmony.Patch(
            typeof(VisualDamageReceiver).GetMethod("ReceiveAttack"),
            new HarmonyMethod(typeof(DontRunMethods).GetMethod("DontRunThis", BindingFlags.Static | BindingFlags.Public))
        );
        MelonLogger.Msg("Patched VisualDamageReceiver.ReceiveAttack method.");

        harmony.Patch(
            typeof(VisualDamageController).GetMethod("AddToHitArray"),
            new HarmonyMethod(typeof(DontRunMethods).GetMethod("DontRunThis", BindingFlags.Static | BindingFlags.Public))
        );
        MelonLogger.Msg("Patched VisualDamageController.AddToHitArray method.");

        harmony.Patch(
            typeof(VisualDamageController).GetMethod("AddToCutArray"),
            new HarmonyMethod(typeof(DontRunMethods).GetMethod("DontRunThis", BindingFlags.Static | BindingFlags.Public))
        );
        MelonLogger.Msg("Patched VisualDamageController.AddToCutArray method.");

        harmony.Patch(
            typeof(VisualDamageController).GetMethod("BleedOverTimer"),
            new HarmonyMethod(typeof(DontRunMethods).GetMethod("DontRunThis", BindingFlags.Static | BindingFlags.Public))
        );
        MelonLogger.Msg("Patched VisualDamageController.BleedOverTimer method.");

        harmony.Patch(
            typeof(VisualDamageController).GetMethod("collectSkins"),
            new HarmonyMethod(typeof(DontRunMethods).GetMethod("DontRunThis", BindingFlags.Static | BindingFlags.Public))
        );
        MelonLogger.Msg("Patched VisualDamageController.collectSkins method.");

        harmony.Patch(
            typeof(ImpactProperties).GetMethod("ReceiveAttack"),
            new HarmonyMethod(typeof(DontRunMethods).GetMethod("DontRunThis", BindingFlags.Static | BindingFlags.Public))
        );
        MelonLogger.Msg("Patched ImpactProperties.ReceiveAttack method.");
    }

    public static void RevertPatches(MelonMod mod)
    {
        var harmony = mod.HarmonyInstance;

        harmony.Unpatch(
            typeof(VisualDamageReceiver).GetMethod("ReceiveAttack"),
            HarmonyPatchType.All,
            harmony.Id
        );
        MelonLogger.Msg("Unpatched VisualDamageReceiver.ReceiveAttack method.");

        harmony.Unpatch(
            typeof(VisualDamageController).GetMethod("AddToHitArray"),
            HarmonyPatchType.All,
            harmony.Id
        );
        MelonLogger.Msg("Unpatched VisualDamageController.AddToHitArray method.");

        harmony.Unpatch(
            typeof(VisualDamageController).GetMethod("AddToCutArray"),
            HarmonyPatchType.All,
            harmony.Id
        );
        MelonLogger.Msg("Unpatched VisualDamageController.AddToCutArray method.");

        harmony.Unpatch(
            typeof(VisualDamageController).GetMethod("BleedOverTimer"),
            HarmonyPatchType.All,
            harmony.Id
        );
        MelonLogger.Msg("Unpatched VisualDamageController.BleedOverTimer method.");

        harmony.Unpatch(
            typeof(VisualDamageController).GetMethod("collectSkins"),
            HarmonyPatchType.All,
            harmony.Id
        );
        MelonLogger.Msg("Unpatched VisualDamageController.collectSkins method.");

        harmony.Unpatch(
            typeof(ImpactProperties).GetMethod("ReceiveAttack"),
            HarmonyPatchType.All,
            harmony.Id
        );
        MelonLogger.Msg("Unpatched ImpactProperties.ReceiveAttack method.");
    }

    public static class DontRunMethods
    {
        public static bool DontRunThis(MethodBase __originalMethod)
        {
            MelonLogger.Msg($"DontRunThis method called. Preventing execution of {__originalMethod.DeclaringType.Name}.{__originalMethod.Name}.");
            return false;
        }
    }
}
