using Nakama;
using StaticData;

namespace Services.Server
{
    public class NakamaConnection : MonoBehaviourSingleton<NakamaConnection>
    {
        private StaticDataService _staticDataService;
        private IClient _client;
        
        public IClient Client => _client ??= CreateClient();
        
        private IClient CreateClient()
        {
            _staticDataService ??= StaticDataService.Instance;
            NakamaConnectionParams data = _staticDataService.GetAppConfig().ConnectionParams;
            return new Client(data.Scheme, data.Host, data.Port, data.ServerKey, UnityWebRequestAdapter.Instance);
        }
    }
}