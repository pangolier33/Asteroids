using _Project.Scripts.Creatures.Enemy;
using _Project.Scripts.Creatures.Player;
using _Project.Scripts.Creatures.Player.SpaceShipWeapon;
using _Project.Scripts.Services;
using _Project.Scripts.UI;
using UnityEngine;

namespace _Project.Scripts
{
    public class EntryPoint : MonoBehaviour
    {
        [Header("Objects to instantiate")]
        [SerializeField] private Canvas _hud;
        [SerializeField] private SpaceShipMovement _spaceShip;
        
        [Header("Objects to bind enemy spawner")]
        [SerializeField] private Enemy[] _enemyPrefabs;
        [SerializeField] private float _spawnOffset;
        [SerializeField] private float _spawnIntervalValue = 5f;
        
        private EnemySpawner _enemySpawner;
        private Camera _mainCamera;
        private WaitForSeconds _spawnInterval;

        private void Start()
        {
            BindObjects();
        }

        private void BindObjects()
        {
            _mainCamera = Camera.main;
            _spaceShip = Instantiate(_spaceShip);
            _hud = Instantiate(_hud);
            BindHud();
            _spawnInterval = new WaitForSeconds(_spawnIntervalValue);
            _enemySpawner = new EnemySpawner(_enemyPrefabs, _spawnOffset, _spawnInterval, _mainCamera);
            
            StartCoroutine(_enemySpawner.SpawnEnemies());
        }

        private void BindHud()
        {
            HUD hudComponent = _hud.GetComponent<HUD>();
            SpaceShipWeapon spaceShipWeapon = _spaceShip.GetComponent<SpaceShipWeapon>();
            hudComponent.Initialize(_spaceShip, spaceShipWeapon);
            _hud.worldCamera = _mainCamera;
        }
    }
}
