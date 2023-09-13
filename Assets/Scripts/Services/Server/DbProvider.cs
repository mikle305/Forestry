using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Services.Server.Models;

namespace Services.Server
{
    public class DbProvider : MonoBehaviourSingleton<DbProvider>
    {
        private RequestWorker _requestWorker;
        private AuthApiService _authApiService;

        private void Start()
        {
            _requestWorker = RequestWorker.Instance;
            _authApiService = AuthApiService.Instance;
        }

        public async UniTask<bool> SetUsername(string username)
            => false; // await SetUserField("username", username);

        public async UniTask<bool> SetGamePoints(int points)
        {
            return false;
            /*int savedPoints = await GetIntUserField("game_points");
            if (savedPoints >= points)
                return true;
                
            return await SetUserField("game_points", points);*/
        }

        public async Task<Leader[]> GetGamePointsLeaders()
            => Array.Empty<Leader>(); // await GetOrderedUsersBy("game_points");
        
        
        /*
        private async UniTask<bool> SetUserField(string field, object value)
            => await _requestWorker.Work(_database
                .Child("users")
                .Child(_authApiService.CurrentUser.UserId)
                .Child(field)
                .SetValueAsync(value));
        
        private async UniTask<int> GetIntUserField(string field)
        {
            DataSnapshot fieldSnapshot = await GetFieldSnapshot(field);
            return fieldSnapshot.Exists 
                ? int.Parse(fieldSnapshot.Value.ToString()) 
                : default;
        }

        private async UniTask<DataSnapshot> GetFieldSnapshot(string field)
        {
            Task<DataSnapshot> request = _database
                .Child("users")
                .Child(_authApiService.CurrentUser.UserId)
                .Child(field)
                .GetValueAsync();

            return await _requestWorker.Work(request);
        }

        private async UniTask<List<Leader>> GetOrderedUsersBy(string field)
        {
            Task<DataSnapshot> request = _database
                .Child("users")
                .OrderByChild(field)
                .LimitToLast(ServerConstants.GameLeadersCount)
                .GetValueAsync();
            
            DataSnapshot leadersSnapshot = await _requestWorker.Work(request);
            if (leadersSnapshot == null)
                return null;
            
            var leaders = new List<Leader>();
            var rank = 1;
            foreach (DataSnapshot leaderSnapshot in leadersSnapshot.Children.Reverse())
            {
                if (!leaderSnapshot.Child("game_points").Exists)
                    continue;
                
                leaders.Add(new Leader
                {
                    Username = leaderSnapshot.Child("username").Value.ToString(),
                    Score = int.Parse(leaderSnapshot.Child("game_points").Value.ToString()),
                    Rank = rank,
                });

                rank++;
            }
            
            return leaders;
        }*/
    }
}