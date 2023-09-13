using System;
using Additional.Constants;

namespace Services
{
    public class InputService : MonoBehaviourSingleton<InputService>
    {
        public bool IsBlocked { get; set; }

        public bool IsPlayInvoked()
            => SimpleInput.GetButtonDown(InputConstants.Play);

        public bool IsErrorConfirmed()
            => SimpleInput.GetButtonDown(InputConstants.ErrorConfirm);

        public bool ToMainMenuInvoked()
            => SimpleInput.GetButtonDown(InputConstants.ToMainMenu);

        public bool IsGuestAuthInvoked()
            => IsBlockableInput(() => SimpleInput.GetButtonDown(InputConstants.GuestAuth));

        public bool IsRegisterInvoked()
            => IsBlockableInput(() => SimpleInput.GetButtonDown(InputConstants.Register));

        public bool IsLoginInvoked()
            => IsBlockableInput(() => SimpleInput.GetButtonDown(InputConstants.Login));

        public bool IsResetPasswordInvoked() 
            => IsBlockableInput(() => SimpleInput.GetButtonDown(InputConstants.ResetPassword));

        public bool IsUsernameEnterInvoked()
            => IsBlockableInput(() => SimpleInput.GetButtonDown(InputConstants.EnterUsername));

        private bool IsBlockableInput(Func<bool> func)
        {
            if (IsBlocked)
                return false;
            
            return func.Invoke();
        }
    }
}