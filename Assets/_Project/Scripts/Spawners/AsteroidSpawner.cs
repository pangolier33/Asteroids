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
            while (true)
            {
                Vector3 screenPoint = CalculateCoordinatesBehindTheScreen();
                _enemy = _enemyFactory.pool.Get();
                _enemy.OnDisabled += _sessionData.AddKillEvent;
                _enemy.OnDisabled += SpawnSmallAsteroid;
                _enemy.transform.position = screenPoint;
                yield return _spawnInterval;
            }
        }
        private void SpawnSmallAsteroid()
        {
            Asteroid asteroid = _enemy.GetComponent<Asteroid>();
            
            if (asteroid.isParentObject)
            {
                for (int i = 0; i < asteroidSmallCount; i++)
                {
                    Vector2 direction = Random.insideUnitCircle.normalized;
                    Vector2 spawnPos = (Vector2)_enemy.transform.position + direction * _spawnOffset;
                    
                    Enemy smallAsteroid = _enemyFactory.pool.Get();
                    smallAsteroid.GetComponent<Asteroid>().isParentObject = false;
                    smallAsteroid.OnDisabled += _sessionData.AddKillEvent;
                    
                    smallAsteroid.transform.localScale = new Vector3(_asteroidSmallScale, _asteroidSmallScale, 1);
                    smallAsteroid.transform.position = spawnPos;
                }
            }
        }
    }
}