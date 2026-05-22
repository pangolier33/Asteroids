using _Project.Scripts.Creatures.Enemy;
using _Project.Scripts.Creatures.Player;
using _Project.Scripts.Services.ScoreSystem;
using _Project.Scripts.Tools;
using UnityEngine;

namespace _Project.Scripts.Spawners
{
    public class UfoSpawner : EnemySpawner<UFO>
    {
        public UfoSpawner(UFO.Pool pool, Camera mainCamera, GameOverController gameOverController, ScoreController scoreController, EnemySpawnerSettings settings) 
            : base(pool, mainCamera, gameOverController, scoreController, settings.SpawnOffset, settings.SpawnInterval)
        {
        }
        
        protected override void SpawnEnemy()
        {
            _enemy = _pool.Spawn();
            _enemy.transform.position = CalculateCoordinatesBehindTheScreen();
            _enemy.OnDied += HandleEnemyDied;
        }

        protected override void HandleEnemyDied(Enemy enemy)
        {
            enemy.OnDied -= HandleEnemyDied;
            _scoreController.AddUfoKill();
            _pool.Despawn((UFO)enemy);
        }
    }
}