#if !DEBUG
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFT.Interactive;
using UnityEngine;

namespace CWX_MegaMod.BushWhacker
{
    public class BushWhackerScript : MonoBehaviour
    {
        private List<ObstacleCollider> Bushes;
        private List<BoxCollider> Swamps;
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
            Bushes = FindObjectsOfType<ObstacleCollider>().ToList();
            Swamps = FindObjectsOfType<BoxCollider>().ToList();
            ReadyToEdit = true;
        }

        public async Task ChangeObjects()
        {
            while (!ReadyToEdit)
            {
                await Task.Delay(500);
            }

            if (Swamps != null)
            {
                foreach (var swamp in Swamps)
                {
                    if (swamp.name == "Swamp_collider")
                    {
                        swamp.SetEnabledUniversal(!MegaMod.BushWhacker.Value);
                    }
                }
            }

            if (Bushes != null)
            {
                foreach (var bushesItem in Bushes)
                {
                    var filbert = bushesItem?.transform?.parent?.gameObject?.name.ToLower().Contains("filbert");
                    var fibert = bushesItem?.transform?.parent?.gameObject?.name.ToLower().Contains("fibert");
                    var fibert2 = bushesItem?.transform?.name.ToLower().Contains("fibert");
                    var filbert2 = bushesItem?.transform?.name.ToLower().Contains("filbert");
                    var swamp = bushesItem?.transform?.name.ToLower().Contains("swamp");

                    if (filbert == true || fibert == true)
                    {
                        bushesItem.SetEnabledUniversal(!MegaMod.BushWhacker.Value);
                    }

                    if (filbert2 == true || fibert2 == true || swamp == true)
                    {
                        bushesItem.gameObject.SetActive(!MegaMod.BushWhacker.Value);
                    }
                }
            }
        }
    }
}
#endif