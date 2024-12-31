#if !DEBUG
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GPUInstancer;
using UnityEngine;

namespace CWX_MegaMod.GrassCutter
{
    public class GrassCutterScript : MonoBehaviour
    {
        private List<GPUInstancerDetailManager> Grass;
        private bool ReadyToEdit = false;

        private void Awake()
        {
            _ = GetObjects();
        }

        public void StartTask()
        {
            _ = ChangeObjects();
        }

        private async Task GetObjects()
        {
            Grass = FindObjectsOfType<GPUInstancerDetailManager>().ToList();
            ReadyToEdit = true;
        }

        private async Task ChangeObjects()
        {
            while (!ReadyToEdit)
            {
                await Task.Delay(500);
            }

            if (Grass != null)
            {
                foreach (var grass in Grass)
                {
                    grass.SetEnabledUniversal(!MegaMod.GrassCutter.Value);
                }
            }
        }
    }
}
#endif