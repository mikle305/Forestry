using System;
using System.Threading.Tasks;
using Additional.Game;
using Cysharp.Threading.Tasks;
using Services.Server.Models;

namespace Services.Server
{
    public class DbProvider : MonoSingleton<DbProvider>
    {
        private RequestWorker _requestWorker;

        private void Start()
        {
            _requestWorker = RequestWorker.Instance;
        }

        public async UniTask<bool> SetGamePoints(int points)
        {
            await _requestWorker.OperationNotSupported();
            return false;
        }

        public async Task<Leader[]> GetGamePointsLeaders()
        {
            await _requestWorker.OperationNotSupported();
            return Array.Empty<Leader>();
        }
    }
}