using System;
using BSG.CameraEffects;
using Comfort.Common;
using EFT;
using UnityEngine;

namespace CWX_MegaMod.ChadMode
{
    public class CameraScripts : MonoBehaviour
    {
        private CameraClass _camera;
        private ThermalVision _thermalVision;
        private NightVision _nightVision;
        private bool _oldGlitch;
        private bool _oldNoise;
        private bool _oldFps;
        private bool _oldPixel;
        private bool _oldBlur;
        
        private void Awake()
        {
            _camera = CameraClass.Instance;
            
            _thermalVision = _camera.ThermalVision;
            _oldGlitch = _thermalVision.IsGlitch;
            _oldNoise = _thermalVision.IsNoisy;
            _oldFps = _thermalVision.IsFpsStuck;
            _oldPixel = _thermalVision.IsPixelated;
            _oldBlur = _thermalVision.IsMotionBlurred;
            
            _nightVision = _camera.NightVision;
        }

        public void StartTask()
        {
            if (_camera == null)
            {
                MegaMod.LogToScreen("Camera object was null in CameraScripts", EMessageType.Error);
                return;
            }

            _thermalVision.On = MegaMod.ThermalMode.Value;

            var betterthermalcheck = MegaMod.BetterThermalMode.Value;
            // BetterThermalMode
            _thermalVision.IsGlitch = betterthermalcheck ? false : _oldGlitch;
            _thermalVision.IsNoisy = betterthermalcheck ? false : _oldNoise;
            _thermalVision.IsFpsStuck = betterthermalcheck ? false : _oldFps;
            _thermalVision.IsPixelated = betterthermalcheck ? false : _oldPixel;
            _thermalVision.IsMotionBlurred = betterthermalcheck ? false : _oldBlur;

            // NightvisionMode
            _nightVision.On = MegaMod.NightVisionMode.Value;
        }
    }
}