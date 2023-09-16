using GameFlow.Context;
using SaveData;
using Services.Save;

namespace GameFlow.States
{
    public class ProgressLoadingState : State
    {
        private readonly GameStateMachine _context;
        private readonly SaveService _saveService;
        private readonly ProgressAccess _progressAccess;


        public ProgressLoadingState(GameStateMachine context)
        {
            _context = context;
            _saveService = SaveService.Instance;
            _progressAccess = ProgressAccess.Instance;
        }

        public override void Enter()
        {
            _progressAccess.Progress = _saveService.Load() ?? CreateNewProgress();
            _context.Enter<SessionRestoringState>();
        }

        private PlayerProgress CreateNewProgress() 
            => new();
    }
}