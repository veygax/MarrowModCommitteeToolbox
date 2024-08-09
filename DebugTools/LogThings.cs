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
}