using UnityEngine;

namespace Services
{
    public class ObjectsProvider : MonoBehaviourSingleton<ObjectsProvider>
    {
        [SerializeField] private Camera _uiCamera;
        
        public Camera UICamera => _uiCamera;
    }
}