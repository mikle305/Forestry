using System;
using Additional.Game;
using Cysharp.Threading.Tasks;
using Services.Server.Models;

namespace Services.Server
{
    public class LeaderboardService : MonoSingleton<LeaderboardService>
    {
        private DbProvider _dbProvider;

        public event Action<Leader[]> PointsLeadersLoaded;


        private void Start()
        {
            _dbProvider = DbProvider.Instance;
        }

        public async UniTask<bool> SubmitGamePoints(int points) 
            => await _dbProvider.SetGamePoints(points);

        public async UniTask<bool> GetGamePointsLeaders()
        {
            var leaders = await _dbProvider.GetGamePointsLeaders();
            if (leaders == null)
                return false;
            
            PointsLeadersLoaded?.Invoke(leaders); 
            return true;
        }
    }
}