using HarmonyLib;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Il2CppSLZ.Marrow;
using Il2CppSLZ.Marrow.Combat;
using static MelonLoader.MelonLaunchOptions;
using Il2CppSLZ.Combat;
using UnityEngine;

[assembly: MelonInfo(typeof(NoBlood), "Marrow Mod Commitee Toolbox", "1.0.0", "VeygaX")]
[assembly: MelonGame("Stress Level Zero", "BONELAB")]

public class NoBlood : MelonMod
{
    public override void OnInitializeMelon()
    {
        MelonLogger.Msg("Initializing No Blood Mod...");

        try
        {
            HarmonyInstance.Patch(
                typeof(VisualDamageReceiver).GetMethod("ReceiveAttack"),
                new HarmonyMethod(typeof(NoBlood).GetMethod("DontRunThis"))
            );
            MelonLogger.Msg("Patched VisualDamageReceiver.ReceiveAttack method.");

            HarmonyInstance.Patch(
                typeof(VisualDamageController).GetMethod("AddToHitArray"),
                new HarmonyMethod(typeof(NoBlood).GetMethod("DontRunThis"))
            );
            MelonLogger.Msg("Patched VisualDamageController.AddToHitArray method.");

            HarmonyInstance.Patch(
                typeof(VisualDamageController).GetMethod("AddToCutArray"),
                new HarmonyMethod(typeof(NoBlood).GetMethod("DontRunThis"))
            );
            MelonLogger.Msg("Patched VisualDamageController.AddToCutArray method.");

            HarmonyInstance.Patch(
                typeof(VisualDamageController).GetMethod("BleedOverTimer"),
                new HarmonyMethod(typeof(NoBlood).GetMethod("DontRunThis"))
            );
            MelonLogger.Msg("Patched VisualDamageController.BleedOverTimer method.");

            HarmonyInstance.Patch(
                typeof(VisualDamageController).GetMethod("collectSkins"),
                new HarmonyMethod(typeof(NoBlood).GetMethod("DontRunThis"))
            );
            MelonLogger.Msg("Patched VisualDamageController.collectSkins method.");

            HarmonyInstance.Patch(
                typeof(ImpactProperties).GetMethod("ReceiveAttack"),
                new HarmonyMethod(typeof(NoBlood).GetMethod("DontRunThis"))
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
