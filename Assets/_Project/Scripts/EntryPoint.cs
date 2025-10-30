using System;
using _Project.Scripts.Creatures.Enemy;
using _Project.Scripts.Creatures.Player;
using _Project.Scripts.Creatures.Player.SpaceShipWeapon;
using _Project.Scripts.Spawners;
using _Project.Scripts.UI;
using UnityEngine;

namespace _Project.Scripts
{
    public class EntryPoint : MonoBehaviour
    {
        [Header("Objects to instantiate")]
        [SerializeField] private Canvas _hud;
        [SerializeField] private SpaceShipMovement _spaceShip;
        [SerializeField] private SessionData _sessionData;
        
        [Header("Objects to bind enemy spawner")]
        [SerializeField] private Enemy _ufoPrefab;
        [SerializeField] private Enemy _asteroidPrefab;
        [SerializeField] private float _spawnOffset;
        [SerializeField] private float _spawnIntervalValue = 5f;
        [SerializeField] private int _poolSize = 10;
        
        private EnemySpawner _ufoSpawner;
        private EnemySpawner _asteroidSpawner;
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
            _sessionData = Instantiate(_sessionData);
            _spaceShip.GetComponent<SpaceShipDied>().Inizialize(_sessionData);
            BindSpawners();
            BindHud();
            StartCoroutine(_ufoSpawner.SpawnEnemies());
            StartCoroutine(_asteroidSpawner.SpawnEnemies());
        }

        private void BindHud()
        {
            HUD hudComponent = _hud.GetComponent<HUD>();
            SpaceShipLaser spaceShipLaser = _spaceShip.GetComponent<SpaceShipLaser>();
            hudComponent.Initialize(_spaceShip, spaceShipLaser);
            _hud.worldCamera = _mainCamera;
        }

        private void BindSpawners()
        {
            _spawnInterval = new WaitForSeconds(_spawnIntervalValue);
            _ufoSpawner = new EnemySpawner(_ufoPrefab, _spawnOffset, _spawnInterval, _mainCamera, _sessionData, _poolSize);
            _ufoSpawner.SetSpawner();
            
            _asteroidSpawner = new AsteroidSpawner(_asteroidPrefab, _spawnOffset, _spawnInterval, _mainCamera, _sessionData, _poolSize);
            _asteroidSpawner.SetSpawner();
        }
    }
}
