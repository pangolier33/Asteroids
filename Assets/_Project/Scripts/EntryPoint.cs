using _Project.Scripts.Creatures.Enemy;
using _Project.Scripts.Creatures.Player;
using _Project.Scripts.Creatures.Player.SpaceShipWeapon;
using _Project.Scripts.UI;
using NavMeshPlus.Components;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;

namespace _Project.Scripts
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private Canvas _hud;
        [SerializeField] private SpaceShipMovement _spaceShip;
        [SerializeField] private EnemySpawner _enemySpawner;

        private void Start()
        {
            BindObjects();
            StartCoroutine(_enemySpawner.SpawnEnemies());
        }

        private void BindObjects()
        {
            _mainCamera = Instantiate(_mainCamera);
            _spaceShip = Instantiate(_spaceShip);
            _hud = Instantiate(_hud);
            BindHud();
            _enemySpawner = Instantiate(_enemySpawner);
        }

        private void BindHud()
        {
            HUD hudComponent = _hud.GetComponent<HUD>();
            SpaceShipWeapon spaceShipWeapon = _spaceShip.GetComponent<SpaceShipWeapon>();
            hudComponent.Initialize(_spaceShip, spaceShipWeapon);
            _hud.worldCamera = _mainCamera;
        }
    }
}
