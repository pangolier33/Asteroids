using _Project.Scripts.Creatures.Enemy;
using _Project.Scripts.Creatures.Player;
using _Project.Scripts.Creatures.Player.SpaceShipWeapon;
using _Project.Scripts.EntryPoints;
using _Project.Scripts.Enums;
using _Project.Scripts.Services;
using _Project.Scripts.Services.Addressebles;
using _Project.Scripts.Tools;
using _Project.Scripts.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;
using Zenject;

namespace _Project.Scripts.Installers
{
    public class SceneInstaller : MonoInstaller
    {
        [Inject] private LevelPrefabs _levelPrefabs;

        private SpaceShipMovement _spaceShip;
        private HUD _hud;
        private SessionDataManager _sessionDataManager;
        private RestartPanelUI _restartPanelUI;
        private Enemy _ufo;
        private Enemy _asteroid;
        private Bullet _bullet;
    
        public override void InstallBindings()
        {
            BindGameObjectsAsync().Forget();
        }

        private async UniTaskVoid BindGameObjectsAsync()
        {
            Container.Bind<Camera>().FromMethod(ctx => Camera.main).AsSingle();
            
            await BindPrefabs();
            
            Container.BindInterfacesAndSelfTo<LevelEntryPoint>().AsSingle().NonLazy();
        }

        private async UniTask BindPrefabs()
        {
            _spaceShip = _levelPrefabs.spaceShipPrefab;
            _hud = _levelPrefabs.hudPrefab;
            _sessionDataManager = _levelPrefabs.sessionDataManagerPrefab;
            _asteroid = _levelPrefabs.asteroidPrefab;
            _ufo = _levelPrefabs.ufoPrefab;
            _bullet = _levelPrefabs.bulletPrefab;
            _restartPanelUI = _levelPrefabs.restartPanelUIPrefab;

            Container.Bind<SpaceShipMovement>().FromInstance(_spaceShip.GetComponent<SpaceShipMovement>()).AsSingle();
            Container.Bind<HUD>().FromInstance(_hud.GetComponent<HUD>()).AsSingle();
            Container.Bind<SessionDataManager>().FromInstance(_sessionDataManager.GetComponent<SessionDataManager>()).AsSingle();
            Container.Bind<Enemy>().WithId(ZenjectIDs.UfoPrefab).FromInstance(_ufo);
            Container.Bind<Enemy>().WithId(ZenjectIDs.AsteroidPrefab).FromInstance(_asteroid);
            
            _spaceShip.GetComponent<SpaceShipGun>().Construct(_bullet);
            _spaceShip.GetComponent<SpaceShipDied>().Construct(_restartPanelUI);
        }
    }
}