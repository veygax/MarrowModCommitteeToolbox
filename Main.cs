using HarmonyLib;
using MelonLoader;
using System;
using System.Reflection;
using Il2CppSLZ.Marrow;
using Il2CppSLZ.Marrow.Combat;
using Il2CppSLZ.Combat;

[assembly: MelonInfo(typeof(MarrowModCommitteeToolbox), "Marrow Mod Commitee Toolbox", "1.0.0", "VeygaX")]
[assembly: MelonGame("Stress Level Zero", "BONELAB")]

public class MarrowModCommitteeToolbox : MelonMod
{
    public override void OnInitializeMelon()
    {
        MelonLogger.Msg("Initializing Marrow Mod Commitee Toolbox...");

        try
        {
            HarmonyInstance.Patch(
                typeof(VisualDamageReceiver).GetMethod("ReceiveAttack"),
                new HarmonyMethod(typeof(MarrowModCommitteeToolbox).GetMethod("DontRunThis"))
            );
            MelonLogger.Msg("Patched VisualDamageReceiver.ReceiveAttack method.");

            HarmonyInstance.Patch(
                typeof(VisualDamageController).GetMethod("AddToHitArray"),
                new HarmonyMethod(typeof(MarrowModCommitteeToolbox).GetMethod("DontRunThis"))
            );
            MelonLogger.Msg("Patched VisualDamageController.AddToHitArray method.");

            HarmonyInstance.Patch(
                typeof(VisualDamageController).GetMethod("AddToCutArray"),
                new HarmonyMethod(typeof(MarrowModCommitteeToolbox).GetMethod("DontRunThis"))
            );
            MelonLogger.Msg("Patched VisualDamageController.AddToCutArray method.");

            HarmonyInstance.Patch(
                typeof(VisualDamageController).GetMethod("BleedOverTimer"),
                new HarmonyMethod(typeof(MarrowModCommitteeToolbox).GetMethod("DontRunThis"))
            );
            MelonLogger.Msg("Patched VisualDamageController.BleedOverTimer method.");

            HarmonyInstance.Patch(
                typeof(VisualDamageController).GetMethod("collectSkins"),
                new HarmonyMethod(typeof(MarrowModCommitteeToolbox).GetMethod("DontRunThis"))
            );
            MelonLogger.Msg("Patched VisualDamageController.collectSkins method.");

            HarmonyInstance.Patch(
                typeof(ImpactProperties).GetMethod("ReceiveAttack"),
                new HarmonyMethod(typeof(MarrowModCommitteeToolbox).GetMethod("DontRunThis"))
            );
            MelonLogger.Msg("Patched ImpactProperties.ReceiveAttack method.");
        }
        catch (Exception ex)
        {
            MelonLogger.Error($"Error while patching methods: {ex.Message}");
        }
    }

    public static bool DontRunThis(MethodBase __originalMethod)
    {
        MelonLogger.Msg($"DontRunThis method called. Preventing execution of {__originalMethod.DeclaringType.Name}.{__originalMethod.Name}.");
        return false;
    }
}
