using UnityEngine;

public static class FixHead
{
    private static Vector3 originalPosition;

    public static void SetPosition()
    {
        originalPosition = BoneLib.Player.Head.transform.position;

        BoneLib.Player.Head.transform.position = new Vector3(1.00f, 1.00f, 1.00f);
    }

    public static void RevertPosition()
    {
        BoneLib.Player.Head.transform.position = originalPosition;
    }
}
