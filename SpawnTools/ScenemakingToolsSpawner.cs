using MelonLoader;
using BoneLib;
using Il2CppSLZ.Bonelab;
using HarmonyLib;
using System.Reflection;
using Il2CppSLZ.Marrow.Warehouse;
using System;
using System.Collections.Generic;

public static class ScenemakingToolsSpawner
{
    private static CheatTool _cheatToolInstance;

    // This patch method captures the CheatTool instance during Start
    [HarmonyPatch(typeof(CheatTool))]
    [HarmonyPatch(nameof(CheatTool.Start))]
    private static class CheatToolStartPatch
    {
        private static void Postfix(CheatTool __instance)
        {
            // Store the instance for later use
            _cheatToolInstance = __instance;
            MelonLogger.Msg("CheatTool instance captured.");
        }
    }

    public static void SpawnTools()
    {
        if (_cheatToolInstance == null)
        {
            MelonLogger.Error("CheatTool instance is not initialized yet.");
            return;
        }

        string[] BarcodeStrArray =
        {
            "Puma.ScenemakingUtilities.Spawnable.Configurablelight",
            "Puma.ScenemakingUtilities.Spawnable.Configurablecamera",
            "Puma.ScenemakingUtilities.Spawnable.Constrainertoggleablelines",
            "Puma.ScenemakingUtilities.Spawnable.InvisibleSeat"
        };

        List<SpawnableCrateReference> newCrateList = new List<SpawnableCrateReference>();
        foreach (var crateCode in BarcodeStrArray)
        {
#if DEBUG
            MelonLogger.Msg("Adding Barcode " + crateCode + " to Array");
#endif
            newCrateList.Add(new SpawnableCrateReference
            {
                _barcode = new Barcode() { ID = crateCode }
            });
        }

        // Store the original crates
        var originalCrates = _cheatToolInstance.crates;

        try
        {
            // Replace crates with the new array
            _cheatToolInstance.crates = newCrateList.ToArray();

            MelonLogger.Msg("CheatTool.crates replaced with custom objects.");

            // Call the original method to spawn the debug objects
            _cheatToolInstance.SpawnDebugObjects();
        }
        finally
        {
            // Revert crates back to the original array
            _cheatToolInstance.crates = originalCrates;
            MelonLogger.Msg("CheatTool.crates reverted to the original objects.");
        }
    }
}
