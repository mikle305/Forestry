using Additional;
using UnityEngine;

namespace Services
{
    public class MonoBehaviourSingleton<T> : MonoBehaviour
        where T : MonoBehaviourSingleton<T>
    {
        private static T _instance;
        
        public static T Instance => _instance;
        
        
        protected virtual void Awake()
        {
            if (_instance != null)
                ThrowUtils.ManyInstancesOfSingleton();
                
            _instance = (T)this;
        }
    }
}