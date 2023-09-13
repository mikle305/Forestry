using Services;
using TMPro;
using UnityEngine;

namespace UI.WindowsInfo
{
    public class EnterUsernameInfo : MonoBehaviourSingleton<EnterUsernameInfo>
    {
        [SerializeField] private TMP_InputField _usernameField;
        
        
        public string Username => _usernameField.text;
    }
}