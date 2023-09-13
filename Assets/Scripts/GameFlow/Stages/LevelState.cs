using Additional.Constants;
using GameFlow.Context;
using Services;
using StaticData;

namespace GameFlow.Stages
{
    public class LevelState : GameState
    {
        private readonly GameStateMachine _context;
        private readonly SceneLoader _sceneLoader;
        private readonly MusicService _musicService;


        public LevelState(GameStateMachine context)
        {
            _context = context;
            _sceneLoader = SceneLoader.Instance;
            _musicService = MusicService.Instance;
        }

        public override void Enter()
        {
            _sceneLoader.Load(SceneNames.Level, OnLevelLoaded);
        }

        public override void Exit()
        {
        }

        private void OnLevelLoaded()
        {
            _musicService.Play(MusicId.Fight);
        }
    }
}