namespace FPFCReset
{
    using System;
    using UnityEngine;

    public class Config
    {
        public static Config Instance { get; set; } = null!;

        public string ResetKey
        {
            get => ResetKeyCode.ToString();

            set
            {
                try
                {
                    ResetKeyCode = (KeyCode)Enum.Parse(typeof(KeyCode), value, false);
                }
                catch (Exception e)
                {
                    Plugin.Logger?.Error($"Failed to save \"{value}\", resetting to default value of \"Backspace\"");
                    Plugin.Logger?.Error(e);
                }
            }
        }

        public float FieldOfView { get; set; } = 70f;

        public float PosX { get; set; } = 0;

        public float PosY { get; set; } = 1.7f;

        public float PosZ { get; set; } = 0;

        public float RotX { get; set; } = 0;

        public float RotY { get; set; } = 0;

        public float RotZ { get; set; } = 0;

        internal KeyCode ResetKeyCode { get; private set; } = KeyCode.Backspace;
    }
}
