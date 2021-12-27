namespace FPFCReset
{
    using System;
    using System.Reflection;
    using HarmonyLib;
    using UnityEngine;

    [HarmonyPatch(typeof(FirstPersonFlyingController))]
    [HarmonyPatch("Start")]
    internal static class FirstPersonFlyingControllerStart
    {
        private static void Postfix(ref float ____cameraFov)
        {
            ____cameraFov = Config.Instance.FieldOfView;
        }
    }

    [HarmonyPatch(typeof(MouseLook))]
    [HarmonyPatch("LookRotation")]
    internal static class MouseLookLookRotation
    {
        private static void Prefix(Transform character, Transform camera, ref Quaternion ____characterTargetRot, ref Quaternion ____cameraTargetRot)
        {
            if (Input.GetKeyDown(Config.Instance.ResetKeyCode))
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

    internal static class SiraUtilFPFCPatch
    {
        private static readonly Type _simpleCameraController = Type.GetType("SiraUtil.Tools.FPFC.SimpleCameraController,SiraUtil", true);
        private static readonly Type _cameraState = Type.GetType("SiraUtil.Tools.FPFC.SimpleCameraController+CameraState,SiraUtil", true);

        private static readonly FieldInfo _yaw = _cameraState.GetField("yaw", AccessTools.allDeclared);
        private static readonly FieldInfo _pitch = _cameraState.GetField("pitch", AccessTools.allDeclared);
        private static readonly FieldInfo _roll = _cameraState.GetField("roll", AccessTools.allDeclared);
        private static readonly FieldInfo _x = _cameraState.GetField("x", AccessTools.allDeclared);
        private static readonly FieldInfo _y = _cameraState.GetField("y", AccessTools.allDeclared);
        private static readonly FieldInfo _z = _cameraState.GetField("z", AccessTools.allDeclared);

        private static readonly MethodInfo _updatePatchMethod = AccessTools.Method(typeof(SiraUtilFPFCPatch), nameof(UpdatePatch));
        private static readonly MethodInfo _updateMethod = AccessTools.Method(_simpleCameraController, "Update");

        internal static void ApplyPatches(Harmony harmony)
        {
            harmony.Patch(_updateMethod, new HarmonyMethod(_updatePatchMethod));
        }

        private static void UpdatePatch(object ____targetCameraState, object ____interpolatingCameraState)
        {
            if (Input.GetKeyDown(Config.Instance.ResetKeyCode))
            {
                float yaw = Config.Instance.RotX;
                float pitch = Config.Instance.RotY;
                float roll = Config.Instance.RotZ;
                float x = Config.Instance.PosX;
                float y = Config.Instance.PosY;
                float z = Config.Instance.PosZ;

                void UpdateAll(object obj)
                {
                    _yaw.SetValue(obj, yaw);
                    _pitch.SetValue(obj, pitch);
                    _roll.SetValue(obj, roll);
                    _x.SetValue(obj, x);
                    _y.SetValue(obj, y);
                    _z.SetValue(obj, z);
                }

                UpdateAll(____targetCameraState);
                UpdateAll(____interpolatingCameraState);
            }
        }
    }
}
