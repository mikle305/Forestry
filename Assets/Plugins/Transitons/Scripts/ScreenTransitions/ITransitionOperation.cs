using System;

namespace ScreenTransitions
{
    public interface ITransitionOperation
    {
        event Action OnCompleted;
    }
}