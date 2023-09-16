using Additional.Game;
using SaveData;
using Services.Save;
using UnityEngine;
using UnityEngine.Audio;

namespace Services
{
    public class SettingsService : MonoSingleton<SettingsService>
    {
        private const string _volumeMixerParameter = "Volume";
        private SaveService _saveService;
        private AudioMixer _audioMixer;


        private void Start()
        {
            _saveService = SaveService.Instance;
            _audioMixer = StaticDataService.Instance.GetMusicConfig().AudioMixer;
            _saveService.ProgressLoaded += ApplySettings;
        }

        public SettingsData GetSettings() 
            => _saveService.Progress.SettingsData;

        public void Apply()
        {
            ApplySettings();
            _saveService.Save();
        }

        private void ApplySettings()
        {
            SettingsData settings = GetSettings();
            SetVolume(settings);
        }
        
        private void SetVolume(SettingsData settings)
            => _audioMixer.SetFloat(_volumeMixerParameter, Mathf.Log10(settings.Volume) * 20);
    }
}