using Additional.Game;
using Cysharp.Threading.Tasks;
using Nakama;
using UnityEngine.Device;

namespace Services.Server
{
    public class AuthApiService : MonoSingleton<AuthApiService>
    {
        private RequestWorker _requestWorker;
        private NakamaConnection _nakamaConnection;


        private void Start()
        {
            _requestWorker = RequestWorker.Instance;
            _nakamaConnection = NakamaConnection.Instance;
        }

        public async UniTask<ISession> AuthEmail(
            string email, 
            string password, 
            string username = null,
            bool isRegistration = true)
        {
            return await _requestWorker.Work(
                request: _nakamaConnection.Client.AuthenticateEmailAsync(email, password, username, isRegistration));
        }

        public async UniTask<ISession> AuthGuest(string username)
        {
            string id = SystemInfo.deviceUniqueIdentifier;
            return await _requestWorker.Work(
                request: _nakamaConnection.Client.AuthenticateDeviceAsync(id, username));
        }


        public async UniTask<bool> SetUsername(ISession session, string username)
            => await _requestWorker.Work(
                request: _nakamaConnection.Client.UpdateAccountAsync(session, username));

        public async UniTask<bool> RequestPasswordReset(string email)
            => false;
    }
}