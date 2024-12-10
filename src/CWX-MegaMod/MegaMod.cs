using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using CWX_MegaMod.BotMonitor.Models;
using CWX_MegaMod.Config;
using CWX_MegaMod.InventoryViewer;
using CWX_MegaMod.PainkillerDesat;
using CWX_MegaMod.SpaceUser;
using CWX_MegaMod.TradingPlayerView;
using CWX_MegaMod.WeatherPatcher;
using EFT.UI;

namespace CWX_MegaMod
{
	[BepInPlugin("CWX.MegaMod", "CWX-MegaMod", "1.3.2")]
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
		internal static ConfigEntry<bool> BotMonitor { get; private set; }
		internal static ConfigEntry<EMonitorMode> BotMonitorValue { get; private set; }
		internal static ConfigEntry<int> BotMonitorFontSize { get; private set; }
		
		// internal static ConfigEntry<bool> ChadMode { get; private set; }
		// internal static ConfigEntry<bool> ChadModeStamJiggle { get; private set; }
		// internal static ConfigEntry<bool> ChadModeCamRock { get; private set; }
		// internal static ConfigEntry<bool> ChadModeBreathing { get; private set; }

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
			new PainkillerDesatScript4().Enable();
			new WeatherPatcherScopePatch().Enable();
		}

		// Higher order number comes first
		private void InitConfig()
		{
			// Normal mods
			ReserveAlarmChanger = Config.Bind("1- All Mods", "ReserveAlarmChanger - On/Off", false, new ConfigDescription("Enable Reserve Alarm Changer - changes alarm sound to file on plugin folder (ONLY RUNS ON RAID START)", tags: new ConfigurationManagerAttributes() { Order = 11 }));
			BushWhacker = Config.Bind("1- All Mods", "BushWhacker - On/Off", false, new ConfigDescription("Enable BushWhacker - removes slowdown from bushes", tags: new ConfigurationManagerAttributes() { Order = 10 }));
			GrassCutter = Config.Bind("1- All Mods", "GrassCutter - On/Off", false, new ConfigDescription("Enable GrassCutter - removes grass", tags: new ConfigurationManagerAttributes() { Order = 9 }));
			MasterKey = Config.Bind("1- All Mods", "MasterKey - On/Off", false, new ConfigDescription("Enable MasterKey - Changes Doors to use MasterKeyToUse", tags: new ConfigurationManagerAttributes() { Order = 8 }));
			EnvironmentEnjoyer = Config.Bind("1- All Mods", "EnvironmentEnjoyer - On/Off", false, new ConfigDescription("Enable EnvironmentEnjoyer - Disables trees and bushes", tags: new ConfigurationManagerAttributes() { Order = 7 }));
			SpaceUser = Config.Bind("1- All Mods", "SpaceUser - On/Off", false, new ConfigDescription("Enable SpaceUser - Able to use spacebar to accept flea and split stacks", tags: new ConfigurationManagerAttributes() { Order = 6 }));
			TradingPlayerView = Config.Bind("1- All Mods", "TradingPlayerView - On/Off", false, new ConfigDescription("Enable TradingPlayerView - Changes trading player view", tags: new ConfigurationManagerAttributes() { Order = 5 }));
			PainkillerDesat = Config.Bind("1- All Mods", "PainkillerDesat - On/Off", false, new ConfigDescription("Enable PainkillerDesat - Removes effects from taking painkillers", tags: new ConfigurationManagerAttributes() { Order = 3 }));
			WeatherDebug = Config.Bind("1- All Mods", "WeatherDebugMode - On/Off", false, new ConfigDescription("Enable WeatherDebugMode - Makes it super sunny", tags: new ConfigurationManagerAttributes() { Order = 2 }));
			FogRemover = Config.Bind("1- All Mods", "FogRemover - On/Off", false, new ConfigDescription("Enable FogRemover - Removes fog", tags: new ConfigurationManagerAttributes() { Order = 1 }));
			//ChadMode = Config.Bind("All Mods", "ChadMode - On/Off", false, new ConfigDescription("Enable ChadMode - Sets stamina drain to 0", tags: new ConfigurationManagerAttributes() { Order = 12 }));
			
			// MasterKey Settings
			MasterKeyToUse = Config.Bind("2- MasterKey", "MasterKeyToUse", EMasterKeys.Yellow, new ConfigDescription("This will be set to all unlockable doors", tags: new ConfigurationManagerAttributes() { Order = 1 }));
			
			// Debugging Mods
			BotMonitor = Config.Bind("3- Debug Mods", "BotMonitor - On/Off", false, new ConfigDescription("Enable BotMonitor - Adds a custom gui for Bot Monitoring", tags: new ConfigurationManagerAttributes() { Order = 12 }));
			InventoryViewer = Config.Bind("3- Debug Mods", "InventoryViewer - On/Off", false, new ConfigDescription("Enable InventoryViewer - Changes inventory view to show all containers or not", tags: new ConfigurationManagerAttributes() { Order = 2 }));
			
			// BotMonitor Settings
			BotMonitorValue = Config.Bind("4- BotMonitor", "BotMonitorValue", EMonitorMode.Total, new ConfigDescription("This will be set to only show total", tags: new ConfigurationManagerAttributes() { Order = 2 }));
			BotMonitorFontSize = Config.Bind("4- BotMonitor", "BotMonitorFontSize", 14, new ConfigDescription("This sets the font size obviously", tags: new ConfigurationManagerAttributes() { Order = 1 }));

			//ChadModeStamJiggle = Config.Bind("ChadMode", "Disable Exhaustion Jiggle", false, new ConfigDescription("Disable Exhaustion Jiggle", tags: new ConfigurationManagerAttributes() { Order = 3 }));
			//ChadModeCamRock = Config.Bind("ChadMode", "Disable Exhaustion CameraRock", false, new ConfigDescription("Disable Exhaustion CameraRock", tags: new ConfigurationManagerAttributes() { Order = 2 }));
			//ChadModeBreathing = Config.Bind("ChadMode", "Disable Exhaustion Breathing", false, new ConfigDescription("Disable Exhaustion Breathing", tags: new ConfigurationManagerAttributes() { Order = 1 }));
		}

		public static void LogToScreen(string message = "", EMessageType eMessageType = EMessageType.Info)
		{
			switch (eMessageType)
			{
				case EMessageType.Warning:
					ConsoleScreen.LogWarning("[CWX-MegaMod] " + message);
					Logger.LogWarning("[CWX-MegaMod] " + message);
					break;
				case EMessageType.Error:
					ConsoleScreen.LogError("[CWX-MegaMod] " + message);
					Logger.LogError("[CWX-MegaMod] " + message);
					break;
				case EMessageType.Info:
				default:
					ConsoleScreen.Log("[CWX-MegaMod] " + message);
					Logger.LogDebug("[CWX-MegaMod] " + message);
					break;
			}
		}
	}

	public enum EMessageType
	{
		Warning,
		Error,
		Info
	}
}