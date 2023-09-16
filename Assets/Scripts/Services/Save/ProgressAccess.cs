using Additional.Game;
using SaveData;

namespace Services.Save
{
    public class ProgressAccess : MonoSingleton<ProgressAccess>
    {
        public PlayerProgress Progress { get; set; }
    }
}