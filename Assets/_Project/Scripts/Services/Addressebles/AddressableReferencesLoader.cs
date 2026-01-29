using _Project.Scripts.Creatures.Enemy;
using _Project.Scripts.Creatures.Player;
using _Project.Scripts.Creatures.Player.SpaceShipWeapon;
using _Project.Scripts.Enums;
using _Project.Scripts.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Services.Addressebles
{
    public class AddressableReferencesLoader : AddressableLoader, IAddressableReferencesLoader
    {
        public UniTask<HUD> CreateHUD() => LoadAssetComponent<HUD>(AddreseblesIDs.HUD.ToString());
        public void ReleaseHUD() => ReleaseAsset(AddreseblesIDs.HUD.ToString());
        
        public UniTask<SpaceShipMovement> CreateSpaceShip() => LoadAssetComponent<SpaceShipMovement>(AddreseblesIDs.SpaceShip.ToString());
        public void ReleaseSpaceShip() => ReleaseAsset(AddreseblesIDs.SpaceShip.ToString());
        
        public UniTask<SessionDataManager> CreateSessionDataManager() => LoadAssetComponent<SessionDataManager>(AddreseblesIDs.SessionDataManager.ToString());
        public void ReleaseSessionDataManager() => ReleaseAsset(AddreseblesIDs.SessionDataManager.ToString());
        
        public UniTask<Enemy> CreateUFO() => LoadAssetComponent<Enemy>(AddreseblesIDs.UFO.ToString());
        public void ReleaseUFO() => ReleaseAsset(AddreseblesIDs.UFO.ToString());
        
        public UniTask<Enemy> CreateAsteroid() => LoadAssetComponent<Enemy>(AddreseblesIDs.Asteroid.ToString());
        public void ReleaseAsteroid() => ReleaseAsset(AddreseblesIDs.Asteroid.ToString());
        
        public UniTask<Canvas> CreateLoadingScreen() => LoadAssetComponent<Canvas>(AddreseblesIDs.LoadingScreen.ToString());
        public void ReleaseLoadingScreen() => ReleaseAsset(AddreseblesIDs.LoadingScreen.ToString());
        
        public UniTask<RestartPanelUI> CreateRestartPanelUI() => LoadAssetComponent<RestartPanelUI>(AddreseblesIDs.RestartCanvas.ToString());
        public void ReleaseRestartPanelUI() => ReleaseAsset(AddreseblesIDs.RestartCanvas.ToString());
        
        public UniTask<Bullet> CreateBullet() => LoadAssetComponent<Bullet>(AddreseblesIDs.Bullet.ToString());
        public void ReleaseBullet() => ReleaseAsset(AddreseblesIDs.Bullet.ToString());

        public void ReleaseAllAssets() => ReleaseAll();
    }
}