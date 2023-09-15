using Services.Server.Models;
using TMPro;
using UI.Windows;
using UnityEngine;

namespace UI.Leaderboard
{
    public class LeaderBoxInfo : MonoBehaviour
    {
        [SerializeField] private Window _window;
        [SerializeField] private TextMeshProUGUI _rankText;
        [SerializeField] private TextMeshProUGUI _usernameText;
        [SerializeField] private TextMeshProUGUI _scoreText;


        private void Awake()
        {
            transform.localScale = Vector3.zero;
        }

        public void Set(Leader leader)
        {
            _rankText.text = leader.Rank.ToString();
            _usernameText.text = leader.Username;
            _scoreText.text = leader.Score.ToString();
            _window.Show();
        }
    }
}