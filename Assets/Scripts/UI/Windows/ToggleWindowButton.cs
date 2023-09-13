using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows
{
    public class ToggleWindowButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Window _window;
        

        private void Start()
        {
            _button.onClick.AddListener(OnButtonClicked);
        }

        private void OnButtonClicked() 
            => _window.Toggle();
    }
}