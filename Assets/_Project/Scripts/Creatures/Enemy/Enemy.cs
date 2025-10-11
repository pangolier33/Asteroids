using UnityEngine;

namespace _Project.Scripts.Creatures.Enemy
{
    public class Enemy : MonoBehaviour
    {
        private int _enemyScoreValue = 1;
        private Health.Health _health;

        protected virtual void OnEnable()
        {
            _health = GetComponent<Health.Health>();
            _health.OnDie += DestroyEnemy;
        }

        private void OnDisable()
        {
            _health.OnDie -= DestroyEnemy;
        }

        protected virtual void DestroyEnemy()
        {
            IncrementScore();
            Destroy(gameObject);
        }
        
        private void IncrementScore()
        {
            int score = PlayerPrefs.GetInt("CurrentScore");
            PlayerPrefs.SetInt("CurrentScore", score + _enemyScoreValue);
        }
    }
}
