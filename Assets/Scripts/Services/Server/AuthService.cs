using Additional.Game;
using Cysharp.Threading.Tasks;
using Nakama;
using Services.Save;

namespace Services.Server
{
    public class AuthService : MonoSingleton<AuthService>
    {
        private AuthValidator _authValidator;
        private AuthApiService _authApiService;
        private SaveService _saveService;

        public ISession Session { get; private set; }


        private void Start()
        {
            _authApiService = AuthApiService.Instance;
            _authValidator = AuthValidator.Instance;
            _saveService = SaveService.Instance;
        }

        public bool RestoreSession()
        {
            string savedAuthToken = _saveService.Progress.AuthToken;
            if (string.IsNullOrEmpty(savedAuthToken))
                return false;
            
            ISession session = Nakama.Session.Restore(savedAuthToken);
            if (session.IsExpired)
                return false;

            Session = session;
            return true;
        }

        public async UniTask<bool> DoEmailRegister(string email, string password, string username)
        {
            if (!_authValidator.ValidateEmail(email) || 
                !_authValidator.ValidatePassword(password) || 
                !_authValidator.ValidateUsername(username))
                return false;

            Session = await _authApiService.AuthEmail(email, password, username);
            SaveAuthToken();
            return Session != null;
        }

        public async UniTask<bool> DoEmailLogin(string email, string password)
        {
            if (!_authValidator.ValidateEmail(email) || 
                !_authValidator.ValidatePassword(password))
                return false;

            Session = await _authApiService.AuthEmail(email, password, isRegistration: false);
            SaveAuthToken();
            return Session != null;
        }

        public async UniTask<bool> AuthGuest()
        {
            Session = await _authApiService.AuthGuest(null);
            SaveAuthToken();
            return Session != null;
        }

        public async UniTask<bool> SetUsername(string username)
        {
            if (!_authValidator.ValidateUsername(username))
                return false;

            return await _authApiService.SetUsername(Session, username);
        }

        public async UniTask<bool> ResetPassword(string email)
        {
            if (!_authValidator.ValidateEmail(email))
                return false;

            return await _authApiService.RequestPasswordReset(email);
        }

        private void SaveAuthToken()
        {
            if (Session == null)
                return;

            _saveService.Progress.AuthToken = Session.AuthToken;
            _saveService.Save();
        }
    }
}