using Additional.Constants;
using Cysharp.Threading.Tasks;
using GameFlow.Context;
using Services;
using Services.Server;
using UI.WindowsInfo;

namespace GameFlow.States
{
    public class AuthMenuState : GameState
    {
        private readonly GameStateMachine _context;
        private readonly AuthService _authService;
        private readonly InputService _inputService;
        private readonly SceneLoader _sceneLoader;


        public AuthMenuState(GameStateMachine context)
        {
            _context = context;
            _sceneLoader = SceneLoader.Instance;
            _inputService = InputService.Instance;
            _authService = AuthService.Instance;
        }

        public override void Enter()
        {
            _sceneLoader.Load(SceneNames.AuthMenu);
        }

        public override void Update()
        {
            if (_inputService.IsRegisterInvoked())
                DoRegister().Forget();
            else if (_inputService.IsLoginInvoked())
                DoLogin().Forget();
            else if (_inputService.IsResetPasswordInvoked())
                ResetPassword().Forget();
            else if (_inputService.IsGuestAuthInvoked())
                GuestAuth();
        }

        private void GuestAuth()
        {
            _authService.IsGuest = true;
            _context.Enter<MainMenuState>();
        }

        private async UniTask DoRegister()
        {
            RegistrationInfo registrationInfo = RegistrationInfo.Instance;
            string email = registrationInfo.Email;
            string password = registrationInfo.Password;
            string username = registrationInfo.Username;
            
            bool isSucceeded = await DoAction(_authService.DoRegister(email, password, username));
            if (!isSucceeded)
                return;

            isSucceeded = await DoAction(_authService.SetUsername(username));
            if (isSucceeded)
                _context.Enter<MainMenuState>();
            else
                _context.Enter<EnterUsernameState>();
        }

        private async UniTask DoLogin()
        {
            LoginInfo loginInfo = LoginInfo.Instance;
            string email = loginInfo.Email;
            string password = loginInfo.Password;

            bool isSucceeded = await DoAction(_authService.DoLogin(email, password));
            if (!isSucceeded)
                return;

            if (!IsUsernameEmpty())
                _context.Enter<MainMenuState>();
            else
                _context.Enter<EnterUsernameState>();
        }

        private async UniTask ResetPassword()
        {
            ResetPasswordInfo resetPasswordInfo = ResetPasswordInfo.Instance;
            string email = resetPasswordInfo.Email;
            await DoAction(_authService.ResetPassword(email));
        }

        private async UniTask<bool> DoAction(UniTask<bool> action)
        {
            _inputService.IsBlocked = true;
            bool result = await action;
            _inputService.IsBlocked = false;
            return result;
        }

        private bool IsUsernameEmpty() 
            => string.IsNullOrEmpty(_authService.Player.Username);
    }
}