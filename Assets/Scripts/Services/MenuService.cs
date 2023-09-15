using System;
using Additional.Constants;
using Additional.Game;
using Cysharp.Threading.Tasks;

namespace Services
{
    public class MenuService : MonoSingleton<MenuService>
    {
        private bool _inProcess;


        public async UniTask<bool> DoAction(UniTask<bool> task)
        {
            _inProcess = true;
            bool result = await task;
            _inProcess = false;
            return result;
        }

        public bool IsPlayInvoked()
            => SimpleInput.GetButtonDown(InputConstants.Play);

        public bool IsErrorConfirmed()
            => SimpleInput.GetButtonDown(InputConstants.ErrorConfirm);

        public bool ToMainMenuInvoked()
            => SimpleInput.GetButtonDown(InputConstants.ToMainMenu);

        public bool IsGuestAuthInvoked()
            => IsProcessableInput(() => SimpleInput.GetButtonDown(InputConstants.GuestAuth));

        public bool IsRegisterInvoked()
            => IsProcessableInput(() => SimpleInput.GetButtonDown(InputConstants.Register));

        public bool IsLoginInvoked()
            => IsProcessableInput(() => SimpleInput.GetButtonDown(InputConstants.Login));

        public bool IsResetPasswordInvoked() 
            => IsProcessableInput(() => SimpleInput.GetButtonDown(InputConstants.ResetPassword));

        public bool IsChangeNameInvoked()
            => IsProcessableInput(() => SimpleInput.GetButtonDown(InputConstants.EnterUsername));

        private bool IsProcessableInput(Func<bool> func)
        {
            if (_inProcess)
                return false;
            
            return func.Invoke();
        }
    }
}