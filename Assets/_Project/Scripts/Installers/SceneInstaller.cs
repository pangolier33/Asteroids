using _Project.Scripts.Creatures.Enemy;
using _Project.Scripts.Creatures.Player;
using _Project.Scripts.Creatures.Player.SpaceShipWeapon;
using _Project.Scripts.EntryPoints;
using _Project.Scripts.Factories;
using _Project.Scripts.Services;
using _Project.Scripts.Services.Addressebles;
using _Project.Scripts.Services.ScoreSystem;
using _Project.Scripts.Spawners;
using _Project.Scripts.Tools;
using _Project.Scripts.UI.HUD;
using _Project.Scripts.UI.RestartPanel;
using UnityEngine;
using Zenject;


namespace _Project.Scripts.Installers
{
    public class SceneInstaller : MonoInstaller
    { 
        [SerializeField] private Camera _mainCamera;

        private LevelAssetsStorage _storage;
        private IInstantiator _instantiator;
         
        [Inject]
        public void Construct(LevelAssetsStorage storage, IInstantiator instantiator)
        {
            _storage = storage;
            _instantiator = instantiator;
        }
        
        public override void InstallBindings()
        {
            BindCore();
            BindPools();
            BindAddressables();
            BindGameplay();
        }
        
        private void BindCore()
        {
            Container.Bind<Camera>()
                .FromInstance(_mainCamera)
                .AsSingle();

            Container.BindInterfacesTo<AddressableReferencesLoader>()
                .AsSingle();
        }

        private void BindAddressables()
        {
            var spaceship = new MonoBehFactory<SpaceShipMovement>(_storage.SpaceShipPrefab, _instantiator).Create();

            Container
                .Bind<SpaceShipMovement>()
                .FromInstance(spaceship.GetComponent<SpaceShipMovement>())
                .AsSingle();

            Container.BindInterfacesAndSelfTo<Bullet>()
                .FromInstance(_storage.BulletPrefab)
                .AsSingle();

            Container.BindInterfacesAndSelfTo<UFO>()
                .FromInstance(_storage.UfoPrefab)
                .AsSingle();

            Container.BindInterfacesAndSelfTo<Asteroid>()
                .FromInstance(_storage.AsteroidPrefab)
                .AsSingle();

            Container.BindInterfacesAndSelfTo<HUDModel>().AsSingle();
            
            var hud = new MonoBehFactory<HUDView>(_storage.HudPrefab, _instantiator).Create();
            
            Container.BindInterfacesAndSelfTo<HUDView>()
                .FromInstance(hud)
                .AsSingle();
            
            Container.BindInterfacesAndSelfTo<HUDPresenter>().AsSingle();
        }

        private void BindPools()
        {
            Container.Bind<EnemySpawnerSettings>().AsSingle();
            
            Container.BindMemoryPool<Bullet, Bullet.Pool>().WithInitialSize(10).FromComponentInNewPrefab(_storage.BulletPrefab).UnderTransformGroup("Bullets");

            Container.BindMemoryPool<UFO, UFO.Pool>().WithInitialSize(10).FromComponentInNewPrefab(_storage.UfoPrefab).UnderTransformGroup("Ufos");

            Container.BindMemoryPool<Asteroid, Asteroid.Pool>().WithInitialSize(10).FromComponentInNewPrefab(_storage.AsteroidPrefab).UnderTransformGroup("Asteroids");
        }

        private void BindGameplay()
        {
            Container.Bind<ScoreController>().AsSingle();

            Container.BindInterfacesAndSelfTo<RecordController>().AsSingle();

            Container.Bind<GameOverController>().AsSingle();

            Container.BindInterfacesAndSelfTo<LevelEntryPoint>().AsSingle();
            
            Container.Bind<UfoSpawner>().AsSingle();
            
            Container.Bind<AsteroidSpawner>().AsSingle();

            BindRestartPanel();
        }

        private void BindRestartPanel()
        {
            Container.BindInterfacesAndSelfTo<RestartPanelModel>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<RestartPanelPresenter>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<RestartPanelUIView>().FromInstance(_storage.RestartPanelPrefab).AsSingle();
        }
    }
}