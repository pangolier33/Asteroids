using _Project.Scripts.Services;
using _Project.Scripts.UI;
using UnityEngine;

namespace _Project.Scripts.Creatures.Enemy
{
    public class Enemy : MonoBehaviour
    {
        public ScoreService _scoreService { get; private set; }

        public void Initialize(ScoreService scoreService)
        {
            _scoreService = scoreService;
        }
        
        public void DestroyEnemy()
        {
            DieEnemy();
            Destroy(gameObject);
        }
        
        private void DieEnemy()
        {
            _scoreService.AddScore();
        }
    }
}
