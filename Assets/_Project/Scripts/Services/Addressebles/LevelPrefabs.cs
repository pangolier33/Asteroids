using _Project.Scripts.Creatures.Enemy;
using _Project.Scripts.Creatures.Player;
using _Project.Scripts.Creatures.Player.SpaceShipWeapon;
using _Project.Scripts.UI;
using UnityEngine;

namespace _Project.Scripts.Services.Addressebles
{
    public class LevelPrefabs
    {
        public SpaceShipMovement spaceShipPrefab;
        public HUD hudPrefab;
        public Enemy asteroidPrefab;
        public Enemy ufoPrefab;
        public Canvas loadingScreenPrefab;
        public RestartPanelUI restartPanelUIPrefab;
        public Bullet bulletPrefab;
    }
}