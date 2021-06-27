namespace FPFCReset
{
    using HarmonyLib;
    using UnityEngine;

    [HarmonyPatch(typeof(FirstPersonFlyingController))]
    [HarmonyPatch("Start")]
    internal static class FirstPersonFlyingControllerStart
    {
        private static void Postfix(ref float ____cameraFov)
        {
            ____cameraFov = Config.Instance?.FieldOfView ?? ____cameraFov;
        }
    }

    [HarmonyPatch(typeof(MouseLook))]
    [HarmonyPatch("LookRotation")]
    internal static class MouseLookLookRotation
    {
        private static void Prefix(Transform character, Transform camera, ref Quaternion ____characterTargetRot, ref Quaternion ____cameraTargetRot)
        {
            if (Config.Instance != null && Input.GetKeyDown(Config.Instance.ResetKeyCode))
            {
                camera.GetComponent<Camera>().fieldOfView = Config.Instance.FieldOfView;

                Vector3 configPos = new Vector3(Config.Instance.PosX, Config.Instance.PosY, Config.Instance.PosZ);
                character.transform.position = configPos;

                Vector3 configRot = new Vector3(Config.Instance.RotX, Config.Instance.RotY, Config.Instance.RotZ);
                ____characterTargetRot = Quaternion.Euler(configRot);
                ____cameraTargetRot = Quaternion.Euler(configRot);
            }
        }
    }
}
