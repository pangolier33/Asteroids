using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Creatures.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject[] _enemyPrefabs;
        [SerializeField] private float _spawnOffset;
        [SerializeField] private float _spawnIntervalValue = 5f;

        private SpaceShipDeath _spaceShipDeath;
        private Camera _mainCamera;
        private float _cameraOffsetZ = 10f;
        private int _sidesScreenCount = 4;
        private WaitForSeconds _spawnInterval;

        private void Start()
        {
            _mainCamera = Camera.main;
            _spaceShipDeath = GameObject.FindWithTag("SpaceShip")?.GetComponent<SpaceShipDeath>();
            _spawnInterval = new WaitForSeconds(_spawnIntervalValue);
        }

        public IEnumerator SpawnEnemies()
        {
            while (true)
            {
                Vector3 screenPoint = CalculateCoordinatesBehindTheScreen();
                int enemyIndex = Random.Range(0, _enemyPrefabs.Length);
                GameObject enemy = Instantiate(_enemyPrefabs[enemyIndex], screenPoint, Quaternion.identity);
                enemy.GetComponent<Enemy>().Initialize(_spaceShipDeath);
                
                yield return _spawnInterval;
            }
        }

        private Vector3 CalculateCoordinatesBehindTheScreen()
        {
            int side = Random.Range(0, _sidesScreenCount);

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

            return Camera.main.ViewportToWorldPoint(viewportPoint);
        }
    }
}
