using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Comfort.Common;
using EFT;
using UnityEngine;
using UnityEngine.Networking;

namespace CWX_MegaMod.AlarmChanger
{
    public class AlarmChangerScript : MonoBehaviour
    {
        private List<AudioClip> _clips;
        private List<string> audioFiles;
        private List<InteractiveSubscriber> subs;

        public async void Awake()
        {
            if (Singleton<GameWorld>.Instance.LocationId.ToLower() != "rezervbase")
            {
                Destroy(this);
            }

            _clips = new List<AudioClip>();
            audioFiles = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + "/BepInEx/plugins/CWX/Sounds/").ToList();
            subs = FindObjectsOfType<InteractiveSubscriber>().Where(x => x.name.Contains("Siren_")).ToList();

            foreach (var File in audioFiles)
            {
                await LoadAudioClip(File);
            }

            if (MegaMod.ReserveAlarmChanger.Value)
            {
                await SetSounds();
            }
        }

        private async Task SetSounds()
        {
            if (_clips.Count <= 0)
            {
                return;
            }

            if (subs.Count <= 0)
            {
                return;
            }

            var clip = _clips[UnityEngine.Random.Range(0, _clips.Count)];

            foreach (var sub in subs)
            {
                sub.Sounds.FirstOrDefault().Clip = clip;
            }
        }

        private async Task LoadAudioClip(string path)
        {
            var audioClip = await RequestAudioClip(path);

            _clips.Add(audioClip);
        }

        private async Task<AudioClip> RequestAudioClip(string path)
        {
            UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(path, AudioType.WAV);

            var sendWeb = www.SendWebRequest();

            while (!sendWeb.isDone)
            {
                await Task.Yield();
            }

            if (www.isNetworkError || www.isHttpError)
            {
                return null;
            }

            AudioClip audioclip = DownloadHandlerAudioClip.GetContent(www);

            return audioclip;
        }
    }
}