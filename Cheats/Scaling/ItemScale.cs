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

namespace MarrowModCommitteeToolbox.Cheats.Scaling;

public class ItemScale
{
    public static float leftHandItemScale = 1f;
    public static float rightHandItemScale = 1f;

    public static void ScaleItemInLeftHand()
    {
        GameObject obj = BoneLib.Player.GetObjectInHand(BoneLib.Player.RightHand).gameObject;

        if (obj != null)
        {
            GameObject gameObject = UnityEngine.Object.Instantiate(obj);
            Vector3 localScale = gameObject.transform.localScale;
            localScale.x *= leftHandItemScale;
            localScale.y *= leftHandItemScale;
            localScale.z *= leftHandItemScale;
            gameObject.transform.localScale = localScale;
            gameObject.transform.parent = BoneLib.Player.GetObjectInHand(BoneLib.Player.LeftHand).transform;
            gameObject.transform.localPosition = Vector3.zero;

#if DEBUG
            MelonLogger.Msg("Changed left hand item scale to " + leftHandItemScale + "x");
#endif
        }
        else
        {
            Main.BoneMenuNotif(NotificationType.Error, "No object found in left hand.");
#if DEBUG
            MelonLogger.Msg("No object found in left hand.");
#endif
        }
    }

    public static void ScaleItemInRightHand()
    {
        GameObject obj = BoneLib.Player.GetObjectInHand(BoneLib.Player.RightHand).gameObject;

        if (obj != null)
        {
            GameObject gameObject = UnityEngine.Object.Instantiate(obj);
            Vector3 localScale = gameObject.transform.localScale;
            localScale.x *= rightHandItemScale;
            localScale.y *= rightHandItemScale;
            localScale.z *= rightHandItemScale;
            gameObject.transform.localScale = localScale;
            gameObject.transform.parent = BoneLib.Player.GetObjectInHand(BoneLib.Player.RightHand).transform;
            gameObject.transform.localPosition = Vector3.zero;

#if DEBUG
            MelonLogger.Msg("Changed right hand item scale to " + rightHandItemScale + "x");
#endif
        }
        else
        {
            Main.BoneMenuNotif(NotificationType.Error, "No object found in right hand.");
#if DEBUG
            MelonLogger.Msg("No object found in right hand.");
#endif
        }
    }

    
}
