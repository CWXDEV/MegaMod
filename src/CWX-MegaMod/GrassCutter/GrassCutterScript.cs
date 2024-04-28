using System.Collections.Generic;
using System.Linq;
using GPUInstancer;
using UnityEngine;

namespace CWX_MegaMod.GrassCutter
{
    public class GrassCutterScript : MonoBehaviour
    {
        private List<GPUInstancerDetailManager> Grass;

        private void Awake()
        {
            Grass = FindObjectsOfType<GPUInstancerDetailManager>().ToList();

            // FIXME: Does not work with Streets
        }

        public void StartTask()
        {
            if (Grass != null)
            {
                foreach (var grass in Grass)
                {
                    if (MegaMod.GrassCutter.Value == true)
                    {
                        grass.enabled = false;
                    }
                    else
                    {
                        grass.enabled = true;
                    }
                }
            }
        }
    }
}