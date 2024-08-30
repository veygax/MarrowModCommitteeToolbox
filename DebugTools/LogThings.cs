using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using BoneLib;
using BoneLib.Notifications;
using Il2CppSLZ.Marrow;
using Il2CppSLZ.Marrow.Pool;
using MelonLoader;
using UnityEngine;

public static class LogThings
{
    public static void LogItemInLeftHand()
    {
        string errortext = "";
        try
        {
            if (BoneLib.Player.GetObjectInHand(BoneLib.Player.LeftHand) == null)
            { errortext = "Error: Nothing in left hand."; throw new Exception(); }
            if (BoneLib.Player.GetComponentInHand<Poolee>(BoneLib.Player.LeftHand).SpawnableCrate == null)
            { errortext = "Error: Object is not a spawnable, or is a prefab."; throw new Exception(); }

            string barcode = BoneLib.Player.GetComponentInHand<Poolee>(BoneLib.Player.LeftHand).SpawnableCrate.Barcode.ID;
            MelonLogger.Msg("Logged left hand contents: " + barcode);

        }
        catch (Exception)
        {
            Main.BoneMenuNotif(NotificationType.Error, errortext);
        }
    }
    public static void LogItemInRightHand()
    {
        string errortext = "";
        try
        {
            if (BoneLib.Player.GetObjectInHand(BoneLib.Player.RightHand) == null)
            { errortext = "Error: Nothing in right hand."; throw new Exception(); }
            if (BoneLib.Player.GetComponentInHand<Poolee>(BoneLib.Player.RightHand).SpawnableCrate == null)
            { errortext = "Error: Object is not a spawnable, or is a prefab."; throw new Exception(); }

            string barcode = BoneLib.Player.GetComponentInHand<Poolee>(BoneLib.Player.RightHand).SpawnableCrate.Barcode.ID;
            MelonLogger.Msg("Logged right hand contents: " + barcode);


        }
        catch (Exception)
        {
            Main.BoneMenuNotif(NotificationType.Error, errortext);
        }
    }
    public static void LogHeadTransform()
    {
        string errortext = "";
        try
        {
            if (BoneLib.Player.Head == null)
            { errortext = "Error: No head bruh."; throw new Exception(); }
            if (BoneLib.Player.Head.transform == null)
            { errortext = "Error: Can't get head transform :("; throw new Exception(); }
            if (BoneLib.Player.PhysicsRig.m_chest.gameObject.transform == null)
            { errortext = "Error: No chest bruh."; throw new Exception(); }
            if (BoneLib.Player.PhysicsRig.m_chest.transform == null)
            { errortext = "Error: Can't get chest transform :("; throw new Exception(); }

            // Log current head transform
            string headposition = BoneLib.Player.Head.transform.position.ToString();
            MelonLogger.Msg("Logged head position: " + headposition);

            string headrotation = BoneLib.Player.Head.transform.rotation.ToString();
            MelonLogger.Msg("Logged head rotation: " + headrotation);

            string headscale = BoneLib.Player.Head.transform.localScale.ToString();
            MelonLogger.Msg("Logged head scale: " + headscale);

            Vector3 referencePosition = BoneLib.Player.Head.transform.position;
            Vector3 referenceScale = BoneLib.Player.Head.transform.localScale;

            // Calculate and log offset
            Vector3 positionOffset = BoneLib.Player.PhysicsRig.m_chest.gameObject.transform.position - referencePosition;
            MelonLogger.Msg("Logged head and player position offset: " + positionOffset.ToString());

            Vector3 scaleOffset = BoneLib.Player.PhysicsRig.m_chest.gameObject.transform.localScale - referenceScale;
            MelonLogger.Msg("Logged head and player scale offset: " + positionOffset.ToString());
        }
        catch (Exception error)
        {
            Main.BoneMenuNotif(NotificationType.Error, errortext);
            MelonLogger.Msg("Exception occured: "+ error.ToString());
            return;
        }
    }

}