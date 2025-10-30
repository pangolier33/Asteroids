using System;
using UnityEngine;

namespace _Project.Scripts
{
    public class SessionData : MonoBehaviour
    {
        public Action OnGameOver;
        private int _enemyScore = 1;
        
        [field: SerializeField] public int EnemyKilledScore { get; private set; }
        public bool IsGameOver { get; private set; }

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