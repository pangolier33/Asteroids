using _Project.Scripts.Creatures.Enemy;
using _Project.Scripts.Creatures.Player;
using _Project.Scripts.Creatures.Player.SpaceShipWeapon;
using _Project.Scripts.Enums;
using _Project.Scripts.Services;
using _Project.Scripts.Spawners;
using _Project.Scripts.UI;
using UnityEngine;
using Zenject;

namespace _Project.Scripts
{
    public class EntryPoint : MonoBehaviour
    {
        [Header("Parameters to bind enemy spawner")]
        [SerializeField] private float _spawnOffset = 5f;
        [SerializeField] private float _spawnIntervalValue = 5f;
        [SerializeField] private int _poolSize = 10;
        
        private SaveService _saveService;
        private Camera _mainCamera;
        private SpaceShipMovement _spaceShip;
        private Canvas _hud;
        private SessionDataManager _sessionDataManager;
        private AnalyticsController _analyticsController;
        private Enemy _ufoPrefab;
        private Enemy _asteroidPrefab;
        private AnalyticsService _analyticsService;
        
        private EnemySpawner _ufoSpawner;
        private EnemySpawner _asteroidSpawner;
        private WaitForSeconds _spawnInterval;

        [Inject]
        public void Construct(
            SaveService saveService,
            Camera mainCamera,
            SpaceShipMovement spaceShip,
            Canvas hud,
            SessionDataManager sessionDataManager,
            AnalyticsService analyticsService,
            [Inject(Id = ZenjectIDs.UfoPrefab)] Enemy ufoPrefab,
            [Inject(Id = ZenjectIDs.AsteroidPrefab)] Enemy asteroidPrefab)
        {
            _saveService = saveService;
            _mainCamera = mainCamera;
            _spaceShip = spaceShip;
            _hud = hud;
            _sessionDataManager = sessionDataManager;
            _analyticsService = analyticsService;
            _ufoPrefab = ufoPrefab;
            _asteroidPrefab = asteroidPrefab;
        }

        private void Start()
        {
            BindObjects();
        }

        private void BindObjects()
        {
            _sessionDataManager.Initialize(_saveService);
                
            var spaceShipDied = _spaceShip.GetComponent<SpaceShipDied>();
            var spaceShipGun = _spaceShip.GetComponent<SpaceShipGun>();
            var spaceShipLaser = _spaceShip.GetComponent<SpaceShipLaser>();
                
            spaceShipDied.Initialize(_sessionDataManager);
                
            _analyticsController = new AnalyticsController(
                _analyticsService,
                _sessionDataManager,
                spaceShipGun,
                spaceShipLaser);
                
            _analyticsController.Initialize();
                
            BindHud(spaceShipLaser);
                
            BindSpawners();
                
            StartCoroutine(_ufoSpawner.SpawnEnemies());
            StartCoroutine(_asteroidSpawner.SpawnEnemies());
        }

        private void BindHud(SpaceShipLaser spaceShipLaser)
        {
            HUD hudComponent = _hud.GetComponent<HUD>();
            hudComponent.Initialize(_spaceShip, spaceShipLaser);
            _hud.worldCamera = _mainCamera;
        }

        private void BindSpawners()
        {
            _spawnInterval = new WaitForSeconds(_spawnIntervalValue);
            
            _ufoSpawner = new EnemySpawner(
                _ufoPrefab, 
                _spawnOffset, 
                _spawnInterval, 
                _mainCamera, 
                _sessionDataManager, 
                _poolSize);
            _ufoSpawner.SetSpawner();
            
            _asteroidSpawner = new AsteroidSpawner(
                _asteroidPrefab, 
                _spawnOffset, 
                _spawnInterval, 
                _mainCamera, 
                _sessionDataManager, 
                _poolSize);
            _asteroidSpawner.SetSpawner();
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
            _analyticsController?.Dispose();
        }
    }
}
