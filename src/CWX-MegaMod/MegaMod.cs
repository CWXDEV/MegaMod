using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using CWX_MegaMod.BotMonitor.Models;
using CWX_MegaMod.ChadMode;
using CWX_MegaMod.Config;
using CWX_MegaMod.InventoryViewer;
using CWX_MegaMod.LootLoss;
using CWX_MegaMod.PainkillerDesat;
using CWX_MegaMod.SpaceUser;
using CWX_MegaMod.TradingPlayerView;
using CWX_MegaMod.WeatherPatcher;
using EFT.Communications;
using EFT.UI;
using UnityEngine;

namespace CWX_MegaMod
{
	[BepInPlugin("CWX.MegaMod", "CWX-MegaMod", "1.5.0")]
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
		internal static ConfigEntry<bool> GodMode { get; private set; }
		internal static ConfigEntry<bool> ThermalMode { get; private set; }
		internal static ConfigEntry<bool> BetterThermalMode { get; private set; }
		internal static ConfigEntry<bool> NightVisionMode { get; private set; }
		internal static ConfigEntry<bool> CameraShake { get; private set; }
		internal static ConfigEntry<bool> WindowWiper { get; private set; }
		internal static ConfigEntry<bool> LootLoss { get; private set; }
		internal static ConfigEntry<bool> FoodWater { get; private set; }
		internal static ConfigEntry<bool> InstantSearch { get; private set; }

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
			// new WeatherPatcherScopePatch().Enable();
			new CameraShakePatch().Enable();
			new LootLossPatch().Enable();
			new HydrationPatch().Enable();
			new EnergyPatch().Enable();
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
			WindowWiper = Config.Bind("1- All Mods", "WindowWiper - On/Off", false, new ConfigDescription("Enable WindowWiper - Removes Windows", tags: new ConfigurationManagerAttributes() { Order = 1 }));

			// MasterKey Settings
			MasterKeyToUse = Config.Bind("3- MasterKey", "MasterKeyToUse", EMasterKeys.Yellow, new ConfigDescription("This will be set to all unlockable doors", tags: new ConfigurationManagerAttributes() { Order = 1 }));

			// Debugging Mods
			InstantSearch = Config.Bind("2- Debug Mods", "InstantSearch - On/Off", false, new ConfigDescription("Enable InstantSearch - Enables InstantSearch", tags: new ConfigurationManagerAttributes() { Order = 10 }));
			FoodWater = Config.Bind("2- Debug Mods", "FoodWater - On/Off", false, new ConfigDescription("Enable FoodWater - removes hydration and energy drain", tags: new ConfigurationManagerAttributes() { Order = 9 }));
			LootLoss = Config.Bind("2- Debug Mods", "LootLoss - On/Off", false, new ConfigDescription("Enable LootLoss - removes loot on map load", tags: new ConfigurationManagerAttributes() { Order = 8 }));
			BotMonitor = Config.Bind("2- Debug Mods", "BotMonitor - On/Off", false, new ConfigDescription("Enable BotMonitor - Adds a custom gui for Bot Monitoring", tags: new ConfigurationManagerAttributes() { Order = 7 }));
			InventoryViewer = Config.Bind("2- Debug Mods", "InventoryViewer - On/Off", false, new ConfigDescription("Enable InventoryViewer - Changes inventory view to show all containers or not", tags: new ConfigurationManagerAttributes() { Order = 6 }));
			GodMode = Config.Bind("2- Debug Mods", "GodMode - On/Off", false, new ConfigDescription("Enable GodMode - Unable to be killed", tags: new ConfigurationManagerAttributes() { Order = 5 }));
			CameraShake = Config.Bind("2- Debug Mods", "CameraShake - On/Off", false, new ConfigDescription("Disable CameraShake - Removes CameraShake", tags: new ConfigurationManagerAttributes() { Order = 4 }));
			ThermalMode = Config.Bind("2- Debug Mods", "ThermalMode - On/Off", false, new ConfigDescription("Enable ThermalMode - Enables ThermalMode", tags: new ConfigurationManagerAttributes() { Order = 3 }));
			BetterThermalMode = Config.Bind("2- Debug Mods", "BetterThermalMode - On/Off", false, new ConfigDescription("Enable BetterThermalMode - Disables Blur/Noise/etc of the thermal", tags: new ConfigurationManagerAttributes() { Order = 2 }));
			NightVisionMode = Config.Bind("2- Debug Mods", "NightVisionMode - On/Off", false, new ConfigDescription("Enable NightVisionMode - Enables NightVisionMode", tags: new ConfigurationManagerAttributes() { Order = 1 }));

			// BotMonitor Settings
			BotMonitorValue = Config.Bind("4- BotMonitor", "BotMonitorValue", EMonitorMode.Total, new ConfigDescription("This will be set to only show total", tags: new ConfigurationManagerAttributes() { Order = 2 }));
			BotMonitorFontSize = Config.Bind("4- BotMonitor", "BotMonitorFontSize", 14, new ConfigDescription("This sets the font size obviously", tags: new ConfigurationManagerAttributes() { Order = 1 }));
		}

		public static void LogToScreen(string message = "", EMessageType eMessageType = EMessageType.Info)
		{
			switch (eMessageType)
			{
				case EMessageType.NotiError:
					ConsoleScreen.LogError("[CWX-MegaMod Error] " + message);
					Logger.LogError("[CWX-MegaMod Error] " + message);
					NotificationManagerClass.DisplayMessageNotification("[CWX-MegaMod Error] " + message, ENotificationDurationType.Default, ENotificationIconType.Alert, Color.red);
					break;
				case EMessageType.NotiWarn:
					ConsoleScreen.LogWarning("[CWX-MegaMod Warning] " + message);
					Logger.LogWarning("[CWX-MegaMod Warning] " + message);
					NotificationManagerClass.DisplayMessageNotification("[CWX-MegaMod Warning] " + message, ENotificationDurationType.Default, ENotificationIconType.Default, Color.yellow);
					break;
				case EMessageType.NotiInfo:
					ConsoleScreen.Log("[CWX-MegaMod Info] " + message);
					Logger.LogDebug("[CWX-MegaMod Info] " + message);
					NotificationManagerClass.DisplayMessageNotification("[CWX-MegaMod Info] " + message, ENotificationDurationType.Default, ENotificationIconType.Friend, Color.cyan);
					break;
				case EMessageType.Error:
					ConsoleScreen.LogError("[CWX-MegaMod Error] " + message);
					Logger.LogError("[CWX-MegaMod Error] " + message);
					break;
				case EMessageType.Warning:
					ConsoleScreen.LogWarning("[CWX-MegaMod Warning] " + message);
					Logger.LogWarning("[CWX-MegaMod Warning] " + message);
					break;
				case EMessageType.Info:
				default:
					ConsoleScreen.Log("[CWX-MegaMod Info] " + message);
					Logger.LogDebug("[CWX-MegaMod Info] " + message);
					break;
			}
		}
	}

	public enum EMessageType
	{
		NotiError,
		NotiWarn,
		NotiInfo,
		Error,
		Warning,
		Info
	}
}