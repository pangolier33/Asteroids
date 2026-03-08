using _Project.Scripts.Creatures.Enemy;
using _Project.Scripts.Creatures.Player;
using _Project.Scripts.Creatures.Player.SpaceShipWeapon;
using _Project.Scripts.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Services.Addressebles
{
    public interface IAddressableReferencesLoader
    {
        UniTask<HUD> CreateHUD();
        void ReleaseHUD();

        UniTask<SpaceShipMovement> CreateSpaceShip();
        void ReleaseSpaceShip();
        
        UniTask<Enemy> CreateUFO();
        void ReleaseUFO();

        UniTask<Enemy> CreateAsteroid();
        void ReleaseAsteroid();

        UniTask<Canvas> CreateLoadingScreen();
        void ReleaseLoadingScreen();

        UniTask<RestartPanelUI> CreateRestartPanelUI();
        void ReleaseRestartPanelUI();

        UniTask<Bullet> CreateBullet();
        void ReleaseBullet();

        void ReleaseAllAssets();
    }
}