using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoneLib;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Il2CppSLZ.Marrow.Warehouse;
using Il2CppSLZ.VRMK;
using MelonLoader;
using UnityEngine;
using UnityEngine.Playables;
using Il2CppSLZ.Bonelab;
using HarmonyLib;
using BoneLib.Notifications;
using Il2CppSLZ.Marrow.Pool;

namespace MarrowModCommitteeToolbox.Cheats.Scaling;

public class ItemScale
{
    public static float leftHandItemScale = 1f;
    public static float rightHandItemScale = 1f;

    public static void ScaleItemInHand(GameObject handObject, float scaleFactor)
    {
        if (handObject != null)
        {
            GameObject gameObject = UnityEngine.Object.Instantiate(handObject);
            Vector3 localScale = gameObject.transform.localScale * scaleFactor;
            gameObject.transform.localScale = localScale;
            gameObject.transform.localPosition = Vector3.zero;

#if DEBUG
            MelonLogger.Msg("Changed hand item scale to " + scaleFactor + "x");
#endif
        }
        else
        {
            Main.BoneMenuNotif(NotificationType.Error, "No object found in hand.");
#if DEBUG
            MelonLogger.Msg("No object found in hand.");
#endif
        }
    }

    public static void ScaleItemInLeftHand()
    {
        ScaleItemInHand(BoneLib.Player.GetComponentInHand<Component>(BoneLib.Player.RightHand).gameObject, leftHandItemScale);
    }

    public static void ScaleItemInRightHand()
    {
        ScaleItemInHand(BoneLib.Player.GetComponentInHand<Component>(BoneLib.Player.RightHand).gameObject, rightHandItemScale);
    }



}
