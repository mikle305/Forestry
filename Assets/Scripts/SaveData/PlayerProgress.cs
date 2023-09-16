using System;
using UnityEngine.Serialization;

namespace SaveData
{
    [Serializable]
    public class PlayerProgress
    {
        [FormerlySerializedAs("AuthKey")] public string AuthToken = string.Empty;
    }
}