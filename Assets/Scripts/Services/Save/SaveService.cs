using Additional.Game;
using SaveData;

namespace Services.Save
{
    public class SaveService : MonoSingleton<SaveService>
    {
        private const string _progressKey = "Progress";
        private ISaveStorage<PlayerProgress> _storage;
        private ProgressAccess _progressAccess;


        private void Start()
        {
            _storage = new PlayerPrefsStorage<PlayerProgress>(_progressKey);
            _progressAccess = ProgressAccess.Instance;
        }
        
        public void Save()
        {
            PlayerProgress progress = _progressAccess.Progress;
            _storage.Save(progress);
        }

        public PlayerProgress Load()
        {
            return _storage.Load();
        }
    }
}