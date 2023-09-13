using Additional;
using Additional.Constants;
using StaticData;
using UnityEngine;

namespace Services
{
    public class StaticDataService : MonoBehaviourSingleton<StaticDataService>
    {
        private MusicConfig _musicConfig;


        public MusicConfig GetMusicConfig()
            => _musicConfig ??= LoadData<MusicConfig>(StaticDataPaths.MusicConfig);

        private T LoadData<T>(string path) 
            where T : ScriptableObject
            => Resources.Load<T>(path);
    }
}