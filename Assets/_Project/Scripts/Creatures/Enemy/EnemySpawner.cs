using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Creatures.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject[] _enemyPrefabs;
        [SerializeField] private float _spawnOffset;
        [SerializeField] private float _spawnInterval;

        private SpaceShipDeath _spaceShipDeath;

        private void Start()
        {
            _spaceShipDeath = GameObject.FindWithTag("SpaceShip")?.GetComponent<SpaceShipDeath>();
        }

        public IEnumerator SpawnEnemies()
        {
            while (true)
            {
                Vector3 screenPoint = CalculateCoordinatesBehindTheScreen();
                int enemyIndex = Random.Range(0, _enemyPrefabs.Length);
                GameObject enemy = Instantiate(_enemyPrefabs[enemyIndex], screenPoint, Quaternion.identity);
                enemy.GetComponent<Enemy>().Initialize(_spaceShipDeath);
                
                yield return new WaitForSeconds(_spawnInterval);
            }
        }

        private Vector3 CalculateCoordinatesBehindTheScreen()
        {
            int side = Random.Range(0, 4);

            Vector3 viewportPoint = Vector3.zero;

            switch (side)
            {
                case 0:
                    viewportPoint = new Vector3(Random.Range(0f, 1f), 1f + _spawnOffset, 10f);
                    break;
                case 1:
                    viewportPoint = new Vector3(1f + _spawnOffset, Random.Range(0f, 1f), 10f);
                    break;
                case 2:
                    viewportPoint = new Vector3(Random.Range(0f, 1f), -_spawnOffset, 10f);
                    break;
                case 3:
                    viewportPoint = new Vector3(-_spawnOffset, Random.Range(0f, 1f), 10f);
                    break;
            }

            return Camera.main.ViewportToWorldPoint(viewportPoint);
        }
    }
}
