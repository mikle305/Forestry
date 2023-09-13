using System;

namespace ScreenTransitions
{
    public class TransitionOperation : ITransitionOperation
    {
        public event Action OnCompleted;
        
        public void Complete()
        {
            OnCompleted?.Invoke();
        }
    }
}