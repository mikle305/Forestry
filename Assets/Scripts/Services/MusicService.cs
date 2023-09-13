using StaticData;
using UnityEngine;

namespace Services
{
    public class MusicService : MonoBehaviourSingleton<MusicService>
    {
        [SerializeField] private AudioSource _audioSource;
    
        private MusicConfig _musicConfig;


        private void Start()
        {
            _musicConfig = StaticDataService.Instance.GetMusicConfig();
        }

        public void Play(MusicId musicId) 
            => _audioSource.clip = _musicConfig.GetMusicClip(musicId);
    }
}
