namespace FPFCReset
{
    using System.Reflection;
    using HarmonyLib;
    using IPA;
    using IPA.Config.Stores;
    using IPALogger = IPA.Logging.Logger;

    [Plugin(RuntimeOptions.SingleStartInit)]
    internal class Plugin
    {
        public static IPALogger Logger { get; set; }

        private const string HARMONYID = "com.aeroluna.BeatSaber.FPFCReset";
        private static readonly Harmony _harmonyInstance = new Harmony(HARMONYID);

        [Init]
        public void Init(IPALogger logger, IPA.Config.Config conf)
        {
            Logger = logger;
            Config.Instance = conf.Generated<Config>();
        }

        [OnEnable]
        public void OnEnable()
        {
            _harmonyInstance.PatchAll(Assembly.GetExecutingAssembly());
        }
    }
}
