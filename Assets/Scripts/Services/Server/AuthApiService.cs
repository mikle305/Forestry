using Cysharp.Threading.Tasks;
using Nakama;
using UnityEngine.Device;

namespace Services.Server
{
    public class AuthApiService : MonoBehaviourSingleton<AuthApiService>
    {
        private RequestWorker _requestWorker;
        private NakamaConnection _nakamaConnection;
        
        private ISession _session;


        private void Start()
        {
            _requestWorker = RequestWorker.Instance;
            _nakamaConnection = NakamaConnection.Instance;
        }

        public async UniTask<bool> Register(string email, string password)
            => false;
        /*await _requestWorker.Work(_firebaseAuth.CreateUserWithEmailAndPasswordAsync(email, password)) != null;*/

        public async UniTask<bool> Login(string email, string password)
            => false; 
        /*await _requestWorker.Work(_firebaseAuth.SignInWithEmailAndPasswordAsync(email, password)) != null;*/

        public async UniTask<bool> SetUsername(string username)
            => false;
        /*await _requestWorker.Work(CurrentUser.UpdateUserProfileAsync(new UserProfile { DisplayName = username }));*/

        public async UniTask<bool> RequestPasswordReset(string email)
            => false;
        /*await _requestWorker.Work(request: _firebaseAuth.SendPasswordResetEmailAsync(email), messageOnSuccess: MessageId.PasswordResetRequested);*/

        public async UniTask<bool> AuthGuest()
        {
            _session = await _requestWorker.Work(
                _nakamaConnection.Client.AuthenticateDeviceAsync(SystemInfo.deviceUniqueIdentifier));

            /*string authToken = _session.AuthToken;
            bool isExpired = Session.Restore(authToken).IsExpired;*/
            return _session != null;
        }
    }
}