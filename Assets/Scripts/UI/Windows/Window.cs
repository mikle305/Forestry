using System;
using UnityEngine;

namespace UI.Windows
{
    public abstract class Window : MonoBehaviour
    {
        private bool _isShown;
        

        public void Toggle(Action onStart = null, Action onDone = null)
        {
            if (_isShown)
                Hide(onStart, onDone);
            else
                Show(onStart, onDone);

            _isShown = !_isShown;
        }
        
        public abstract void Show(Action onStart = null, Action onDone = null);
        public abstract void Hide(Action onStart = null, Action onDone = null);
    }
}