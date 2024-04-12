using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CWX_MegaMod.Config;
using EFT.Interactive;
using EFT.UI;
using UnityEngine;

namespace CWX_MegaMod.MasterKey
{
    public class MasterKeyScript : MonoBehaviour
    {
        private List<Door> Doors;
        private Dictionary<string, string> DoorDefaults = new Dictionary<string, string>();
        private List<KeycardDoor> KeycardDoors;
        private Dictionary<string, string> KeycardDoorDefaults = new Dictionary<string, string>();
        private List<LootableContainer> LootableContainers;
        private Dictionary<string, string> LootableContainerDefaults = new Dictionary<string, string>();
        private List<Trunk> Trunks;
        private Dictionary<string, string> TrunkDefaults = new Dictionary<string, string>();

        public MasterKeyScript()
        {
            Doors = UnityEngine.Object.FindObjectsOfType<Door>().ToList();
            KeycardDoors = UnityEngine.Object.FindObjectsOfType<KeycardDoor>().ToList();
            LootableContainers = UnityEngine.Object.FindObjectsOfType<LootableContainer>().ToList();
            Trunks = UnityEngine.Object.FindObjectsOfType<Trunk>().ToList();

            SetDefaults();
        }

        private void SetDefaults()
        {
            foreach (var door in Doors)
            {
                if (door.KeyId != String.Empty)
                {
                    DoorDefaults.Add(door.Id, door.KeyId);
                }
            }

            foreach (var keycardDoor in KeycardDoors)
            {
                if (keycardDoor.KeyId != String.Empty)
                {
                    KeycardDoorDefaults.Add(keycardDoor.Id, keycardDoor.KeyId);
                }
            }

            foreach (var lootableContainer in LootableContainers)
            {
                if (lootableContainer.KeyId != String.Empty)
                {
                    LootableContainerDefaults.Add(lootableContainer.Id, lootableContainer.KeyId);
                }
            }

            foreach (var trunk in Trunks)
            {
                if (trunk.KeyId != String.Empty)
                {
                    TrunkDefaults.Add(trunk.Id, trunk.KeyId);
                }
            }
        }

        public Task StartTask()
        {
            return Task.Factory.StartNew(() =>
            {
                foreach (var door in Doors)
                {
                    if (door.KeyId != String.Empty)
                    {
                        if (MegaMod.MasterKey.Value == true)
                        {
                            door.KeyId = MasterKeyHelper.GetMasterKey(MegaMod.MasterKeyToUse.Value);
                        }
                        else
                        {
                            door.KeyId = DoorDefaults[door.Id];
                        }
                    }
                }

                foreach (var keycardDoor in KeycardDoors)
                {
                    if (keycardDoor.KeyId != String.Empty)
                    {
                        if (MegaMod.MasterKey.Value == true)
                        {
                            keycardDoor.KeyId = MasterKeyHelper.GetMasterKey(MegaMod.MasterKeyToUse.Value);
                        }
                        else
                        {
                            keycardDoor.KeyId = KeycardDoorDefaults[keycardDoor.Id];
                        }
                    }
                }

                foreach (var lootableContainer in LootableContainers)
                {
                    if (lootableContainer.KeyId != String.Empty)
                    {
                        if (MegaMod.MasterKey.Value == true)
                        {
                            lootableContainer.KeyId = MasterKeyHelper.GetMasterKey(MegaMod.MasterKeyToUse.Value);
                        }
                        else
                        {
                            lootableContainer.KeyId = LootableContainerDefaults[lootableContainer.Id];
                        }
                    }
                }

                foreach (var trunk in Trunks)
                {
                    if (trunk.KeyId != String.Empty)
                    {
                        if (MegaMod.MasterKey.Value == true)
                        {
                            trunk.KeyId = MasterKeyHelper.GetMasterKey(MegaMod.MasterKeyToUse.Value);
                        }
                        else
                        {
                            trunk.KeyId = TrunkDefaults[trunk.Id];
                        }
                    }
                }
            });
        }
    }
}