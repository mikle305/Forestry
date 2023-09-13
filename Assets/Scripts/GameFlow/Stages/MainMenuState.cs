using Additional.Constants;
using GameFlow.Context;
using Services;

namespace GameFlow.Stages
{
    public class MainMenuState : GameState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly InputService _inputService;


        public MainMenuState(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = SceneLoader.Instance;
            _inputService = InputService.Instance;
        }

        public override void Enter()
        {
            _sceneLoader.Load(SceneNames.MainMenu);
        }

        public override void Update()
        {
            if (_inputService.IsPlayInvoked())
                _gameStateMachine.Enter<LevelState>();
        }
    }
}