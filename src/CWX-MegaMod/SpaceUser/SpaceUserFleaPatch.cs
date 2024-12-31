#if !DEBUG
using System.Linq;
using System.Reflection;
using SPT.Reflection.Patching;
using EFT.UI;
using HarmonyLib;
using UnityEngine;

namespace CWX_MegaMod.SpaceUser
{
    public class SpaceUserFleaPatch: ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(MenuUI), nameof(MenuUI.Awake));
        }

        [PatchPostfix]
        public static void PatchPostFix()
        {
            GameObject.Find("Menu UI")
                .GetComponentsInChildren<Transform>(true)
                .First(x => x.name == "YesButton")
                .gameObject.AddComponent<SpaceUserFleaScript>();
        }
    }
}
#endif