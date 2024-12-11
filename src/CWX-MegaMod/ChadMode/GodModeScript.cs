using System;
using System.Threading.Tasks;
using Comfort.Common;
using EFT;
using UnityEngine;

namespace CWX_MegaMod.ChadMode
{
    public class GodModeScript : MonoBehaviour
    {
        private GameWorld _gameWorld;
        private Player _player;
        private float _oldHealthCoef;
        private float _newHealthCoef = 0;
        private float _oldSafeFallHeight;
        private float _newSafeFallHeight = 999999f;
        
        private void Awake()
        {
            _gameWorld = Singleton<GameWorld>.Instance;
            _player = _gameWorld.MainPlayer;
            _oldHealthCoef = _player.ActiveHealthController.DamageCoeff;
            _oldSafeFallHeight = _player.ActiveHealthController.FallSafeHeight;
        }

        public void StartTask()
        {
            if (_player == null)
            {
                MegaMod.LogToScreen("Player object was null, Not changing Damage coef", EMessageType.Error);
                return;
            }

            if (MegaMod.GodMode.Value)
            {
                _player.ActiveHealthController.SetDamageCoeff(_newHealthCoef);
                _player.ActiveHealthController.RemoveNegativeEffects(EBodyPart.Common);
                _player.ActiveHealthController.RestoreFullHealth();
                _player.ActiveHealthController.FallSafeHeight = _newSafeFallHeight;
            }
            else
            {
                _player.ActiveHealthController.SetDamageCoeff(_oldHealthCoef);
                _player.ActiveHealthController.FallSafeHeight = _oldSafeFallHeight;
            }
        }
    }
}