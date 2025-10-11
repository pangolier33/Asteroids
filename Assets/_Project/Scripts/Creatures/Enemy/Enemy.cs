using _Project.Scripts.Services;
using _Project.Scripts.UI;
using UnityEngine;

namespace _Project.Scripts.Creatures.Enemy
{
    public class Enemy : MonoBehaviour
    {
        public ScoreService _scoreService { get; private set; }
        private int _enemyScoreValue = 1;
        
        public void DestroyEnemy()
        {
            DieEnemy();
            Destroy(gameObject);
        }
        
        private void DieEnemy()
        {
            int score = PlayerPrefs.GetInt("CurrentScore");
            PlayerPrefs.SetInt("CurrentScore", score + _enemyScoreValue);
        }
    }
}
