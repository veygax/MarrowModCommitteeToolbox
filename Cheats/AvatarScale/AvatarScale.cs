using BoneLib;
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
            MelonLogger.Msg("BodyVitals instance captured.");
        }
    }

    public static void ScaleAvatar()
    {
        AvatarCrateReference avatarCrateRef = (AvatarCrateReference)Player.RigManager._avatarCrate;
        string barcode = avatarCrateRef.Barcode.ID;
        Action<GameObject> action = delegate (GameObject obj)
        {
            GameObject val = UnityEngine.Object.Instantiate<GameObject>(obj);
            Vector3 localScale = val.transform.localScale;
            localScale.x *= scale;
            localScale.y *= scale;
            localScale.z *= scale;
            val.transform.localScale = localScale;
            val.transform.parent = ((Component)Player.RigManager).transform;
            val.transform.localPosition = Vector3.zero;
            Avatar componentInChildren = val.GetComponentInChildren<Avatar>();
            foreach (SkinnedMeshRenderer item in (Il2CppArrayBase<SkinnedMeshRenderer>)(object)componentInChildren.hairMeshes)
            {
                ((Renderer)item).enabled = false;
            }
            MelonLogger.Msg("Changed scale to " + scale + "x");
            componentInChildren.PrecomputeAvatar();
            componentInChildren.RefreshBodyMeasurements();
            Player.RigManager.SwitchAvatar(componentInChildren);
            _bodyVitalsInstance.PROPEGATE();
            
        };

    }
}
