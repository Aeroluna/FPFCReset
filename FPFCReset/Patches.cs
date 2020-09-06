namespace FPFCReset
{
    using HarmonyLib;
    using UnityEngine;

    [HarmonyPatch(typeof(FirstPersonFlyingController))]
    [HarmonyPatch("Start")]
    internal static class FirstPersonFlyingControllerStart
    {
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
        private static void Postfix(ref float ____cameraFov)
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter
        {
            ____cameraFov = Config.Instance.FieldOfView;
        }
    }

    [HarmonyPatch(typeof(MouseLook))]
    [HarmonyPatch("LookRotation")]
    internal static class MouseLookLookRotation
    {
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
        private static void Prefix(Transform character, Transform camera, ref Quaternion ____characterTargetRot, ref Quaternion ____cameraTargetRot)
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter
        {
            if (Input.GetKeyDown(KeyCode.Backspace))
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
