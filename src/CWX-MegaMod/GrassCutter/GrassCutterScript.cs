using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFT.UI;
using GPUInstancer;
using UnityEngine;

namespace CWX_MegaMod.GrassCutter
{
    public class GrassCutterScript : MonoBehaviour
    {
        private List<GPUInstancerDetailManager> Grass;

        public GrassCutterScript()
        {
            Grass = UnityEngine.Object.FindObjectsOfType<GPUInstancerDetailManager>().ToList();
        }

        public Task StartTask()
        {
            return Task.Factory.StartNew(() =>
            {
                foreach (var grass in Grass)
                {
                    if (MegaMod.GrassCutter.Value == true)
                    {
                        grass.gameObject.SetActive(false);
                    }
                    else
                    {
                        grass.gameObject.SetActive(true);
                    }
                }
            });
        }
    }
}