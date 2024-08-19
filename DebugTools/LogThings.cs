using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoneLib;
using BoneLib.Notifications;
using Il2CppSLZ.Marrow.Pool;
using MelonLoader;

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
            MarrowModCommitteeToolbox.BoneMenuNotif(NotificationType.Error, errortext);
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
            MarrowModCommitteeToolbox.BoneMenuNotif(NotificationType.Error, errortext);
        }
    }
    public static void LogHeadTransform()
    {
        string errortext = "";
        try
        {
            if (BoneLib.Player.Head == null)
            { errortext = "Error: No head bruh."; throw new Exception(); }
            if (BoneLib.Player.Head.transform.position == null)
            { errortext = "Error: Can't get head position :("; throw new Exception(); }
            if (BoneLib.Player.Head.transform.rotation == null)
            { errortext = "Error: Can't get head rotation :("; throw new Exception(); }
            if (BoneLib.Player.Head.transform.localScale == null)
            { errortext = "Error: Can't get head scale :("; throw new Exception(); }
            if (BoneLib.Player.Head.transform == null)
            { errortext = "Error: Can't get head transform :("; throw new Exception(); }

            string headposition = BoneLib.Player.Head.transform.position.ToString();
            MelonLogger.Msg("Logged head position: " + headposition);

            string headrotation = BoneLib.Player.Head.transform.rotation.ToString();
            MelonLogger.Msg("Logged head rotation: " + headrotation);

            string headscale = BoneLib.Player.Head.transform.localScale.ToString();
            MelonLogger.Msg("Logged head scale: " + headscale);

            string headtransform = BoneLib.Player.Head.transform.ToString();
            MelonLogger.Msg("Logged head transform: " + headtransform);
        }
        catch (Exception)
        {
            MarrowModCommitteeToolbox.BoneMenuNotif(NotificationType.Error, errortext);
            return;
        }
    }
}