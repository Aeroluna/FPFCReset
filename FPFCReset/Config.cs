namespace FPFCReset
{
    public class Config
    {
        public static Config Instance { get; set; }
        public float FieldOfView { get; set; } = 70f;
        public float PosX { get; set; } = 0;
        public float PosY { get; set; } = 1.7f;
        public float PosZ { get; set; } = 0;
        public float RotX { get; set; } = 0;
        public float RotY { get; set; } = 0;
        public float RotZ { get; set; } = 0;
    }
}
