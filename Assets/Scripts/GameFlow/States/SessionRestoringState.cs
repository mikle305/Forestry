using GameFlow.Context;
using Services.Server;

namespace GameFlow.States
{
    public class SessionRestoringState : State
    {
        private readonly GameStateMachine _context;
        private readonly AuthService _authService;


        public SessionRestoringState(GameStateMachine context)
        {
            _context = context;
            _authService = AuthService.Instance;
        }

        public override void Enter() 
            => CheckSession();

        private void CheckSession()
        {
            bool isSessionActive = _authService.RestoreSession();
            if (isSessionActive) 
                _context.Enter<MainMenuState>();
            else
                _context.Enter<AuthSelectionState>();
        }
    }
}