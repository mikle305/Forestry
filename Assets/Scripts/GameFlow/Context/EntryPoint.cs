using Additional.Game;
using Cysharp.Threading.Tasks;
using GameFlow.States;

namespace GameFlow.Context
{
    public class EntryPoint : MonoSingleton<EntryPoint>
    {
        private GameStateMachine _stateMachine;


        private void Start()
            => Init().Forget();
        
        private async UniTask Init()
        {
            await UniTask.Yield();
            _stateMachine = CreateStateMachine();
            _stateMachine.Enter<ProgressLoadingState>();
        }

        private void Update() 
            => _stateMachine?.Update();

        private GameStateMachine CreateStateMachine()
        {
            var stateMachine = new GameStateMachine();
            State[] states = {
                new ProgressLoadingState(stateMachine),
                new SessionRestoringState(stateMachine),
                new AuthSelectionState(stateMachine),
                new EmailAuthState(stateMachine),
                new ChangeNameState(stateMachine),
                new MainMenuState(stateMachine),
                new LevelState(stateMachine),
            };

            foreach (State state in states) 
                stateMachine.AddState(state);

            return stateMachine;
        }
    }
}