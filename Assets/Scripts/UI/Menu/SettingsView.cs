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


        private void Start()
        {
            _settingsService = SettingsService.Instance;
            UpdateView();
            InitViewEvents();
        }

        private void InitViewEvents()
        {
            _volumeSlider.onValueChanged.AddListener(
                value => _settingsService.Settings.Volume = value);
            
            _applyButton.onClick.AddListener(
                () => _settingsService.Apply());
        }

        private void UpdateView()
        {
            _volumeSlider.value = _settingsService.Settings.Volume;
        }
    }
}