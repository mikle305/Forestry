using Additional.Game;
using Cysharp.Threading.Tasks;
using Nakama;
using Services.Server.Models;

namespace Services.Server
{
    public class AuthService : MonoSingleton<AuthService>
    {
        private AuthValidator _authValidator;
        private AuthApiService _authApiService;
        private ISession _session;
        /*string authToken = _session.AuthToken;
        bool isExpired = Session.Restore(authToken).IsExpired;*/

        public ProfileData ProfileData => new()
        {
            Id = _session.UserId,
            Username = _session.Username,
        };

        
        private void Start()
        {
            _authApiService = AuthApiService.Instance;
            _authValidator = AuthValidator.Instance;
        }

        public async UniTask<bool> DoRegister(string email, string password, string username)
        {
            if (!_authValidator.ValidateEmail(email) || 
                !_authValidator.ValidatePassword(password) || 
                !_authValidator.ValidateUsername(username))
                return false;

            _session = await _authApiService.AuthEmail(email, password, username);
            return _session != null;
        }

        public async UniTask<bool> DoLogin(string email, string password)
        {
            if (!_authValidator.ValidateEmail(email) || 
                !_authValidator.ValidatePassword(password))
                return false;

            _session = await _authApiService.AuthEmail(email, password, isRegistration: false);
            return _session != null;
        }

        public async UniTask<bool> AuthGuest()
        {
            _session = await _authApiService.AuthGuest(null);
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
    }
}