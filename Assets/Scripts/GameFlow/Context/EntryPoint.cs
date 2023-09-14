using Cysharp.Threading.Tasks;
using GameFlow.States;
using Services;

namespace GameFlow.Context
{
    public class EntryPoint : MonoBehaviourSingleton<EntryPoint>
    {
        private GameStateMachine _stateMachine;


        private void Start()
            => Init().Forget();
        
        private async UniTask Init()
        {
            await UniTask.Yield();
            _stateMachine = CreateStateMachine();
            _stateMachine.Enter<AuthMenuState>();
        }

        private void Update() 
            => _stateMachine?.Update();

        private GameStateMachine CreateStateMachine()
        {
            var stateMachine = new GameStateMachine();
            GameState[] states = {
                new CheckSessionState(stateMachine),
                new AuthMenuState(stateMachine),
                new EnterUsernameState(stateMachine),
                new MainMenuState(stateMachine),
                new LevelState(stateMachine),
            };

            foreach (GameState state in states) 
                stateMachine.AddState(state);

            return stateMachine;
        }
    }
}