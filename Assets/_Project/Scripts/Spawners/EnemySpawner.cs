using System.Collections;
using _Project.Scripts.Creatures.Enemy;
using _Project.Scripts.Creatures.Factories;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Spawners
{
    public class EnemySpawner
    {
        private const int SIDES_SCREEN_COUNT = 4;
        private const float OFFSET_FOR_SPAWN_BEHIND_THE_SCREEN = 1f;
        
        protected WaitForSeconds _spawnInterval;
        protected SessionData _sessionData;
        protected BaseFactory<Enemy> _enemyFactory;
        
        private Enemy _enemyPrefab;
        private float _spawnOffset;
        private Camera _mainCamera;
        private float _cameraOffsetZ = 10f;
        private int _poolSize;

        public EnemySpawner(Enemy enemyPrefab, float spawnOffset, WaitForSeconds spawnInterval, Camera mainCamera, SessionData sessionData, int poolSize)
        {
            _enemyPrefab = enemyPrefab;
            _spawnOffset = spawnOffset;
            _spawnInterval = spawnInterval;
            _mainCamera = mainCamera;
            _sessionData = sessionData;
            _poolSize = poolSize;
        }

        public void SetSpawner()
        {
            _enemyFactory = new BaseFactory<Enemy>(_enemyPrefab, _poolSize);
            _enemyFactory.PoolInitialize();
        }
        
        public virtual IEnumerator SpawnEnemies()
        {
            while (true)
            {
                Vector3 screenPoint = CalculateCoordinatesBehindTheScreen();
                Enemy enemy = _enemyFactory.pool.Get();
                enemy.OnDisabled += _sessionData.AddKillEvent;
                enemy.transform.position = screenPoint;
                yield return _spawnInterval;
            }
        }

        protected Vector3 CalculateCoordinatesBehindTheScreen()
        {
            int side = Random.Range(0, SIDES_SCREEN_COUNT);
            Vector3 viewportPoint = Vector3.zero;

            switch (side)
            {
                case 0:
                    viewportPoint = new Vector3(Random.value, OFFSET_FOR_SPAWN_BEHIND_THE_SCREEN + _spawnOffset, _cameraOffsetZ);
                    break;
                case 1:
                    viewportPoint = new Vector3(OFFSET_FOR_SPAWN_BEHIND_THE_SCREEN + _spawnOffset, Random.value, _cameraOffsetZ);
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