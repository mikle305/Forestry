using GameFlow.Context;

namespace GameFlow.States
{
    public class CheckSessionState : State
    {
        private readonly GameStateMachine _context;

        public CheckSessionState(GameStateMachine context)
        {
            _context = context;
        }

        public override void Enter()
        {
            _context.Enter<EmailAuthState>();
        }

        /*private async UniTask CheckSession()
        {
            bool isLoggedIn = await _authService.IsLoggedIn();
            if (!isLoggedIn)
            {
                _context.EnterStage(GameStageId.AuthMenu);
                return;
            }

            var isSessionStarted = false;
            while (!isSessionStarted) 
                isSessionStarted = await _authService.StartSession();

            var response = new UsernameResponse { Success = false };
            while (!response.Success) 
                response = await _authService.HasUsername();
            
            _context.EnterStage(response.HasUsername
                ? GameStageId.MainMenu
                : GameStageId.EnterUsername);
        }*/
    }
}