using GameFlow.Context;

namespace GameFlow.States
{
    public class AuthSelectionState : State
    {
        private readonly GameStateMachine _context;

        
        public AuthSelectionState(GameStateMachine context)
        {
            _context = context;
        }

        public override void Enter()
        {
            _context.Enter<EmailAuthState>();
        }
    }
}