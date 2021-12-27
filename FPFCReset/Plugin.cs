namespace FPFCReset
{
    using System.Linq;
    using System.Reflection;
    using HarmonyLib;
    using IPA;
    using IPA.Config.Stores;
    using IPALogger = IPA.Logging.Logger;

    [Plugin(RuntimeOptions.SingleStartInit)]
    internal class Plugin
    {
        private const string HARMONYID = "com.aeroluna.BeatSaber.FPFCReset";
        private static readonly Harmony _harmonyInstance = new Harmony(HARMONYID);

        public static IPALogger Logger { get; set; } = null!;

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

            if (IPA.Loader.PluginManager.EnabledPlugins.Any(x => x.Id == "SiraUtil"))
            {
                Logger.Info("SiraUtil detected, patching...");
                SiraUtilFPFCPatch.ApplyPatches(_harmonyInstance);
            }
        }
    }
}
