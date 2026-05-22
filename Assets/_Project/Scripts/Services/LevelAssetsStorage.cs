using _Project.Scripts.Creatures.Enemy;
using _Project.Scripts.Creatures.Player;
using _Project.Scripts.Creatures.Player.SpaceShipWeapon;
using _Project.Scripts.UI;
using _Project.Scripts.UI.HUD;
using _Project.Scripts.UI.RestartPanel;

namespace _Project.Scripts.Services
{
    public class LevelAssetsStorage
    {
        public SpaceShipMovement SpaceShipPrefab;
        public HUDView HudPrefab;
        public UFO UfoPrefab;
        public Asteroid AsteroidPrefab;
        public Bullet BulletPrefab;
        public RestartPanelUIView RestartPanelPrefab;
    }
}