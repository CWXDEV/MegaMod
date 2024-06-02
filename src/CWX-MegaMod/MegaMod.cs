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
            // Higher order number comes first
            BushWhacker = Config.Bind("BushWhacker", "On/Off", false, new ConfigDescription("Enable BushWhacker - removes slowdown from bushes", tags: new ConfigurationManagerAttributes() { Order = 1 }));

			GrassCutter = Config.Bind("GrassCutter", "On/Off", false, new ConfigDescription("Enable GrassCutter - removes grass", tags: new ConfigurationManagerAttributes() { Order = 1 }));

			MasterKey = Config.Bind("MasterKey", "On/Off", false, new ConfigDescription("Enable MasterKey - Changes Doors to use MasterKeyToUse", tags: new ConfigurationManagerAttributes() { Order = 2 }));
			MasterKeyToUse = Config.Bind("MasterKey", "MasterKeyToUse", EMasterKeys.Yellow, new ConfigDescription("This will be set to all unlockable doors", tags: new ConfigurationManagerAttributes() { Order = 1 }));

			EnvironmentEnjoyer = Config.Bind("EnvironmentEnjoyer", "On/Off", false, new ConfigDescription("Enable EnvironmentEnjoyer - Disables trees and bushes", tags: new ConfigurationManagerAttributes() { Order = 1 }));

			SpaceUser = Config.Bind("SpaceUser", "On/Off", false, new ConfigDescription("Enable SpaceUser - Able to use spacebar to accept flea and split stacks", tags: new ConfigurationManagerAttributes() { Order = 1 }));

			TradingPlayerView = Config.Bind("TradingPlayerView", "On/Off", false, new ConfigDescription("Enable TradingPlayerView - Changes trading player view", tags: new ConfigurationManagerAttributes() { Order = 1 }));

			InventoryViewer = Config.Bind("InventoryViewer", "On/Off", false, new ConfigDescription("Enable InventoryViewer - Changes inventory view to show all containers or not", tags: new ConfigurationManagerAttributes() { Order = 1 }));

			//BotMonitor = Config.Bind("BotMonitor", "On/Off", false, new ConfigDescription("Enable BotMonitor", tags: new ConfigurationManagerAttributes() { Order = 2 }));
			//BotMonitorType = Config.Bind("BotMonitor", "BotMonitorType", EMonitorMode.Total, new ConfigDescription("Changes bot monitor type", tags: new ConfigurationManagerAttributes() { Order = 1 }));
		}
	}
}