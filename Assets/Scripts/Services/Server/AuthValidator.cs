using System.Text.RegularExpressions;
using Additional.Game;
using Services.Notifications;

namespace Services.Server
{
    public class AuthValidator : MonoSingleton<AuthValidator>
    {
        private const string _emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        private const string _passwordPattern = @"^[a-zA-Z0-9]{8,}$";
        private const string _usernamePattern = @"^[a-zA-Z0-9]{6,}$";

        private MessageNotifier _messageNotifier;


        private void Start()
        {
            _messageNotifier = MessageNotifier.Instance;
        }

        public bool ValidateEmail(string email) =>
            ValidateInput(email, _emailPattern, ErrorId.InvalidEmail);
        
        public bool ValidatePassword(string password) =>
            ValidateInput(password, _passwordPattern, ErrorId.InvalidPassword);

        public bool ValidateUsername(string username) => 
            ValidateInput(username, _usernamePattern, ErrorId.InvalidUsername);

        private bool ValidateInput(string input, string pattern, ErrorId errorId)
        {
            bool isValid = Regex.IsMatch(input, pattern);
            if (!isValid)
                _messageNotifier.NotifyError(errorId);
            
            return isValid;
        }
    }
}