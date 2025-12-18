using _Project.Scripts.Creatures.Enemy;
using _Project.Scripts.Creatures.Player;
using _Project.Scripts.Services;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Installers
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private SpaceShipMovement _spaceShipPrefab;
        [SerializeField] private SessionDataManager _sessionDataManager;
        [SerializeField] private Canvas _hudPrefab;
        [SerializeField] private Enemy _ufoPrefab;
        [SerializeField] private Enemy _asteroidPrefab;
        
        public override void InstallBindings()
        {
            Container.Bind<SaveService>().AsSingle();
            Container.Bind<AnalyticsService>().AsSingle();
            
            Container.Bind<Camera>().FromMethod(ctx => Camera.main).AsSingle();
            
            Container.Bind<Enemy>().WithId("UfoPrefab").FromInstance(_ufoPrefab);
            Container.Bind<Enemy>().WithId("AsteroidPrefab").FromInstance(_asteroidPrefab);
            
            CreateGameObjects();
        }
        
        private void CreateGameObjects()
        {
            _spaceShipPrefab = Instantiate(_spaceShipPrefab);
            Container.Bind<SpaceShipMovement>().FromInstance(_spaceShipPrefab).AsSingle();
            
            _hudPrefab = Instantiate(_hudPrefab);
            Container.Bind<Canvas>().FromInstance(_hudPrefab.GetComponent<Canvas>()).AsSingle();
            
            _sessionDataManager = Instantiate(_sessionDataManager);
            Container.Bind<SessionDataManager>().FromInstance(_sessionDataManager.GetComponent<SessionDataManager>()).AsSingle();
            
            Container.Bind<EntryPoint>().FromComponentInHierarchy().AsSingle();
        }
    }
}