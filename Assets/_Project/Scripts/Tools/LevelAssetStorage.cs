using _Project.Scripts.Creatures.Enemy;
using _Project.Scripts.Creatures.Player;
using _Project.Scripts.Creatures.Player.SpaceShipWeapon;
using _Project.Scripts.UI;
using _Project.Scripts.UI.HUD;
using _Project.Scripts.UI.RestartPanel;

namespace _Project.Scripts.Tools
{
    public class LevelAssetStorage
    {
        public HUDView HudPrefab;
        public SpaceShipMovement SpaceShipPrefab;
        public UFO UfoPrefab;
        public Asteroid AsteroidPrefab;
        public RestartPanelUIView RestartPanelPrefab;
        public Bullet BulletPrefab;
    }
}
