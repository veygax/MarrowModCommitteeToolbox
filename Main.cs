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
    private static bool _preferencesSetup = false;
    public static MelonPreferences_Entry<bool> MelonPrefNoBloodEnabled { get; private set; }
    public static MelonPreferences_Entry<bool> MelonPrefInfiniteAmmoEnabled { get; private set; }
    public static MelonPreferences_Entry<bool> MelonPrefImmortalityEnabled { get; private set; }
    public static MelonPreferences_Entry<bool> MelonPrefLevelButtonsDisabled { get; private set; }
    public static MelonPreferences_Entry<bool> MelonPrefHeadFixed { get; private set; }


    public static bool isNoBloodEnabled { get; private set; }
    public static bool isInfiniteAmmoEnabled { get; private set; }
    public static bool isImmortalityEnabled { get; private set; }
    public static bool isLevelButtonsDisabled { get; private set; }
    public static bool isHeadFixed { get ; private set; }

    public static Page menuPage { get; private set; }
    public static Page cheatsCategory { get; private set; }
    public static Page debugCategory { get; private set; }
    public static Page avatarScaleSubCategory { get; private set; }
    public static Page miscCategory { get; private set; }



    public static BoolElement noBloodEnabledMenu { get; private set; }
    public static FunctionElement spawnScenemakingToolsMenu { get; private set; }
    public static BoolElement disableLevelButtonsMenu {  get; private set; }
    public static BoolElement fixHeadMenu { get; private set; }


    public static BoolElement infiniteAmmoEnabledMenu { get; private set; }
    public static BoolElement immortalityEnabledMenu { get; private set; }

    public static FunctionElement logLeftHandMenu { get; private set; }
    public static FunctionElement logRightHandMenu { get; private set; }
    public static FunctionElement logHeadTransform { get; private set; }
    public static FunctionElement leftHandItemDetailsMenu { get; private set; }
    public static FunctionElement rightHandItemDetailsMenu { get; private set; }
    
    public static FloatElement setAvatarScaleMenu { get; private set; }
    public static FunctionElement applyAvatarScaleMenu { get; private set; }

    public override void OnInitializeMelon()
    {
        MelonLogger.Msg("Initializing Marrow Mod Commitee Toolbox...");
        SetupMelonPrefs();
        CreateMenu();

        InitializeNoBlood(false);
        InitializeInfiniteAmmo(false);
        InitializeImmortality(false);
        InitializeLevelButtonDisabler(false);
        InitializeHeadFixer(false);
    }

    public override void OnUpdate()
    {
        if (isHeadFixed)
        {
            FixHead.SetPosition();
        }
    }

    public static void SetupMelonPrefs()
    {
        MelonPrefCategory = MelonPreferences.CreateCategory("Marrow Mod Commitee");
        MelonPrefNoBloodEnabled = MelonPrefCategory.CreateEntry("isNoBloodEnabled", true);
        MelonPrefInfiniteAmmoEnabled = MelonPrefCategory.CreateEntry("isInfiniteAmmoEnabled", true);
        MelonPrefImmortalityEnabled = MelonPrefCategory.CreateEntry("isImmortalityEnabled", false);
        MelonPrefLevelButtonsDisabled = MelonPrefCategory.CreateEntry("isLevelButtonsDisabled", false);
        MelonPrefHeadFixed = MelonPrefCategory.CreateEntry("isHeadFixed", false);

        isNoBloodEnabled = MelonPrefNoBloodEnabled.Value;
        isInfiniteAmmoEnabled = MelonPrefInfiniteAmmoEnabled.Value;
        isImmortalityEnabled = MelonPrefImmortalityEnabled.Value;
        isLevelButtonsDisabled = MelonPrefLevelButtonsDisabled.Value;
        isHeadFixed = MelonPrefHeadFixed.Value;

        _preferencesSetup = true;
    }

    private void CreateMenu()
    {
        menuPage = Page.Root.CreatePage("Marrow Mod Commitee Toolbox", Color.white);
        cheatsCategory = menuPage.CreatePage("Cheats", Color.red);
        debugCategory = menuPage.CreatePage("Debug", Color.blue);
        avatarScaleSubCategory = cheatsCategory.CreatePage("Avatar Scale", Color.blue);
        miscCategory = menuPage.CreatePage("Misc", Color.white);
        
        noBloodEnabledMenu = miscCategory.CreateBool("NoBlood", Color.red, isNoBloodEnabled, OnSetNoBloodEnabled);
        spawnScenemakingToolsMenu = miscCategory.CreateFunction("Spawn Scenemaking Tools", Color.white, ScenemakingToolsSpawner.SpawnTools);
        disableLevelButtonsMenu = miscCategory.CreateBool("Disable Level Buttons", Color.white, isLevelButtonsDisabled, OnSetLevelButtonsDisabled);
        fixHeadMenu = miscCategory.CreateBool("Fix Head", Color.white, isHeadFixed, OnSetHeadFixed);


        infiniteAmmoEnabledMenu = cheatsCategory.CreateBool("Infinite Ammo", Color.yellow, isInfiniteAmmoEnabled, OnSetInfiniteAmmoEnabled);
        immortalityEnabledMenu = cheatsCategory.CreateBool("Immortality", Color.cyan, isImmortalityEnabled, OnSetImmortalityEnabled);


        logLeftHandMenu = debugCategory.CreateFunction("Log Left Hand Contents", Color.white, LogThings.LogItemInLeftHand);
        logRightHandMenu = debugCategory.CreateFunction("Log Right Hand Contents", Color.white, LogThings.LogItemInRightHand);
        logHeadTransform = debugCategory.CreateFunction("Log Head Transform", Color.white, LogThings.LogHeadTransform);
        leftHandItemDetailsMenu = debugCategory.CreateFunction("Left Hand Contents Details", Color.blue, ItemInHandDetails.LeftHandItemDetails);
        rightHandItemDetailsMenu = debugCategory.CreateFunction("Right Hand Contents Details", Color.blue, ItemInHandDetails.RightHandItemDetails);
        

        setAvatarScaleMenu = avatarScaleSubCategory.CreateFloat("Scale", Color.yellow, AvatarScale.scale, 0.1f, 0.1f, 10f, (Action<float>)delegate (float f)
        {
            AvatarScale.scale = f;
        });
        applyAvatarScaleMenu = avatarScaleSubCategory.CreateFunction("Apply", Color.green, (Action)delegate
        {
            AvatarScale.ScaleAvatar();
        });

    }

    public static void BoneMenuNotif(BoneLib.Notifications.NotificationType type, string content)
    {
        var notif = new BoneLib.Notifications.Notification
        {
            Title = "Marrow Mod Committee Toolbox",
            Message = content,
            Type = type,
            PopupLength = 3,
            ShowTitleOnPopup = true
        };
        BoneLib.Notifications.Notifier.Send(notif);

#if DEBUG
        MelonLogger.Msg("Sent a notification: " + content);
#endif

    }

    // Set variables.

    private void OnSetImmortalityEnabled(bool value)
    {
        isImmortalityEnabled = value;
        MelonPrefImmortalityEnabled.Value = value;
        MelonPrefCategory.SaveToFile(false);
        InitializeImmortality(true);
    }

    private void OnSetInfiniteAmmoEnabled(bool value)
    {
        isInfiniteAmmoEnabled = value;
        MelonPrefInfiniteAmmoEnabled.Value = value;
        MelonPrefCategory.SaveToFile(false);
        InitializeInfiniteAmmo(true);
    }

    private void OnSetNoBloodEnabled(bool value)
    {
        isNoBloodEnabled = value;
        MelonPrefNoBloodEnabled.Value = value;
        MelonPrefCategory.SaveToFile(false);
        InitializeNoBlood(true);
    }

    private void OnSetLevelButtonsDisabled(bool value)
    {
        isLevelButtonsDisabled = value;
        MelonPrefLevelButtonsDisabled.Value = value;
        MelonPrefCategory.SaveToFile(false);
        InitializeLevelButtonDisabler(true);
    }

    private void OnSetHeadFixed(bool value)
    {
        isHeadFixed = value;
        MelonPrefHeadFixed.Value = value;
        MelonPrefCategory.SaveToFile(false);
        InitializeHeadFixer(true);
    }

    // Initialize different tools

    private void InitializeImmortality(bool updated)
    {
        try
        {
            if (isImmortalityEnabled)
            {
                Immortality.ApplyPatches(this);
#if DEBUG
                MelonLogger.Msg("Applied Immortality Patches.");
#endif
            }
            else if (isImmortalityEnabled == false && updated == true)
            {
                Immortality.RevertPatches(this);
#if DEBUG
                MelonLogger.Msg("Reverted Immortality Patches.");
#endif
            }
        }
        catch (Exception ex)
        {
            MelonLogger.Error($"Error while patching methods for Immortality: {ex.Message}");
        }
    }

    private void InitializeNoBlood(bool updated)
    {
        try
        {
            if (isNoBloodEnabled)
            {
                NoBlood.ApplyPatches(this);
#if DEBUG
                MelonLogger.Msg("Applied NoBlood Patches.");
#endif
            }
            else if (isNoBloodEnabled == false && updated == true)
            {
                NoBlood.RevertPatches(this);
#if DEBUG
                MelonLogger.Msg("Reverted NoBlood Patches.");
#endif
            }
        }
        catch (Exception ex)
        {
            MelonLogger.Error($"Error while patching methods for NoBlood: {ex.Message}");
        }
    }

    private void InitializeInfiniteAmmo(bool updated)
    {
        try
        {
            if (isInfiniteAmmoEnabled)
            {
                InfiniteAmmo.ApplyPatches(this);
#if DEBUG
                MelonLogger.Msg("Applied InfiniteAmmo Patches.");
#endif
            }
            else if (isInfiniteAmmoEnabled == false && updated == true)
            {
                InfiniteAmmo.RevertPatches(this);
#if DEBUG
                MelonLogger.Msg("Reverted InfiniteAmmo Patches.");
#endif
            }
        }
        catch (Exception ex)
        {
            MelonLogger.Error($"Error while patching methods for InfiniteAmmo: {ex.Message}");
        }
    }

    private void InitializeLevelButtonDisabler(bool updated)
    {
        try
        {
            if (isLevelButtonsDisabled)
            {
                LevelButtons.ApplyPatches(this);
#if DEBUG
                MelonLogger.Msg("Disabled level buttons.");
#endif
            }
            else if (isLevelButtonsDisabled == false && updated == true)
            {
                LevelButtons.RevertPatches(this);
#if DEBUG
                MelonLogger.Msg("Enabled level buttons.");
#endif
            }
        }
        catch (Exception ex)
        {
            MelonLogger.Error($"Error while disabling/enabling level buttons: {ex.Message}");
        }
    }

    private void InitializeHeadFixer(bool updated)
    {
        try
        {
            if (isHeadFixed)
            {
                FixHead.SetPosition();
#if DEBUG
                MelonLogger.Msg("Fixed head.");
#endif
            }
            else if (isHeadFixed == false && updated == true)
            {
                FixHead.RevertPosition();
#if DEBUG
                MelonLogger.Msg("Broke your neck lol.");
#endif
            }
        }
        catch (Exception ex)
        {
            MelonLogger.Error($"Error while fixing head: {ex.Message}");
        }
    }


}
