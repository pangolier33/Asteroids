using System.Collections;
using _Project.Scripts.Services;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Creatures.Enemy
{
    public class EnemySpawner
    {
        private const int SIDES_SCREEN_COUNT = 4;
        private const float OFFSET_FOR_SPAWN_BEHIND_THE_SCREEN = 1f;
        
        private Enemy[] _enemyPrefabs;
        private float _spawnOffset;
        private ScoreService _scoreService;
        private Camera _mainCamera;
        private float _cameraOffsetZ = 10f;
        private WaitForSeconds _spawnInterval;

        public EnemySpawner(Enemy[] enemyPrefabs, float spawnOffset, WaitForSeconds spawnInterval, Camera mainCamera)
        {
            _enemyPrefabs = enemyPrefabs;
            _spawnOffset = spawnOffset;
            _spawnInterval = spawnInterval;
            _mainCamera = mainCamera;
        }
        
        public IEnumerator SpawnEnemies()
        {
            while (true)
            {
                Vector3 screenPoint = CalculateCoordinatesBehindTheScreen();
                int enemyIndex = Random.Range(0, _enemyPrefabs.Length);
                GameObject.Instantiate(_enemyPrefabs[enemyIndex].gameObject, screenPoint, Quaternion.identity);
                
                yield return _spawnInterval;
            }
        }

        private Vector3 CalculateCoordinatesBehindTheScreen()
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