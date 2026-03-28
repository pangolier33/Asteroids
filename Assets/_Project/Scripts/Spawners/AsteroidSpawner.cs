using System;
using System.Threading;
using _Project.Scripts.Creatures.Enemy;
using _Project.Scripts.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Spawners
{
    public class AsteroidSpawner : EnemySpawner
    {
        private Enemy _enemy;
        private int _asteroidSmallCount = 2;
        private float _spawnOffset = 0.5f;
        private float _asteroidSmallScale = 0.5f;

        public AsteroidSpawner(Enemy enemyPrefab, float spawnOffset, float spawnInterval, Camera mainCamera, SessionDataManager sessionDataManager, int poolSize) : base(enemyPrefab, spawnOffset, spawnInterval, mainCamera, sessionDataManager, poolSize)
        {
            
        }

        public override async UniTask SpawnEnemies(CancellationToken token)
        {
            while (SessionDataManager.IsGameOver == false && !token.IsCancellationRequested)
            {
                Vector3 screenPoint = CalculateCoordinatesBehindTheScreen();
                Enemy enemy = _enemyFactory.GetPooledObject();
                
                enemy.OnDied += HandleEnemyDied;
            
                enemy.transform.position = screenPoint;
                await UniTask.Delay(TimeSpan.FromSeconds(_spawnInterval), cancellationToken: token);
            }
        }

        public override void HandleEnemyDied(Enemy enemy)
        {
            SessionDataManager.AddKillAsteroidEvent();
            _enemyFactory.ReturnAction(enemy);
            
            Asteroid asteroid = enemy.GetComponent<Asteroid>();
            if (asteroid != null && asteroid.isParentObject)
            {
                SpawnSmallAsteroids(enemy.transform.position);
            } 
        }

        private void SpawnSmallAsteroids(Vector3 spawnPosition)
        {
            for (int i = 0; i < _asteroidSmallCount; i++)
            {
                Vector2 direction = Random.insideUnitCircle.normalized;
                Vector2 spawnPos = (Vector2)spawnPosition + direction * _spawnOffset;
            
                Enemy smallAsteroid = _enemyFactory.GetPooledObject();
            
                Asteroid smallAsteroidComp = smallAsteroid.GetComponent<Asteroid>();
                if (smallAsteroidComp != null)
                {
                    smallAsteroidComp.isParentObject = false;
                }
                smallAsteroid.OnDied += HandleSmallAsteroidDied;
            
                smallAsteroid.transform.localScale = new Vector3(_asteroidSmallScale, _asteroidSmallScale, 1);
                smallAsteroid.transform.position = spawnPos;
            }
        }

        private void HandleSmallAsteroidDied(Enemy enemy)
        {
            SessionDataManager.AddKillAsteroidEvent();
            _enemyFactory.ReturnAction(enemy);
        }
    }
}