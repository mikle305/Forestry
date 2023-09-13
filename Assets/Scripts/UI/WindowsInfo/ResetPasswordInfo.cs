using Services;
using TMPro;
using UnityEngine;

namespace UI.WindowsInfo
{
    public class ResetPasswordInfo : MonoBehaviourSingleton<ResetPasswordInfo>
    {
        [SerializeField] private TMP_InputField _emailField;
        
        public string Email => _emailField.text;
    }
}