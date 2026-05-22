using _Project.Scripts.Creatures.Enemy;
using _Project.Scripts.Creatures.Player;
using _Project.Scripts.Creatures.Player.SpaceShipWeapon;
using _Project.Scripts.UI;
using _Project.Scripts.UI.HUD;
using _Project.Scripts.UI.RestartPanel;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Services.Addressebles
{
    public interface IAddressableReferencesLoader
    {
        UniTask<HUDView> CreateHUD();
        void ReleaseHUD();

        UniTask<SpaceShipMovement> CreateSpaceShip();
        void ReleaseSpaceShip();
        
        UniTask<UFO> CreateUFO();
        void ReleaseUFO();

        UniTask<Asteroid> CreateAsteroid();
        void ReleaseAsteroid();

        UniTask<Canvas> CreateLoadingScreen();
        void ReleaseLoadingScreen();

        UniTask<RestartPanelUIView> CreateRestartPanelUI();
        void ReleaseRestartPanelUI();

        UniTask<Bullet> CreateBullet();
        void ReleaseBullet();

        void ReleaseAllAssets();
    }
}