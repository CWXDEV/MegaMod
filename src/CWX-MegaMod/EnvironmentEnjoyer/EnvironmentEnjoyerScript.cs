using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Comfort.Common;
using EFT;
using UnityEngine;

namespace CWX_MegaMod.EnvironmentEnjoyer
{
    public class EnvironmentEnjoyerScript : MonoBehaviour
    {
        private List<GameObject> TreeBush = new List<GameObject>();
        private GameWorld _gameWorld;
        private bool ReadyToEdit = false;

        private void Awake()
        {
            _gameWorld = Singleton<GameWorld>.Instance;

            _ = GetGameObjects();
        }

        public void StartTask()
        {
            _ = ChangeObjects();
        }

        private async Task ChangeObjects()
        {
            while (!ReadyToEdit)
            {
                await Task.Delay(500);
            }

            if (TreeBush != null)
            {
                foreach (var bush in TreeBush)
                {
                    bush.SetActive(!MegaMod.EnvironmentEnjoyer.Value);
                }
            }
        }

        public async Task GetGameObjects()
        {
            switch (_gameWorld.LocationId.ToLower())
            {
                // check strings as they might not be frontend strings
                case "woods":
                    GetWoods();
                    break;
                case "bigmap":
                    GetCustoms();
                    break;
                case "tarkovstreets":
                    GetStreets();
                    break;
                case "lighthouse":
                    GetLighthouse();
                    break;
                case "rezervbase":
                    GetReserve();
                    break;
                case "shoreline":
                    GetShoreline();
                    break;
                case "interchange":
                    GetInterchange();
                    break;
                case "sandbox":
                case "sandbox_high":
                    GetGroundZero();
                    break;
                default:
                    break;
            }

            ReadyToEdit = true;
        }

        /**
        * LocationId is bigmap
        */
        public void GetCustoms()
        {
            var customs = FindObjectsOfType<GameObject>().Where(x => x.name.Contains("Slice_") && x.name.Contains("_trees")).ToList();

            foreach (var item in customs)
            {
                if (item != null)
                {
                    TreeBush.Add(item);
                }
            }

            // "Slice_3_2_trees"
            // "Slice_3_3_trees"
        }

        /**
        * LocationId is Woods
        */
        private void GetWoods()
        {
            var woods = FindObjectsOfType<GameObject>().Where(x => x.name.Contains("Slice_") && x.name.Contains("_trees")).ToList();

            foreach (var item in woods)
            {
                if (item != null)
                {
                    TreeBush.Add(item);
                }
            }

            // "Slice_2_4_trees"
            // "Slice_2_3_trees"
            // "Slice_1_3_trees"
            // "Slice_1_4_trees"
        }

        /**
        * LocationId is Sandbox
        */
        private void GetGroundZero()
        {
            var groundzero = FindObjectsOfType<GameObject>().Where(x => x.name.Contains("SBG_Sandbox_") && x.name.Contains("_Plants")).ToList();

            foreach (var item in groundzero)
            {
                if (item != null)
                {
                    TreeBush.Add(item);
                }
            }

            // "SBG_Sandbox_roads_PLANTS"
            // "SBG_Sandbox_Area_01_courtyard_PLANTS"
            // "SBG_Sandbox_Area_02_PLANTS"
            // "SBG_Sandbox_Area_02_courtyard_PLANTS"
            // "SBG_Sandbox_Area_03_PLANTS"
            // "SBG_Sandbox_Area_03_courtyard_PLANTS"
            // "SBG_Sandbox_Area_03_indoor_PLANTS"
            // "SBG_Sandbox_Area_04_PLANTS"
            // "SBG_Sandbox_Area_04_courtyard_PLANTS"
            // "SBG_Sandbox_Area_04_indoor_PLANTS"
            // "SBG_Sandbox_Area_05_courtyard_PLANTS"
            // "SBG_Sandbox_Area_07_courtyard_PLANTS"
            // "SBG_Sandbox_Area_08_PLANTS"
            // "SBG_Sandbox_Area_08_courtyard_PLANTS"
            // "SBG_Sandbox_background_01_PLANTS"
            // "SBG_Sandbox_background_02_PLANTS"
        }

        /**
        * LocationId is Shoreline
        */
        private void GetShoreline()
        {
            var shoreline1 = GameObject.Find("SBG_Trees");
            var shoreline2 = GameObject.Find("SBG_Shoreline_Plants");

            if (shoreline1 != null)
            {
                TreeBush.Add(shoreline1);
            }

            if (shoreline2 != null)
            {
                TreeBush.Add(shoreline2);
            }
        }

        /**
        * LocationId is RezervBase
        */
        private void GetReserve()
        {
            var reserve = FindObjectsOfType<GameObject>().Where(x => x.name.Contains("Slice_") && x.name.Contains("_trees")).ToList();

            foreach (var item in reserve)
            {
                if (item != null)
                {
                    TreeBush.Add(item);
                }
            }

            // "Slice_4_3_trees"
            // "Slice_3_3_trees"
            // "Slice_4_4_trees"
            // "Slice_3_4_trees"
        }

