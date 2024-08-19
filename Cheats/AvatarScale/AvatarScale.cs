﻿using BoneLib;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Il2CppSLZ.Marrow.Warehouse;
using Il2CppSLZ.VRMK;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Playables;
using Il2CppSLZ.Bonelab;
using HarmonyLib;

public class AvatarScale
{
    public static float scale = 1f;
    private static BodyVitals _bodyVitalsInstance;

    [HarmonyPatch(typeof(BodyVitals))]
    [HarmonyPatch(nameof(BodyVitals.Start))]
    private static class BodyVitalsStartPatch
    {
        private static void Postfix(BodyVitals __instance)
        {
            // Store the instance for later use
            _bodyVitalsInstance = __instance;
#if DEBUG
            MelonLogger.Msg("BodyVitals instance captured.");
#endif
        }
    }

    public static void ScaleAvatar()
    {
        AvatarCrate crate;
        if (AssetWarehouse.Instance.TryGetCrate(Player.RigManager._avatarCrate._barcode, out crate))
        {
            System.Action<GameObject> action = delegate (GameObject obj)
            {
                GameObject gameObject = UnityEngine.Object.Instantiate(obj);
                Vector3 localScale = gameObject.transform.localScale;
                localScale.x *= scale;
                localScale.y *= scale;
                localScale.z *= scale;
                gameObject.transform.localScale = localScale;
                gameObject.transform.parent = ((Component)(object)Player.RigManager).transform;
                gameObject.transform.localPosition = Vector3.zero;
                Avatar componentInChildren = gameObject.GetComponentInChildren<Avatar>();
                foreach (SkinnedMeshRenderer item in (Il2CppArrayBase<SkinnedMeshRenderer>)(object)componentInChildren.hairMeshes)
                {
                    item.enabled = false;
                }
#if DEBUG
            MelonLogger.Msg("Changed scale to " + scale + "x");
#endif
                componentInChildren.PrecomputeAvatar();
                componentInChildren.RefreshBodyMeasurements();
                Player.RigManager.SwitchAvatar(componentInChildren);
                _bodyVitalsInstance.PROPEGATE();
            };
            ((CrateT<GameObject>)(object)crate).LoadAsset((Il2CppSystem.Action<GameObject>)action);
        }
        else
        {
#if DEBUG
            MelonLogger.Msg("Failed to find avatar crate.");
#endif
        }
        
    }
}
