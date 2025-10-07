using System.Collections;
using _Project.Scripts.Services;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Creatures.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        private const int SIDES_SCREEN_COUNT = 4;
        
        [SerializeField] private Enemy[] _enemyPrefabs;
        [SerializeField] private float _spawnOffset;
        [SerializeField] private float _spawnIntervalValue = 5f;

        private ScoreService _scoreService;
        private Camera _mainCamera;
        private float _cameraOffsetZ = 10f; // Изменил на 10f, так как 0 может быть перед камерой
        private WaitForSeconds _spawnInterval;

        private void Start()
        {
            _mainCamera = Camera.main;
            _spawnInterval = new WaitForSeconds(_spawnIntervalValue);
        }

        public void Initialize(ScoreService scoreService)
        {
            _scoreService = scoreService;
        }
        
        public IEnumerator SpawnEnemies()
        {
            // Проверка перед началом спавна
            if (_scoreService == null)
            {
                Debug.LogError("ScoreService not initialized in EnemySpawner!");
                yield break;
            }

            if (_enemyPrefabs == null || _enemyPrefabs.Length == 0)
            {
                Debug.LogError("No enemy prefabs assigned!");
                yield break;
            }

            while (true)
            {
                Vector3 screenPoint = CalculateCoordinatesBehindTheScreen();
                int enemyIndex = Random.Range(0, _enemyPrefabs.Length);
                
                GameObject enemy = Instantiate(_enemyPrefabs[enemyIndex].gameObject, screenPoint, Quaternion.identity);
                enemy.GetComponent<Enemy>().Initialize(_scoreService);
                
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
                    viewportPoint = new Vector3(Random.value, 1f + _spawnOffset, _cameraOffsetZ);
                    break;
                case 1:
                    viewportPoint = new Vector3(1f + _spawnOffset, Random.value, _cameraOffsetZ);
                    break;
                case 2:
                    viewportPoint = new Vector3(Random.value, -_spawnOffset, _cameraOffsetZ);
                    break;
                case 3:
                    viewportPoint = new Vector3(-_spawnOffset, Random.value, _cameraOffsetZ);
                    break;
            }
            
            if (_mainCamera != null)
            {
                return _mainCamera.ViewportToWorldPoint(viewportPoint);
            }
            else
            {
                Debug.LogWarning("Main camera is null");
                return Camera.main.ViewportToWorldPoint(viewportPoint);
            }
        }
    }
}