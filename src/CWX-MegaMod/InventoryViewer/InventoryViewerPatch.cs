using EFT.InventoryLogic;
using HarmonyLib;
using System.Reflection;
using SPT.Reflection.Patching;

namespace CWX_MegaMod.InventoryViewer
{
	public class InventoryViewerPatch : ModulePatch
	{
		protected override MethodBase GetTargetMethod()
		{
			return AccessTools.Method(typeof(InventoryController), nameof(InventoryController.IsAllowedToSeeSlot));
		}

		[PatchPrefix]
		public static bool PatchPrefix(InventoryController __instance, Slot slot, EquipmentSlot slotName, ref bool __result)
		{
			if (MegaMod.InventoryViewer.Value)
			{
                if (slotName == EquipmentSlot.Dogtag && slot.ParentItem.Parent.GetOwner() == __instance)
                {
                    return true;
                }

				__result = true;
				return false;
			}

			return true;
		}
	}
}