using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
//using CWX_MegaMod.BotMonitor.Models;
using CWX_MegaMod.Config;
using CWX_MegaMod.InventoryViewer;
using CWX_MegaMod.SpaceUser;
using CWX_MegaMod.TradingPlayerView;

namespace CWX_MegaMod
{
	[BepInPlugin("CWX.MegaMod", "CWX-MegaMod", "1.0.0")]
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
		//internal static ConfigEntry<bool> BotMonitor { get; private set; }
		//internal static ConfigEntry<EMonitorMode> BotMonitorType { get; private set; }

		public void Awake()
		{
			Logger = base.Logger;
			InitConfig();
			new MegaModPatch().Enable();
			new SpaceUserSplitPatch().Enable();
			new SpaceUserFleaPatch().Enable();
			new TradingPlayerItemViewPatch().Enable();
			new InventoryViewerPatch().Enable();
		}

		private void InitConfig()
		{
			BushWhacker = Config.Bind("BushWhacker", "On/Off", false, "Enable BushWhacker - removes slowdown from bushes");
			GrassCutter = Config.Bind("GrassCutter", "On/Off", false, "Enable GrassCutter - removes grass");
			MasterKey = Config.Bind("MasterKey", "On/Off", false, "Enable MasterKey - Changes Doors to use MasterKeyToUse");
			MasterKeyToUse = Config.Bind("MasterKey", "MasterKeyToUse", EMasterKeys.Yellow, "This will be set to all unlockable doors");
			EnvironmentEnjoyer = Config.Bind("EnvironmentEnjoyer", "On/Off", false, "Enable EnvironmentEnjoyer - Disables trees and bushes");
			SpaceUser = Config.Bind("SpaceUser", "On/Off", false, "Enable SpaceUser - Able to use spacebar to accept flea and split stacks");
			TradingPlayerView = Config.Bind("TradingPlayerView", "On/Off", false, "Enable TradingPlayerView - Changes trading player view");
			InventoryViewer = Config.Bind("InventoryViewer", "On/Off", false, "Enable InventoryViewer - Changes inventory view to show all containers or not");
			//BotMonitor = Config.Bind("BotMonitor", "On/Off", false, "Enable BotMonitor");
			//BotMonitorType = Config.Bind("BotMonitor", "BotMonitorType", EMonitorMode.Total, "Changes bot monitor type");
		}
	}
}