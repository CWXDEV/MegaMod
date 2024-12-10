using System;
using UnityEngine;
using Comfort.Common;
using EFT;
using EFT.UI;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CWX_MegaMod.BotMonitor.Models;
using UnityEngine.Serialization;

namespace CWX_MegaMod.BotMonitor
{
    public class BotMonitorScript : MonoBehaviour
    {
        private GUIContent _guiContent;
        public GUIStyle TextStyle;
        private Player _player;
        private Dictionary<string, List<Player>> _zoneAndPlayers = new Dictionary<string, List<Player>>();
        private Dictionary<string, BotRoleAndDiffClass> _playerRoleAndDiff = new Dictionary<string, BotRoleAndDiffClass>();
        private List<BotZone> _zones;
        private GameWorld _gameWorld;
        private IBotGame _botGame;
        private Rect _rect;
        private string _content = "";
        private Vector2 _guiSize;
        private float _distance;

        public void Awake()
        {
            try
            {
                // Get GameWorld Instance
                _gameWorld = Singleton<GameWorld>.Instance;

                // Get BotGame Instance
                _botGame = Singleton<IBotGame>.Instance;

                // Get Player from GameWorld
                _player = _gameWorld.MainPlayer;

                // Make new rect to use for GUI
                _rect = new Rect(0, 60, 0, 0);

                // Get all BotZones - can get for MPT
                _zones = LocationScene.GetAllObjects<BotZone>().ToList();

                // Set up the Dictionary - can get for MPT
                foreach (var botZone in _zones)
                {
                    _zoneAndPlayers.Add(botZone.name, new List<Player>());
                }

                // Add existing Players to list
                if (_gameWorld.AllAlivePlayersList.Count > 1)
                {
                    foreach (var player in _gameWorld.AllAlivePlayersList)
                    {
                        if (player.IsYourPlayer) continue;

                        _playerRoleAndDiff.Add(player.ProfileId, GetBotRoleAndDiffClass(player.Profile.Info));

                        // cant get for MPT
                        var theirZone = player.AIData.BotOwner.BotsGroup.BotZone.NameZone;

                        // cant do for MPT
                        _zoneAndPlayers[theirZone].Add(player);
                    }
                }

                // cant do for MPT - we dont spawn bots as client
                // Sub to Event to get and add Bot when they spawn
                _botGame.BotsController.BotSpawner.OnBotCreated += owner =>
                {
                    var player = owner.GetPlayer;
                    _zoneAndPlayers[owner.BotsGroup.BotZone.NameZone].Add(player);
                    _playerRoleAndDiff.Add(player.ProfileId, GetBotRoleAndDiffClass(player.Profile.Info));
                };

                // cant do for MPT - we dont spawn bots as client
                // Sub to event to get and remove Bot when they despawn
                _botGame.BotsController.BotSpawner.OnBotRemoved += owner =>
                {
                    var player = owner.GetPlayer;
                    _zoneAndPlayers[owner.BotsGroup.BotZone.NameZone].Remove(player);
                    _playerRoleAndDiff.Remove(player.ProfileId);
                };
                
            }
            catch (Exception e)
            {
                MegaMod.LogToScreen("Exception in Awake", EMessageType.Error);
                MegaMod.LogToScreen(e.Message, EMessageType.Error);
            }

            // Init the script and set it to disabled/enabled from bepinex menu
            enabled = MegaMod.BotMonitor.Value;
        }

        public BotRoleAndDiffClass GetBotRoleAndDiffClass(InfoClass info)
        {
            var settings = info.Settings;
            var role = settings.Role.ToString();
            var diff = settings.BotDifficulty.ToString();

            return new BotRoleAndDiffClass(string.IsNullOrEmpty(role) ? "" : role, string.IsNullOrEmpty(diff) ? "" : diff);
        }

        public void OnGUI()
        {
            try
            {
                // should no longer need as script is disabled or enabled instead
                // if (!MegaMod.BotMonitor.Value) return;

                // set basics on GUI
                if (TextStyle == null)
                {
                    TextStyle = new GUIStyle(GUI.skin.box)
                    {
                        alignment = TextAnchor.MiddleLeft,
                        fontSize = MegaMod.BotMonitorFontSize.Value,
                        margin = new RectOffset(3, 3, 3, 3)
                    };
                }

                // new GUI Content
                if (_guiContent == null)
                {
                    _guiContent = new GUIContent();
                }

                _content = string.Empty;

                if (MegaMod.BotMonitorValue.Value >= EMonitorMode.PerZoneTotalWithListAndDelayedSpawn)
                {
                    _content += $"Alive & Loading = {_botGame.BotsController.BotSpawner.AliveAndLoadingBotsCount}\n";
                    _content += $"Delayed Bots = {_botGame.BotsController.BotSpawner.BotsDelayed}\n";
                    _content += $"All Bots With Delayed = {_botGame.BotsController.BotSpawner.AllBotsWithDelayed}\n";
                }

                // If Mode Greater than or equal to Total show total
                if (MegaMod.BotMonitorValue.Value >= EMonitorMode.Total)
                {
                    _content += $"Total = {_gameWorld.AllAlivePlayersList.Count - 1}\n";
                }

                // If Mode Greater than or equal to PerZoneTotal show total for each zone
                if (MegaMod.BotMonitorValue.Value >= EMonitorMode.PerZoneTotal && _zoneAndPlayers != null)
                {
                    foreach (var zone in _zoneAndPlayers)
                    {
                        if (_zoneAndPlayers[zone.Key].FindAll(x => x.HealthController.IsAlive).Count <= 0) continue;

                        _content += $"{zone.Key} = {_zoneAndPlayers[zone.Key].FindAll(x => x.HealthController.IsAlive).Count}\n";

                        // If Mode Greater than or equal to PerZoneTotalWithList show Bots individually also
                        if (MegaMod.BotMonitorValue.Value < EMonitorMode.PerZoneTotalWithList) continue;

                        foreach (var player in _zoneAndPlayers[zone.Key].Where(player => player.HealthController.IsAlive))
                        {
                            _distance = Vector3.Distance(player.Transform.position, _player.CameraPosition.position);
                            _content += $"> [{_distance:n2}m] [{_playerRoleAndDiff.First(x => x.Key == player.ProfileId).Value.Role}] " +
                                        $"[{player.Profile.Side}] [{_playerRoleAndDiff.First(x => x.Key == player.ProfileId).Value.Difficulty}] {player.Profile.Nickname}\n";
                        }
                    }
                }

                _guiContent.text = _content;

                _guiSize = TextStyle.CalcSize(_guiContent);

                _rect.x = Screen.width - _guiSize.x - 5f;
                _rect.width = _guiSize.x;
                _rect.height = _guiSize.y;

                GUI.Box(_rect, _guiContent, TextStyle);
            }
            catch (Exception e)
            {
                MegaMod.LogToScreen("Exception on Gui", EMessageType.Error);
                MegaMod.LogToScreen(e.Message, EMessageType.Warning);
            }

        }
    }
}
