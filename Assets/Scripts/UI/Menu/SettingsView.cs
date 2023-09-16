using SaveData;
using Services;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Menu
{
    public class SettingsView : MonoBehaviour
    {
        [SerializeField] private Button _applyButton;
        [SerializeField] private Slider _volumeSlider;

        private SettingsService _settingsService;
        private SettingsData _settings;


        private void Start()
        {
            _settingsService = SettingsService.Instance;
            _settings = _settingsService.Settings;
            UpdateView();
            InitViewEvents();
        }

        private void InitViewEvents()
        {
            _volumeSlider.onValueChanged.AddListener(
                value => _settings.Volume = value);
            
            _applyButton.onClick.AddListener(
                () => _settingsService.Apply());
        }

        private void UpdateView()
        {
            _volumeSlider.value = _settings.Volume;
        }
    }
}