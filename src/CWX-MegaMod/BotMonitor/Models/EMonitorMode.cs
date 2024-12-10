using System.ComponentModel;

namespace CWX_MegaMod.BotMonitor.Models
{
    public enum EMonitorMode
    {
        [Description("Total")]
        Total = 0,
        [Description("Per Zone Total")]
        PerZoneTotal = 1,
        [Description("Per Zone Total With Bot List")]
        PerZoneTotalWithList = 2,
        [Description("Per Zone Bot List and delayed numbers")]
        PerZoneTotalWithListAndDelayedSpawn = 3,
    }
}
