using System;
using System.Threading;
using _Project.Scripts.Creatures.Enemy;
using _Project.Scripts.Services.ScoreSystem;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Spawners
{
    public class EnemySpawner<TEnemy> where TEnemy : Enemy
    {
        private const int SIDES_SCREEN_COUNT = 4;
        private const float OFFSET_FOR_SPAWN_BEHIND_THE_SCREEN = 1f;

        protected readonly MonoMemoryPool<TEnemy> _pool;
        protected readonly Camera _mainCamera;
        protected readonly GameOverController _gameOverController;
        protected readonly ScoreController _scoreController;
        protected TEnemy _enemy;

        protected readonly float _spawnInterval;
        protected readonly float _spawnOffset;
        
        private float _cameraOffsetZ = 10f;

        protected EnemySpawner(
            MonoMemoryPool<TEnemy> pool,
            Camera mainCamera,
            GameOverController gameOverController,
            ScoreController scoreController,
            float spawnOffset,
            float spawnInterval)
        {
            _pool = pool;
            _mainCamera = mainCamera;
            _gameOverController = gameOverController;
            _scoreController = scoreController;

            _spawnOffset = spawnOffset;
            _spawnInterval = spawnInterval;
        }

        public virtual async UniTask SpawnEnemies(CancellationToken token)
        {
            while (!_gameOverController.IsGameOver &&
                   !token.IsCancellationRequested)
            {
                SpawnEnemy();

                await UniTask.Delay(
                    TimeSpan.FromSeconds(_spawnInterval),
                    cancellationToken: token);
            }
        }

        protected virtual void SpawnEnemy()
        {
            _enemy = _pool.Spawn();
            _enemy.transform.position = CalculateCoordinatesBehindTheScreen();
            _enemy.OnDied += HandleEnemyDied;
        }

        protected virtual void HandleEnemyDied(Enemy enemy)
        {
            enemy.OnDied -= HandleEnemyDied;

            _pool.Despawn((TEnemy)enemy);
        }

        protected Vector3 CalculateCoordinatesBehindTheScreen()
        {
            int side = Random.Range(0, SIDES_SCREEN_COUNT);
            Vector3 viewportPoint = Vector3.zero;

            switch (side)
            {
                case 0:
                    viewportPoint = new Vector3(Random.value, OFFSET_FOR_SPAWN_BEHIND_THE_SCREEN + _spawnOffset,
                        _cameraOffsetZ);
                    break;
                case 1:
                    viewportPoint = new Vector3(OFFSET_FOR_SPAWN_BEHIND_THE_SCREEN + _spawnOffset, Random.value,
                        _cameraOffsetZ);
                    break;
                case 2:
                    viewportPoint = new Vector3(Random.value, -_spawnOffset, _cameraOffsetZ);
                    break;
                case 3:
                    viewportPoint = new Vector3(-_spawnOffset, Random.value, _cameraOffsetZ);
                    break;
            }

            return _mainCamera.ViewportToWorldPoint(viewportPoint);
        }
    }
}