        /**
        * LocationId is TarkovStreets
        */
        private void GetStreets()
        {
            var streets = FindObjectsOfType<GameObject>().Where(x => x.name.Contains("_PLANTS")).ToList();

            foreach (var item in streets)
            {
                if (item != null)
                {
                    TreeBush.Add(item);
                }
            }

            // "SBG_City_SE_01_courtyard_A_PLANTS"
            // "SBG_City_SE_01_courtyard_B_PLANTS"
            // "SBG_City_SE_02_courtyard_A_PLANTS"
            // "SBG_City_SE_02_courtyard_B_PLANTS"
            // "SBG_City_SE_02_Nikitskaya_6_indoor_PLANTS"
            // "SBG_City_SE_03_Cinema_Street_PLANTS"
            // "SBG_City_SE_03_Lenina_80_PLANTS"
            // "SBG_City_SE_03_Square_Gagarina_PLANTS"
            // "SBG_City_SE_04_courtyard_PLANTS"
            // "SBG_City_SE_05_courtyard_PLANTS"
            // "SBG_City_SE_05_Nikitskiy_Market_PLANTS"
            // "SBG_City_SE_06_courtyard_PLANTS"
            // "SBG_City_SW_01_courtyard_A_PLANTS"
            // "SBG_City_SW_01_B_courtyard_PLANTS"
            // "SBG_City_SW_01_B_Kamchatskaya_1a_indoor_2ST_NEW_PLANTS"
            // "SBG_City_SW_01_B_Kamchatskaya_5_PLANTS"
            // "SBG_City_SW_01_B_School_30_PLANTS"
            // "SBG_City_SW_02_LexOs_AutoService_cortyard_PLANTS"
            // "SBG_City_SW_02_LexOs_blockpost_PLANTS"
            // "SBG_City_SW_02_Razvedchikov_Courtyard_A_PLANTS"
            // "SBG_City_SW_02_Sparja_cortyard_PLANTS"
            // "SBG_City_SW_03_courtyad_A_2ST_NEW_PLANTS"
            // "SBG_City_SW_03_courtyad_B_PLANTS"
            // "SBG_City_SW_04_courtyard_PLANTS"
            // "SBG_City_SW_04_Primorskiy_58_indoor_PLANTS"
            // "SBG_City_SW_05_courtyard_A_PLANTS"
            // "SBG_City_SW_05_courtyard_B_PLANTS"
            // "SBG_City_NW_01_Cardinal_PLANTS"
            // "SBG_City_NW_01_courtyard_PLANTS"
            // "SBG_City_NW_02_courtyard_PLANTS"
            // "SBG_City_NW_02_Tetris_2ST_NEW_PLANTS"
            // "SBG_City_NW_03_buildings_back_PLANTS"
            // "SBG_City_NW_03_courtyard_A_PLANTS"
            // "SBG_City_NW_03_Senator_2ST_NEW_PLANTS"
            // "SBG_City_NE_01_courtyard_PLANTS"
            // "SBG_City_NE_02_buildings_back_2ST_REWORK_PLANTS"
            // "SBG_City_NE_02_TD_Klimova_Beluga_indoor_2ST_NEW_PLANTS"
            // "SBG_City_NE_02_TD_Klimova_courtyard_2ST_REWORK_PLANTS"
            // "SBG_City_NE_02_TD_Klimova_Foodcourt_indoor_2ST_NEW_PLANTS"
            // "SBG_City_NE_02_TD_Klimova_indoor_2ST_NEW_PLANTS"
            // "SBG_City_NE_02_TD_Klimova_Toy_Store_indoor_PLANTS"
            // "SBG_City_Roads_Klimova_PLANTS"
            // "SBG_City_Roads_Razvedchikov_PLANTS"
            // "SBG_City_Roads_Tyaglovoy_Per_2ST_NEW_PLANTS"
        }

        /**
        * LocationId is Lighthouse
        */
        private void GetLighthouse()
        {
            var lighthouse1 = GameObject.Find("SBG_Trees");

            if (lighthouse1 != null)
            {
                TreeBush.Add(lighthouse1);
            }
        }

        /**
        * LocationId is Interchange
        */
        private void GetInterchange()
        {
            var interchange = FindObjectsOfType<GameObject>().Where(x => x.name.Contains("Slice_") && x.name.Contains("_trees")).ToList();

            foreach (var item in interchange)
            {
                if (item != null)
                {
                    TreeBush.Add(item);
                }
            }

            // "Slice_2_2_trees"
            // "Slice_1_1_trees"
            // "Slice_1_2_trees"
            // "Slice_2_1_trees"
        }
    }
}
