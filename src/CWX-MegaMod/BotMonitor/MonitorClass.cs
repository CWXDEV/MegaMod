﻿// using UnityEngine;
// using Comfort.Common;
// using EFT;
// using EFT.UI;
// using System.Collections.Generic;
// using System.Linq;
// using System.Reflection;
// using CWX_MegaMod.BotMonitor.Models;

// namespace CWX_MegaMod.BotMonitor
// {
//     public class MonitorClass : MonoBehaviour
//     {
//         private GUIContent _guiContent;
//         private GUIStyle _textStyle;
//         private Player _player;
//         private Dictionary<string, List<Player>> _zoneAndPlayers = new Dictionary<string, List<Player>>();
//         private Dictionary<string, BotRoleAndDiffClass> _playerRoleAndDiff = new Dictionary<string, BotRoleAndDiffClass>();
//         private List<BotZone> _zones;
//         private GameWorld _gameWorld;
//         private IBotGame _botGame;
//         private Rect _rect;
//         private string _content = "";
//         private Vector2 _guiSize;
//         private float _distance;

//         ~MonitorClass()
//         {
//             ConsoleScreen.Log("BotMonitor Disabled on game end");
//         }

//         public void Awake()
//         {
//             // Get GameWorld Instance
//             _gameWorld = Singleton<GameWorld>.Instance;

//             // Get BotGame Instance
//             _botGame = Singleton<IBotGame>.Instance;

//             // Get Player from GameWorld
//             _player = _gameWorld.MainPlayer;

//             // Make new rect to use for GUI
//             _rect = new Rect(0, 60, 0, 0);

//             // Get all BotZones - can get for MPT
//             _zones = LocationScene.GetAllObjects<BotZone>().ToList();

//             // Set up the Dictionary - can get for MPT
//             foreach (var botZone in _zones)
//             {
//                 _zoneAndPlayers.Add(botZone.name, new List<Player>());
//             }

//             // Add existing Players to list
//             if (_gameWorld.AllAlivePlayersList.Count > 1)
//             {
//                 foreach (var player in _gameWorld.AllAlivePlayersList)
//                 {
//                     if (player.IsYourPlayer) continue;

//                     _playerRoleAndDiff.Add(player.ProfileId, GetBotRoleAndDiffClass(player.Profile.Info));

//                     // cant get for MPT
//                     var theirZone = player.AIData.BotOwner.BotsGroup.BotZone.NameZone;

//                     // cant do for MPT
//                     _zoneAndPlayers[theirZone].Add(player);
//                 }
//             }

//             // cant do for MPT - we dont spawn bots as client
//             // Sub to Event to get and add Bot when they spawn
//             _botGame.BotsController.BotSpawner.OnBotCreated += owner =>
//             {
//                 var player = owner.GetPlayer;
//                 _zoneAndPlayers[owner.BotsGroup.BotZone.NameZone].Add(player);
//                 _playerRoleAndDiff.Add(player.ProfileId, GetBotRoleAndDiffClass(player.Profile.Info));
//             };

//             // cant do for MPT - we dont spawn bots as client
//             // Sub to event to get and remove Bot when they despawn
//             _botGame.BotsController.BotSpawner.OnBotRemoved += owner =>
//             {
//                 var player = owner.GetPlayer;
//                 _zoneAndPlayers[owner.BotsGroup.BotZone.NameZone].Remove(player);
//                 _playerRoleAndDiff.Remove(player.ProfileId);
//             };
//         }

//         public BotRoleAndDiffClass GetBotRoleAndDiffClass(InfoClass info)
//         {
//             var settings = info.GetType().GetField("Settings", BindingFlags.Public | BindingFlags.Instance).GetValue(info);
//             var role = settings.GetType().GetField("Role", BindingFlags.Instance | BindingFlags.Public).GetValue(settings).ToString();
//             var diff = settings.GetType().GetField("BotDifficulty", BindingFlags.Instance | BindingFlags.Public).GetValue(settings).ToString();

//             return new BotRoleAndDiffClass(string.IsNullOrEmpty(role) ? "" : role, string.IsNullOrEmpty(diff) ? "" : diff);
//         }

//         public void OnGUI()
//         {
//             if (!MegaMod.BotMonitor.Value) return;

//             // set basics on GUI
//             if (_textStyle == null)
//             {
//                 _textStyle = new GUIStyle(GUI.skin.box)
//                 {
//                     alignment = TextAnchor.MiddleLeft,
//                     fontSize = 14,
//                     margin = new RectOffset(3, 3, 3, 3)
//                 };
//             }

//             // new GUI Content
//             if (_guiContent == null)
//             {
//                 _guiContent = new GUIContent();
//             }
//             _content = string.Empty;

//             if (MegaMod.BotMonitorType.Value >= EMonitorMode.DelayedSpawn)
//             {
//                 _content += $"Alive & Loading = {_botGame.BotsController.BotSpawner.AliveAndLoadingBotsCount}\n";
//                 _content += $"Delayed Bots = {_botGame.BotsController.BotSpawner.BotsDelayed}\n";
//                 _content += $"All Bots With Delayed = {_botGame.BotsController.BotSpawner.AllBotsWithDelayed}\n";
//             }

//             // If Mode Greater than or equal to Total show total
//             if (MegaMod.BotMonitorType.Value >= EMonitorMode.Total)
//             {
//                 _content += $"Total = {_gameWorld.AllAlivePlayersList.Count - 1}\n";
//             }

//             // If Mode Greater than or equal to PerZoneTotal show total for each zone
//             if (MegaMod.BotMonitorType.Value >= EMonitorMode.PerZoneTotal && _zoneAndPlayers != null)
//             {
//                 foreach (var zone in _zoneAndPlayers)
//                 {
//                     if (_zoneAndPlayers[zone.Key].FindAll(x => x.HealthController.IsAlive).Count <= 0) continue;

//                     _content += $"{zone.Key} = {_zoneAndPlayers[zone.Key].FindAll(x => x.HealthController.IsAlive).Count}\n";

//                     // If Mode Greater than or equal to FullList show Bots individually also
//                     if (MegaMod.BotMonitorType.Value < EMonitorMode.FullList) continue;

//                     foreach (var player in _zoneAndPlayers[zone.Key].Where(player => player.HealthController.IsAlive))
//                     {
//                         _distance = Vector3.Distance(player.Transform.position, _player.CameraPosition.position);
//                         _content += $"> [{_distance:n2}m] [{_playerRoleAndDiff.First(x => x.Key == player.ProfileId).Value.Role}] " +
//                                     $"[{player.Profile.Side}] [{_playerRoleAndDiff.First(x => x.Key == player.ProfileId).Value.Difficulty}] {player.Profile.Nickname}\n";
//                     }
//                 }
//             }

//             _guiContent.text = _content;

//             _guiSize = _textStyle.CalcSize(_guiContent);

//             _rect.x = Screen.width - _guiSize.x - 5f;
//             _rect.width = _guiSize.x;
//             _rect.height = _guiSize.y;

//             GUI.Box(_rect, _guiContent, _textStyle);
//         }
//     }
// }
