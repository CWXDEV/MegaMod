using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using CWX_MegaMod.BackpackReload;
using CWX_MegaMod.Config;
using CWX_MegaMod.InventoryViewer;
using CWX_MegaMod.PainkillerDesat;
using CWX_MegaMod.SpaceUser;
using CWX_MegaMod.TradingPlayerView;
using CWX_MegaMod.WeatherPatcher;

namespace CWX_MegaMod
{
	[BepInPlugin("CWX.MegaMod", "CWX-MegaMod", "1.2.0")]
	public class MegaMod : BaseUnityPlugin
	{
		internal new static ManualLogSource Logger { get; private set; }
		internal static ConfigEntry<bool> BushWhacker { get; private set; }
		internal static ConfigEntry<bool> GrassCutter { get; private set; }
		internal static ConfigEntry<bool> MasterKey { get; private set; }
		internal static ConfigEntry<EMasterKeys> MasterKeyToUse { get; private set; }
		internal static ConfigEntry<bool> TradingPlayerView { get; private set; }
		internal static ConfigEntry<bool> SpaceUser { get; private set; }
		internal static ConfigEntry<bool> EnvironmentEnjoyer { get; private set; }
		internal static ConfigEntry<bool> InventoryViewer { get; private set; }
		internal static ConfigEntry<bool> PainkillerDesat { get; private set; }
		internal static ConfigEntry<bool> FogRemover { get; private set; }
		internal static ConfigEntry<bool> WeatherDebug { get; private set; }
		internal static ConfigEntry<bool> ReserveAlarmChanger { get; private set; }

		public void Awake()
		{
			Logger = base.Logger;
			InitConfig();

			new MegaModPatch().Enable();
			new SpaceUserSplitPatch().Enable();
			new SpaceUserFleaPatch().Enable();
			new TradingPlayerItemViewPatch().Enable();
			new InventoryViewerPatch().Enable();
			new PainkillerDesatScript1().Enable();
			new PainkillerDesatScript2().Enable();
			new PainkillerDesatScript3().Enable();
			new WeatherPatcherScopePatch().Enable();
		}

		private void InitConfig()
		{
			// Higher order number comes first
			ReserveAlarmChanger = Config.Bind("All Mods", "ReserveAlarmChanger - On/Off", false, new ConfigDescription("Enable Reserve Alarm Changer - changes alarm sound to file on plugin folder (ONLY RUNS ON RAID START)", tags: new ConfigurationManagerAttributes() { Order = 11 }));
			BushWhacker = Config.Bind("All Mods", "BushWhacker - On/Off", false, new ConfigDescription("Enable BushWhacker - removes slowdown from bushes", tags: new ConfigurationManagerAttributes() { Order = 10 }));
			GrassCutter = Config.Bind("All Mods", "GrassCutter - On/Off", false, new ConfigDescription("Enable GrassCutter - removes grass", tags: new ConfigurationManagerAttributes() { Order = 9 }));
			MasterKey = Config.Bind("All Mods", "MasterKey - On/Off", false, new ConfigDescription("Enable MasterKey - Changes Doors to use MasterKeyToUse", tags: new ConfigurationManagerAttributes() { Order = 8 }));
			EnvironmentEnjoyer = Config.Bind("All Mods", "EnvironmentEnjoyer - On/Off", false, new ConfigDescription("Enable EnvironmentEnjoyer - Disables trees and bushes", tags: new ConfigurationManagerAttributes() { Order = 7 }));
			SpaceUser = Config.Bind("All Mods", "SpaceUser - On/Off", false, new ConfigDescription("Enable SpaceUser - Able to use spacebar to accept flea and split stacks", tags: new ConfigurationManagerAttributes() { Order = 6 }));
			TradingPlayerView = Config.Bind("All Mods", "TradingPlayerView - On/Off", false, new ConfigDescription("Enable TradingPlayerView - Changes trading player view", tags: new ConfigurationManagerAttributes() { Order = 5 }));
			InventoryViewer = Config.Bind("All Mods", "InventoryViewer - On/Off", false, new ConfigDescription("Enable InventoryViewer - Changes inventory view to show all containers or not", tags: new ConfigurationManagerAttributes() { Order = 4 }));
			PainkillerDesat = Config.Bind("All Mods", "PainkillerDesat - On/Off", false, new ConfigDescription("Enable PainkillerDesat - Removes effects from taking painkillers", tags: new ConfigurationManagerAttributes() { Order = 3 }));
			WeatherDebug = Config.Bind("All Mods", "WeatherDebugMode - On/Off", false, new ConfigDescription("Enable WeatherDebugMode - Makes it super sunny", tags: new ConfigurationManagerAttributes() { Order = 2 }));
			FogRemover = Config.Bind("All Mods", "FogRemover - On/Off", false, new ConfigDescription("Enable FogRemover - Removes fog", tags: new ConfigurationManagerAttributes() { Order = 1 }));

			MasterKeyToUse = Config.Bind("MasterKey", "MasterKeyToUse", EMasterKeys.Yellow, new ConfigDescription("This will be set to all unlockable doors", tags: new ConfigurationManagerAttributes() { Order = 1 }));
		}
	}
}