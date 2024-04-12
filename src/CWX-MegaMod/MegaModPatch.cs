using System.Reflection;
using Aki.Reflection.Patching;
using Comfort.Common;
using EFT;
using HarmonyLib;

namespace CWX_MegaMod
{
    public class MegaModPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(GameWorld), nameof(GameWorld.OnGameStarted));
        }
        
        [PatchPostfix]
        public static void PatchPostFix()
        {
            var gameWorld = Singleton<GameWorld>.Instance;

            if (gameWorld == null)
                return;

            gameWorld.gameObject.AddComponent<MegaModScript>();
        }
    }
}