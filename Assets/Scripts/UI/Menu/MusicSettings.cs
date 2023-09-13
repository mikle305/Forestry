using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace UI.Menu
{
    public class MusicSettings : MonoBehaviour
    {
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private AudioMixer _audioMixer;
        
        private const string _volumeMixerParameter = "Volume";

        
        private void Awake()
        {
            _musicSlider.value = _musicSlider.maxValue;
            ChangeVolume(_musicSlider.value);
            _musicSlider.onValueChanged.AddListener(ChangeVolume);
        }

        private void ChangeVolume(float volume)
        {
            _audioMixer.SetFloat(_volumeMixerParameter, Mathf.Log10(volume) * 20);
        }
    }
}