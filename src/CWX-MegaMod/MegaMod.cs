using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using CWX_MegaMod.Config;

namespace CWX_MegaMod
{
    [BepInPlugin("com.CWX.MegaMod", "CWX-MegaMod", "1.0.0")]
    public class MegaMod : BaseUnityPlugin
    {
        internal new static ManualLogSource Logger { get; private set; }
        internal static ConfigEntry<bool> BushWhacker { get; private set; }
        internal static ConfigEntry<bool> GrassCutter { get; private set; }
        internal static ConfigEntry<bool> MasterKey { get; private set; }
        internal static ConfigEntry<EMasterKeys> MasterKeyToUse { get; private set; }
        
        public void Awake()
        {
            Logger = base.Logger;
            InitConfig();
            new MegaModPatch().Enable();
        }

        private void InitConfig()
        {
            BushWhacker = Config.Bind("BushWhacker", "On/Off", false, "Enable BushWhacker - removes slowdown from bushes");
            GrassCutter = Config.Bind("GrassCutter", "On/Off", false, "Enable GrassCutter - removes grass");
            MasterKey = Config.Bind("MasterKey", "On/Off", false, "Enable MasterKey - Changes Doors to use MasterKeyToUse");
            MasterKeyToUse = Config.Bind("MasterKey", "MasterKeyToUse", EMasterKeys.Yellow, "This will be set to all unlockable doors");
        }
    }
}