using MelonLoader;
using System;
using UnityEngine;
using BoneLib.BoneMenu;
using Il2CppSLZ.Marrow;
using Unity.Baselib.LowLevel;

[assembly: MelonInfo(typeof(MarrowModCommitteeToolbox), "Marrow Mod Commitee Toolbox", "1.0.0", "VeygaX")]
[assembly: MelonGame("Stress Level Zero", "BONELAB")]

public class MarrowModCommitteeToolbox : MelonMod
{
    public static MelonPreferences_Category MelonPrefCategory { get; private set; }
    public static bool isNoBloodEnabled { get; private set; }
    public static MelonPreferences_Entry<bool> MelonPrefNoBloodEnabled { get; private set; }
    private static bool _preferencesSetup = false;
    public static Page menuPage { get; private set; }
    public static BoolElement noBloodEnabledMenu { get; private set; }

    public override void OnInitializeMelon()
    {
        MelonLogger.Msg("Initializing Marrow Mod Commitee Toolbox...");
        SetupMelonPrefs();
        CreateMenu();

        InitializeNoBlood();
    }

    public static void SetupMelonPrefs()
    {
        MelonPrefCategory = MelonPreferences.CreateCategory("Marrow Mod Commitee Toolbox");
        MelonPrefNoBloodEnabled = MelonPrefCategory.CreateEntry("isNoBloodEnabled", true);

        isNoBloodEnabled = MelonPrefNoBloodEnabled.Value;

        _preferencesSetup = true;
    }

    private void CreateMenu()
    {
        menuPage = Page.Root.CreatePage("Marrow Mod Commitee Toolbox", Color.white);
        noBloodEnabledMenu = menuPage.CreateBool("NoBlood", Color.red, isNoBloodEnabled, OnSetNoBloodEnabled);
    }

    private void OnSetNoBloodEnabled(bool value)
    {
        isNoBloodEnabled = value;
        MelonPrefNoBloodEnabled.Value = value;
        MelonPrefCategory.SaveToFile(false);
        InitializeNoBlood();
    }

    private void InitializeNoBlood()
    {
        try
        {
            if (isNoBloodEnabled)
            {
                NoBlood.ApplyPatches(this);
            }
            else
            {
                NoBlood.RevertPatches(this);
            }
        }
        catch (Exception ex)
        {
            MelonLogger.Error($"Error while patching methods for NoBlood: {ex.Message}");
        }
    }
}
