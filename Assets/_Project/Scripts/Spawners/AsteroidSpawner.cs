using System.Collections;
using _Project.Scripts.Creatures.Enemy;
using UnityEngine;

namespace _Project.Scripts.Spawners
{
    public class AsteroidSpawner : EnemySpawner
    {
        private Enemy _enemy;
        private int asteroidSmallCount = 2;
        private float _spawnOffset = 0.5f;
        private float _asteroidSmallScale = 0.5f;

        public AsteroidSpawner(Enemy enemyPrefab, float spawnOffset, WaitForSeconds spawnInterval, Camera mainCamera, SessionData sessionData, int poolSize) : base(enemyPrefab, spawnOffset, spawnInterval, mainCamera, sessionData, poolSize)
        {
            
        }

        public override IEnumerator SpawnEnemies()
        {
            while (_sessionData.IsGameOver == false)
            {
                Vector3 screenPoint = CalculateCoordinatesBehindTheScreen();
                Enemy enemy = _enemyFactory.GetPooledObject();
                
                enemy.OnDied += HandleEnemyDied;
            
                enemy.transform.position = screenPoint;
                yield return _spawnInterval;
            }
        }

        private void HandleEnemyDied(Enemy enemy)
        {
            _sessionData.AddKillEvent();
            _enemyFactory.ReturnAction(enemy);
            
            Asteroid asteroid = enemy.GetComponent<Asteroid>();
            if (asteroid != null && asteroid.isParentObject)
            {
                SpawnSmallAsteroids(enemy.transform.position);
            } 
        }

        private void SpawnSmallAsteroids(Vector3 spawnPosition)
        {
            for (int i = 0; i < asteroidSmallCount; i++)
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
            _sessionData.AddKillEvent();
            _enemyFactory.ReturnAction(enemy);
        }
    }
}