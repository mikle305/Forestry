namespace GameFlow.Stages
{
    public abstract class GameState
    {
        public virtual void Enter() {}

        public virtual void Exit() {}
        
        public virtual void Update() {}
    }
}