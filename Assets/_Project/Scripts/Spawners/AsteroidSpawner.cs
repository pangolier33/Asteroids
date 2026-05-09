using _Project.Scripts.Creatures.Enemy;
using _Project.Scripts.Services.ScoreSystem;
using _Project.Scripts.Tools;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Spawners
{
    public class AsteroidSpawner : EnemySpawner<Asteroid>
    {
        private const int ASTEROID_SMALL_COUNT = 2;
        private const float SMALL_ASTEROID_SCALE = 0.5f;
        private const float SMALL_ASTEROID_OFFSET = 0.5f;

        public AsteroidSpawner(Asteroid.Pool pool, Camera mainCamera, GameOverController gameOverController, ScoreController scoreController, EnemySpawnerSettings settings)
            : base(pool, mainCamera, gameOverController, scoreController, settings.SpawnOffset, settings.SpawnInterval)
        {
        }

        protected override void HandleEnemyDied(Enemy enemy)
        {
            enemy.OnDied -= HandleEnemyDied;
            _scoreController.AddAsteroidKill();
            Asteroid asteroid = (Asteroid)enemy;

            if (asteroid.isParentObject)
            {
                SpawnSmallAsteroids(asteroid.transform.position);
            }

            _pool.Despawn(asteroid);
        }

        private void SpawnSmallAsteroids(Vector3 spawnPosition)
        {
            for (int i = 0; i < ASTEROID_SMALL_COUNT; i++)
            {
                Vector2 direction = Random.insideUnitCircle.normalized;
                Vector2 spawnPos = (Vector2)spawnPosition + direction * SMALL_ASTEROID_OFFSET;
                Asteroid asteroid = _pool.Spawn();
                asteroid.isParentObject = false;
                asteroid.transform.position = spawnPos;
                asteroid.transform.localScale = Vector3.one * SMALL_ASTEROID_SCALE;
                asteroid.OnDied += HandleSmallAsteroidDied;
            }
        }

        private void HandleSmallAsteroidDied(Enemy enemy)
        {
            enemy.OnDied -= HandleSmallAsteroidDied;
            _scoreController.AddAsteroidKill();
            _pool.Despawn((Asteroid)enemy);
        }
    }
}