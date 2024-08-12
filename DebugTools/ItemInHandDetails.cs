using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoneLib.Notifications;
using Il2CppSLZ.Marrow.Pool;
using MelonLoader;

public static class ItemInHandDetails
{
    public static void LeftHandItemDetails()
    {
        string errortext = "";
        try
        {
            if (BoneLib.Player.GetObjectInHand(BoneLib.Player.LeftHand) == null)
            { errortext = "Error: Nothing in left hand."; throw new Exception(); }
            if (BoneLib.Player.GetComponentInHand<Poolee>(BoneLib.Player.LeftHand).SpawnableCrate == null)
            { errortext = "Error: Object is not a spawnable, or is a prefab."; throw new Exception(); }

            string name = BoneLib.Player.GetComponentInHand<Poolee>(BoneLib.Player.LeftHand).SpawnableCrate.name;
            string description = BoneLib.Player.GetComponentInHand<Poolee>(BoneLib.Player.LeftHand).SpawnableCrate.Description;
            //string creator = BoneLib.Player.GetComponentInHand<Poolee>(BoneLib.Player.LeftHand).SpawnableCrate.title;

            MarrowModCommitteeToolbox.BoneMenuNotif(NotificationType.Information, name + "\n" + description);

        }
        catch (Exception)
        {
            MarrowModCommitteeToolbox.BoneMenuNotif(NotificationType.Error, errortext);
        }
    }

    public static void RightHandItemDetails()
    {
        string errortext = "";
        try
        {
            if (BoneLib.Player.GetObjectInHand(BoneLib.Player.RightHand) == null)
            { errortext = "Error: Nothing in right hand."; throw new Exception(); }
            if (BoneLib.Player.GetComponentInHand<Poolee>(BoneLib.Player.RightHand).SpawnableCrate == null)
            { errortext = "Error: Object is not a spawnable, or is a prefab."; throw new Exception(); }

            string name = BoneLib.Player.GetComponentInHand<Poolee>(BoneLib.Player.RightHand).SpawnableCrate.name;
            string description = BoneLib.Player.GetComponentInHand<Poolee>(BoneLib.Player.RightHand).SpawnableCrate.Description;
            //string creator = BoneLib.Player.GetComponentInHand<Poolee>(BoneLib.Player.LeftHand).SpawnableCrate.title;

            MarrowModCommitteeToolbox.BoneMenuNotif(NotificationType.Information, name + "\n" + description);

        }
        catch (Exception)
        {
            MarrowModCommitteeToolbox.BoneMenuNotif(NotificationType.Error, errortext);
        }
    }
}