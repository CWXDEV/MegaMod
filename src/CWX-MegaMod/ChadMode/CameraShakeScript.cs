using System;
using Comfort.Common;
using EFT;
using UnityEngine;

namespace CWX_MegaMod.ChadMode
{
    public class CameraShakeScript : MonoBehaviour
    {
        private CameraClass _camera;
        
        private void Awake()
        {
            _camera = CameraClass.Instance;
        }

        public void StartTask()
        {
            if (_camera == null)
            {
                MegaMod.LogToScreen("Camera object was null, not changing Camera shake", EMessageType.Error);
                return;
            }
            
            // _camera.EffectsController.enabled = !MegaMod.CameraShake.Value;
            // also does this
            // _camera.Camera.GetComponent<PrismEffects>().useVignette = !MegaMod.CameraShake.Value;
        }
    }
}