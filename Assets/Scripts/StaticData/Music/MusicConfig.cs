using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace StaticData.Music
{
    [CreateAssetMenu(menuName = "Static Data/Music Config", fileName = "MusicConfig")]
    public class MusicConfig : ScriptableObject
    {
        [SerializeField] private List<Music> _musicCollection;


        public AudioClip GetMusicClip(MusicId id)
            => _musicCollection
                .FirstOrDefault(m => m.Id == id)
                ?.Clip;
    }
}