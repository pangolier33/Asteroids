using _Project.Scripts.Creatures.Enemy;
using _Project.Scripts.Services;
using _Project.Scripts.Services.ScoreSystem;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Spawners
{
    public class UfoSpawner : EnemySpawner
    { public UfoSpawner(Enemy enemyPrefab, float spawnOffset, float spawnInterval, Camera mainCamera,
            GameOverController gameOverController,  ScoreController scoreController, IInstantiator instantiator, int poolSize)
            : base(enemyPrefab, spawnOffset, spawnInterval, mainCamera, gameOverController, scoreController, instantiator, poolSize)
        {
        }
    
        public override void HandleEnemyDied(Enemy enemy)
        {
            enemy.OnDied -= HandleEnemyDied;
            _scoreController.AddUfoKill();
            _enemyFactory.ReturnAction(enemy);
        }
    }
}