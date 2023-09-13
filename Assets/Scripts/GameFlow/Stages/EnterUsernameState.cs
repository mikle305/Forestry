using Additional.Constants;
using Cysharp.Threading.Tasks;
using GameFlow.Context;
using Services;
using Services.Server;
using UI.WindowsInfo;

namespace GameFlow.Stages
{
    public class EnterUsernameState : GameState
    {
        private readonly GameStateMachine _context;
        private readonly SceneLoader _sceneLoader;
        private readonly InputService _inputService;
        private readonly AuthService _authService;

        public EnterUsernameState(GameStateMachine context)
        {
            _context = context;
            _sceneLoader = SceneLoader.Instance;
            _inputService = InputService.Instance;
            _authService = AuthService.Instance;
        }

        public override void Enter()
        {
            _sceneLoader.Load(SceneNames.EnterUsername);
        }

        public override void Update()
        {
            if (_inputService.IsUsernameEnterInvoked())
                EnterUsername().Forget();
        }

        private async UniTask EnterUsername()
        {
            string username = EnterUsernameInfo.Instance.Username;
            
            _inputService.IsBlocked = true;
            bool isSucceeded = await _authService.SetUsername(username);
            _inputService.IsBlocked = false;
            if (isSucceeded)
                _context.Enter<MainMenuState>();
        }
    }
}