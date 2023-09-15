using Additional.Game;
using UnityEngine;

namespace Services
{
    public class ObjectsProvider : MonoSingleton<ObjectsProvider>
    {
        [SerializeField] private Camera _uiCamera;
        
        public Camera UICamera => _uiCamera;
    }
}