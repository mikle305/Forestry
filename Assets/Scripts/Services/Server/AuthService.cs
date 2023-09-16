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
        private ProgressAccess _progressAccess;
        private SaveService _saveService;

        private ISession _session;


        private void Start()
        {
            _authApiService = AuthApiService.Instance;
            _authValidator = AuthValidator.Instance;
            _progressAccess = ProgressAccess.Instance;
            _saveService = SaveService.Instance;
        }

        public bool RestoreSession()
        {
            string savedAuthToken = _progressAccess.Progress.AuthToken;
            if (string.IsNullOrEmpty(savedAuthToken))
                return false;
            
            ISession session = Session.Restore(savedAuthToken);
            if (session.IsExpired)
                return false;

            _session = session;
            print($"Session restored: {_session.AuthToken}");
            return true;
        }

        public async UniTask<bool> DoEmailRegister(string email, string password, string username)
        {
            if (!_authValidator.ValidateEmail(email) || 
                !_authValidator.ValidatePassword(password) || 
                !_authValidator.ValidateUsername(username))
                return false;

            _session = await _authApiService.AuthEmail(email, password, username);
            SaveAuthToken();
            return _session != null;
        }

        public async UniTask<bool> DoEmailLogin(string email, string password)
        {
            if (!_authValidator.ValidateEmail(email) || 
                !_authValidator.ValidatePassword(password))
                return false;

            _session = await _authApiService.AuthEmail(email, password, isRegistration: false);
            SaveAuthToken();
            return _session != null;
        }

        public async UniTask<bool> AuthGuest()
        {
            _session = await _authApiService.AuthGuest(null);
            SaveAuthToken();
            return _session != null;
        }

        public async UniTask<bool> SetUsername(string username)
        {
            if (!_authValidator.ValidateUsername(username))
                return false;

            return await _authApiService.SetUsername(_session, username);
        }

        public async UniTask<bool> ResetPassword(string email)
        {
            if (!_authValidator.ValidateEmail(email))
                return false;

            return await _authApiService.RequestPasswordReset(email);
        }

        private void SaveAuthToken()
        {
            if (_session == null)
                return;

            _progressAccess.Progress.AuthToken = _session.AuthToken;
            _saveService.Save();
            print($"Session saved {_session.AuthToken}");
        }
    }
}