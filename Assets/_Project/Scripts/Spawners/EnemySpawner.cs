using System;
using System.Threading;
using _Project.Scripts.Creatures.Enemy;
using _Project.Scripts.Factories;
using _Project.Scripts.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Spawners
{
    public class EnemySpawner
    {
        private const int SIDES_SCREEN_COUNT = 4;
        private const float OFFSET_FOR_SPAWN_BEHIND_THE_SCREEN = 1f;
        
        protected Enemy _enemyPrefab;
        protected BaseFactory<Enemy> _enemyFactory;
        protected float _spawnInterval;
        protected SessionDataManager SessionDataManager;

        private int _poolSize;
        private float _spawnOffset;
        protected Camera _mainCamera;
        private float _cameraOffsetZ = 10f;
        

        public EnemySpawner(Enemy enemyPrefab, float spawnOffset, float spawnInterval, Camera mainCamera,
            SessionDataManager sessionDataManager, int poolSize)
        {
            _enemyPrefab = enemyPrefab;
            _spawnOffset = spawnOffset;
            _spawnInterval = spawnInterval;
            _mainCamera = mainCamera;
            SessionDataManager = sessionDataManager;
            _poolSize = poolSize;
        }

        public void SetSpawner()
        {
            _enemyFactory = new BaseFactory<Enemy>(_enemyPrefab, _poolSize);
            _enemyFactory.PoolInitialize();
            if (SessionDataManager == null) Debug.Log("SessionDataManager == null");
        }
        
        public virtual async UniTask SpawnEnemies(CancellationToken token)
        {
            while (SessionDataManager.IsGameOver == false)
            {
                Vector3 screenPoint = CalculateCoordinatesBehindTheScreen();
                Enemy enemy = _enemyFactory.GetPooledObject();
                enemy.OnDied += HandleEnemyDied;
                enemy.transform.position = screenPoint;
                await UniTask.Delay(TimeSpan.FromSeconds(_spawnInterval), cancellationToken: token);
            }
        }

        public virtual void HandleEnemyDied(Enemy enemy)
        {
            _enemyFactory.ReturnAction(enemy);
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