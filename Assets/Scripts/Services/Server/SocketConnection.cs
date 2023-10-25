using Additional.Game;
using Cysharp.Threading.Tasks;
using Nakama;

namespace Services.Server
{
    public class SocketConnection : MonoSingleton<SocketConnection>
    {
        private NakamaConnection _nakamaConnection;
        private AuthService _authService;
        private RequestWorker _requestWorker;


        private void Start()
        {
            _nakamaConnection = NakamaConnection.Instance;
            _authService = AuthService.Instance;
            _requestWorker = RequestWorker.Instance;
        }

        public async UniTask Connect()
        {
            ISocket socket = _nakamaConnection.Client.NewSocket(useMainThread: true);
            bool isSucceeded = await _requestWorker.Work(
                request: socket.ConnectAsync(_authService.Session, appearOnline: true), 
                timeout: 10);
        }
    }
}