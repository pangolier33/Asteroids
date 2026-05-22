using _Project.Scripts.Creatures.Enemy;
using _Project.Scripts.Creatures.Player;
using _Project.Scripts.Creatures.Player.SpaceShipWeapon;
using _Project.Scripts.Enums;
using _Project.Scripts.UI;
using _Project.Scripts.UI.HUD;
using _Project.Scripts.UI.RestartPanel;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Services.Addressebles
{
    public class AddressableReferencesLoader : AddressableLoader, IAddressableReferencesLoader
    {
        public UniTask<HUDView> CreateHUD() => LoadAssetComponent<HUDView>(AddreseblesIDs.HUD.ToString());
        public void ReleaseHUD() => ReleaseAsset(AddreseblesIDs.HUD.ToString());
        
        public UniTask<SpaceShipMovement> CreateSpaceShip() => LoadAssetComponent<SpaceShipMovement>(AddreseblesIDs.SpaceShip.ToString());
        public void ReleaseSpaceShip() => ReleaseAsset(AddreseblesIDs.SpaceShip.ToString());
        
        public UniTask<UFO> CreateUFO() => LoadAssetComponent<UFO>(AddreseblesIDs.UFO.ToString());
        public void ReleaseUFO() => ReleaseAsset(AddreseblesIDs.UFO.ToString());
        
        public UniTask<Asteroid> CreateAsteroid() => LoadAssetComponent<Asteroid>(AddreseblesIDs.Asteroid.ToString());
        public void ReleaseAsteroid() => ReleaseAsset(AddreseblesIDs.Asteroid.ToString());
        
        public UniTask<Canvas> CreateLoadingScreen() => LoadAssetComponent<Canvas>(AddreseblesIDs.LoadingScreen.ToString());
        public void ReleaseLoadingScreen() => ReleaseAsset(AddreseblesIDs.LoadingScreen.ToString());
        
        public UniTask<RestartPanelUIView> CreateRestartPanelUI() => LoadAssetComponent<RestartPanelUIView>(AddreseblesIDs.RestartCanvas.ToString());
        public void ReleaseRestartPanelUI() => ReleaseAsset(AddreseblesIDs.RestartCanvas.ToString());
        
        public UniTask<Bullet> CreateBullet() => LoadAssetComponent<Bullet>(AddreseblesIDs.Bullet.ToString());
        public void ReleaseBullet() => ReleaseAsset(AddreseblesIDs.Bullet.ToString());

        public void ReleaseAllAssets() => ReleaseAll();
    }
}