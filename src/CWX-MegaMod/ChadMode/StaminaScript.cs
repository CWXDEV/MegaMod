using Comfort.Common;
using EFT;
using EFT.HealthSystem;
using HarmonyLib;
using UnityEngine;

namespace CWX_MegaMod.ChadMode
{
    public class StaminaScript : MonoBehaviour
    {
        private BackendConfigSettingsClass backendConfigSettingsClass;

        private void Awake()
        {
            backendConfigSettingsClass = Singleton<BackendConfigSettingsClass>.Instance;
        }

        public void StartTask()
        {
            if (backendConfigSettingsClass != null)
            {
                // backendConfigSettingsClass.Stamina
            }
        }
    }
}