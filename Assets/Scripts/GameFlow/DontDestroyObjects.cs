using UnityEngine;

namespace GameFlow
{
    public class DontDestroyObjects : MonoBehaviour
    {
        [SerializeField] private GameObject[] _objects;

        private void Awake()
        {
            foreach (GameObject obj in _objects) 
                DontDestroyOnLoad(obj);
        }
    }
}
