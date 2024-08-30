using UnityEngine;

public static class FixHead
{
    private static Vector3 originalPositionOffset;
    private static Vector3 originalScaleOffset;

    public static void SetOffset()
    {
        originalScaleOffset = BoneLib.Player.Head.transform.localScale - BoneLib.Player.PhysicsRig.m_chest.gameObject.transform.localScale;

        // Set the desired offsets (1.00, 1.00, 1.00)
        BoneLib.Player.Head.transform.localScale = BoneLib.Player.PhysicsRig.m_chest.gameObject.transform.localScale + new Vector3(1.00f, 1.00f, 1.00f);
    }

    public static void RevertOffset()
    {
        // Revert the head's position and scale to the original offsets
        BoneLib.Player.Head.transform.localScale = BoneLib.Player.PhysicsRig.m_chest.gameObject.transform.localScale + originalScaleOffset;
    }
}
