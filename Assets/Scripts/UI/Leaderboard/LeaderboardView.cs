using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Services.Server.Models;
using UnityEngine;

namespace UI.Leaderboard
{
    public class LeaderboardView : MonoBehaviour
    {
        [SerializeField] private Transform _leadersGrid;
        [SerializeField] private float _showingLeadersDelay = 0.4f;
        [SerializeField] private LeaderBoxInfo _firstRankBoxPrefab;
        [SerializeField] private LeaderBoxInfo _secondRankBoxPrefab;
        [SerializeField] private LeaderBoxInfo _thirdRankBoxPrefab;
        [SerializeField] private LeaderBoxInfo _defaultRankBoxPrefab;

        private Dictionary<int,LeaderBoxInfo> _ranksPrefabMap;


        private void Awake()
        {
            InitRanksPrefabMap();
        }

        public void ClearLeaders()
        {
            foreach(Transform leader in _leadersGrid)
                Destroy(leader);
        }

        public async UniTask ShowLeaders(Leader[] leaders)
        {
            var leaderBoxesMap = new Dictionary<Leader, LeaderBoxInfo>();
            foreach (Leader leader in leaders)
            {
                LeaderBoxInfo prefab = GetRankBoxPrefab(leader);
                LeaderBoxInfo leaderBox = Instantiate(prefab, _leadersGrid);
                leaderBoxesMap[leader] = leaderBox;
            }
            
            foreach (KeyValuePair<Leader, LeaderBoxInfo> item in leaderBoxesMap)
            {
                await UniTask.WaitForSeconds(_showingLeadersDelay);
                LeaderBoxInfo leaderBox = item.Value;
                Leader leader = item.Key;
                leaderBox.Set(leader);
            }
        }

        private LeaderBoxInfo GetRankBoxPrefab(Leader leader)
        {
            return _ranksPrefabMap.TryGetValue(leader.Rank, out LeaderBoxInfo topRankPrefab) 
                ? topRankPrefab 
                : _defaultRankBoxPrefab;
        }

        private void InitRanksPrefabMap()
        {
            _ranksPrefabMap = new Dictionary<int, LeaderBoxInfo>()
            {
                { 1, _firstRankBoxPrefab },
                { 2, _secondRankBoxPrefab },
                { 3, _thirdRankBoxPrefab },
            };
        }
    }
}