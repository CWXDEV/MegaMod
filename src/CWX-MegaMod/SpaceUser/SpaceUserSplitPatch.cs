using System.Reflection;
using SPT.Reflection.Patching;
using EFT.UI;
using HarmonyLib;
using UnityEngine;

namespace CWX_MegaMod.SpaceUser
{
    public class SpaceUserSplitPatch: ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(SplitDialog), nameof(SplitDialog.Awake));
        }

        [PatchPostfix]
        public static void PatchPostFix()
        {
            GameObject.Find("Split Dialog(Clone)")
                .AddComponent<SpaceUserSplitScript>();            
        }
    }
}