using System;
using UnityEngine;

namespace _Project.Scripts
{
    public class SessionData : MonoBehaviour
    {
        public Action OnGameOver;
        
        [field: SerializeField] public int EnemyKilledScore { get; private set; }
        public bool IsGameOver;

        private int _enemyScore = 1;

        public void AddKillEvent()
        {
            EnemyKilledScore += _enemyScore;
        }

        public void GameOverEvent()
        {
            IsGameOver = true;

            if (OnGameOver != null) 
                OnGameOver();
        }
    }
}