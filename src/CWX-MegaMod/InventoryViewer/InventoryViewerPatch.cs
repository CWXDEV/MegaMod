using Aki.Reflection.Patching;
using HarmonyLib;
using System.Reflection;

namespace CWX_MegaMod.InventoryViewer
{
	public class InventoryViewerPatch : ModulePatch
	{
		protected override MethodBase GetTargetMethod()
		{
			return AccessTools.Method(typeof(InventoryControllerClass), nameof(InventoryControllerClass.IsAllowedToSeeSlot));
		}

		[PatchPrefix]
		public static bool PatchPrefix(InventoryControllerClass __instance, ref bool __result)
		{
			if (MegaMod.InventoryViewer.Value)
			{
				__result = true;
				return false;
			}

			return true;
		}
	}
}