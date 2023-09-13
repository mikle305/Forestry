using Cysharp.Threading.Tasks;
using Services.Server.Models;

namespace Services.Server
{
    public class AuthService : MonoBehaviourSingleton<AuthService>
    {
        private AuthValidator _authValidator;
        private AuthApiService _authApiService;
        private DbProvider _dbProvider;


        public Player Player => new()
        {
            Id = string.Empty, /*_authApiService.CurrentUser.UserId,*/
            Username = string.Empty, /*_authApiService.CurrentUser.DisplayName,*/
        };

        public bool IsGuest { get; set; }

        
        private void Start()
        {
            _authApiService = AuthApiService.Instance;
            _authValidator = AuthValidator.Instance;
            _dbProvider = DbProvider.Instance;
        }

        public async UniTask<bool> DoRegister(string email, string password, string username)
        {
            if (!_authValidator.ValidateEmail(email) || 
                !_authValidator.ValidatePassword(password) || 
                !_authValidator.ValidateUsername(username))
                return false;

            return await _authApiService.Register(email, password);
        }

        public async UniTask<bool> DoLogin(string email, string password)
        {
            if (!_authValidator.ValidateEmail(email) || 
                !_authValidator.ValidatePassword(password))
                return false;

            return await _authApiService.Login(email, password);
        }

        public async UniTask<bool> SetUsername(string username)
        {
            if (!_authValidator.ValidateUsername(username))
                return false;

            bool isDisplayDone = await _authApiService.SetUsername(username);
            if (!isDisplayDone)
                return false;

            return await _dbProvider.SetUsername(username);
        }

        public async UniTask<bool> ResetPassword(string email)
        {
            if (!_authValidator.ValidateEmail(email))
                return false;

            return await _authApiService.RequestPasswordReset(email);
        }
    }
}