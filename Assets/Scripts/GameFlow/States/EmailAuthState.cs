using Additional.Constants;
using Cysharp.Threading.Tasks;
using GameFlow.Context;
using Services;
using Services.Server;
using UI.WindowsInfo;

namespace GameFlow.States
{
    public class EmailAuthState : State
    {
        private readonly GameStateMachine _context;
        private readonly AuthService _authService;
        private readonly MenuService _menuService;
        private readonly SceneLoader _sceneLoader;


        public EmailAuthState(GameStateMachine context)
        {
            _context = context;
            _sceneLoader = SceneLoader.Instance;
            _menuService = MenuService.Instance;
            _authService = AuthService.Instance;
        }

        public override void Enter()
        {
            _sceneLoader.Load(SceneNames.EmailAuth);
        }

        public override void Update()
        {
            if (_menuService.IsRegisterInvoked())
                DoRegister().Forget();
            else if (_menuService.IsLoginInvoked())
                DoLogin().Forget();
            else if (_menuService.IsResetPasswordInvoked())
                ResetPassword().Forget();
            else if (_menuService.IsGuestAuthInvoked())
                GuestAuth().Forget();
        }

        private async UniTask DoRegister()
        {
            EmailRegInfo emailRegInfo = EmailRegInfo.Instance;
            string email = emailRegInfo.Email;
            string password = emailRegInfo.Password;
            string username = emailRegInfo.Username;
            
            bool isSucceeded = await _menuService.DoAction(_authService.DoRegister(email, password, username));
            if (isSucceeded)
                _context.Enter<MainMenuState>();
        }

        private async UniTask DoLogin()
        {
            EmailLoginInfo emailLoginInfo = EmailLoginInfo.Instance;
            string email = emailLoginInfo.Email;
            string password = emailLoginInfo.Password;

            bool isSucceeded = await _menuService.DoAction(_authService.DoLogin(email, password));
            if (isSucceeded)
                _context.Enter<MainMenuState>();
        }

        private async UniTask GuestAuth()
        { 
            bool isSucceeded = await _menuService.DoAction(_authService.AuthGuest());
            if (isSucceeded)
                _context.Enter<MainMenuState>();
        }

        private async UniTask ResetPassword()
        {
            string email = EmailResetInfo.Instance.Email;
            await _menuService.DoAction(_authService.ResetPassword(email));
        }
    }
}