using Comfort.Common;
using EFT;
using EFT.HealthSystem;
using HarmonyLib;
using UnityEngine;

namespace CWX_MegaMod.ChadMode
{
    public class StaminaScript : MonoBehaviour
    {
        private GameWorld _gameWorld;
        private Player _player;
        private float _oldStaminaCoef;
        private float _newStaminaCoef = 0;

        private void Awake()
        {
            _gameWorld = Singleton<GameWorld>.Instance;
            _player = _gameWorld.MainPlayer;
            _oldStaminaCoef = _player.ActiveHealthController.StaminaCoeff;
        }

        public void StartTask()
        {
            if (_player == null)
            {   
                MegaMod.LogToScreen("Player object was null, Not changing stamina coef", EMessageType.Error);
                return;
            }
            
            // _player.ActiveHealthController.SetStaminaCoeff((MegaMod.UnlimitesStamina.Value ? _newStaminaCoef : _oldStaminaCoef));
        }
    }
}