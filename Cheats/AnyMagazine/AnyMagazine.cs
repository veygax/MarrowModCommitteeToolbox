/*using Il2CppInterop.Runtime.InteropTypes;
using Il2CppSLZ.Marrow;
using Il2CppSystem.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Il2CppSLZ.Marrow.PuppetMasta.Muscle;
using HarmonyLib;
using BoneLib;

public static class AnyMagazine
{
    [HarmonyPatch(typeof(AmmoPlug), "OnPlugInsertComplete")]
    public static class AmmoPlugOnPlugInsertCompletePatch
    {
        private static Hand FindLocalHand(InteractableHost host)
        {
            Enumerator<Hand> enumerator = host._hands.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Hand current = enumerator.Current;
                if (((Object)current.manager).name == "[RigManager (Blank)]")
                {
                    return current;
                }
            }
            return null;
        }

        private static void Prefix(AmmoPlug __instance)
        {
            if (!Prefs.Enabled || (!Prefs.ApplyMagazine && !Prefs.ApplyCartridge))
            {
                return;
            }
            Hand lastHand = ((Plug)__instance).host.GetLastHand();
            if (!((Object)(object)lastHand != (Object)null) || (!lastHand.Controller.GetThumbStick() && Prefs.HoldThumbstick))
            {
                return;
            }
            AmmoSocket val = ((Il2CppObjectBase)((AlignPlug)__instance)._lastSocket).TryCast<AmmoSocket>();
            if ((Object)(object)val != (Object)null && (Object)(object)__instance.magazine != (Object)null && __instance.magazine.magazineState != null && (Object)(object)val.gun != (Object)null && (Object)(object)((Socket)val).host != (Object)null)
            {
                Hand val2 = FindLocalHand(((Socket)val).host);
                Gun gun = val.gun;
                InventoryAmmoReceiver ammoReceiver = gun._AmmoInventory.ammoReceiver;
                if ((Object)(object)val2 != (Object)null && (Object)(object)val2.slot != (Object)null)
                {
                    ammoReceiver.OnHandItemSlotRemoved(val2.slot);
                }
                if (Prefs.ApplyMagazine)
                {
                    gun.defaultMagazine = __instance.magazine.magazineState.magazineData;
                }
                if (Prefs.ApplyCartridge)
                {
                    gun.defaultCartridge = __instance.magazine.magazineState.cartridgeData;
                }
                if ((Object)(object)val2 != (Object)null && (Object)(object)val2.slot != (Object)null)
                {
                    ammoReceiver.OnHandItemSlot(val2.slot);
                }
            }
        }
    }

    [HarmonyPatch(typeof(AmmoPlug), "Awake")]
    public static class AmmoPlugProxyGripFix
    {
        private static void Prefix(AmmoPlug __instance)
        {
            if ((Object)(object)((AlignPlug)__instance).proxyGrip == (Object)null && (Object)(object)__instance.magazine.grip != (Object)null)
            {
                ((AlignPlug)__instance).proxyGrip = __instance.magazine.grip;
            }
        }
    }
}*/
