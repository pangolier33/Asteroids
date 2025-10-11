using _Project.Scripts.Creatures.Enemy;
using _Project.Scripts.Creatures.Player;
using _Project.Scripts.Creatures.Player.SpaceShipWeapon;
using _Project.Scripts.Services;
using _Project.Scripts.UI;
using UnityEngine;

namespace _Project.Scripts
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private Canvas _hud;
        [SerializeField] private SpaceShipMovement _spaceShip;
        [SerializeField] private EnemySpawner _enemySpawner;

        private void Start()
        {
            BindObjects();
        }

        private void BindObjects()
        {
            _spaceShip = Instantiate(_spaceShip);
            _hud = Instantiate(_hud);
            BindHud();
            _enemySpawner = Instantiate(_enemySpawner);
            
            StartCoroutine(_enemySpawner.SpawnEnemies());
        }

        private void BindHud()
        {
            HUD hudComponent = _hud.GetComponent<HUD>();
            SpaceShipWeapon spaceShipWeapon = _spaceShip.GetComponent<SpaceShipWeapon>();
            hudComponent.Initialize(_spaceShip, spaceShipWeapon);
            _hud.worldCamera = Camera.main;
        }
    }
}
