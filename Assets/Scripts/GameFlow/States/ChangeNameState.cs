using Additional.Constants;
using Cysharp.Threading.Tasks;
using GameFlow.Context;
using Services;
using Services.Server;
using UI.WindowsInfo;

namespace GameFlow.States
{
    public class ChangeNameState : State
    {
        private readonly GameStateMachine _context;
        private readonly SceneLoader _sceneLoader;
        private readonly MenuService _menuService;
        private readonly AuthService _authService;

        
        public ChangeNameState(GameStateMachine context)
        {
            _context = context;
            _sceneLoader = SceneLoader.Instance;
            _menuService = MenuService.Instance;
            _authService = AuthService.Instance;
        }

        public override void Enter()
        {
            _sceneLoader.Load(SceneNames.ChangeName);
        }

        public override void Update()
        {
            if (_menuService.IsChangeNameInvoked())
                ChangeName().Forget();
        }

        private async UniTask ChangeName()
        {
            string username = UsernameInfo.Instance.Username;

            bool isSucceeded = await _menuService.DoAction(_authService.SetUsername(username));
            if (isSucceeded)
                _context.Enter<MainMenuState>();
        }
    }
}