using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Il2CppSLZ.Interaction;

public static class LevelButtons
{
    public static void Disable()
    {
        var objectsWithKeyword = UnityEngine.Object.FindObjectsOfType<Transform>(true);
        foreach (Transform obj in objectsWithKeyword)
        {
            if (obj.name.Contains("FLOORS") || obj.name.Contains("LoadButtons") || obj.name.Contains("prop_bigButton") || obj.name.Contains("INTERACTION"))
            {
                for (int i = 0; i < obj.childCount; i++)
                {
                    Transform child = obj.GetChild(i);
                    ButtonToggle buttonToggle = child.GetComponent<ButtonToggle>();
                    if (buttonToggle != null)
                    {
                        if (!child.name.Contains("prop_bigButton_NEXTLEVEL"))
                        {
                            buttonToggle.enabled = false;
                        }
                    }
                }
            }
        }
    }

    public static void Enable()
    {
        var objectsWithKeyword = UnityEngine.Object.FindObjectsOfType<Transform>(true);
        foreach (Transform obj in objectsWithKeyword)
        {
            if (obj.name.Contains("FLOORS") || obj.name.Contains("LoadButtons") || obj.name.Contains("prop_bigButton") || obj.name.Contains("INTERACTION"))
            {
                for (int i = 0; i < obj.childCount; i++)
                {
                    Transform child = obj.GetChild(i);
                    ButtonToggle buttonToggle = child.GetComponent<ButtonToggle>();
                    if (buttonToggle != null)
                    {
                        buttonToggle.enabled = true;
                    }
                }
            }
        }
    }
}