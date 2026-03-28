using _Project.Scripts.Creatures.Player.SpaceShipWeapon;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Project.Scripts.Creatures.Player
{
    [RequireComponent(typeof(SpaceShipMovement))]
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private SpaceShipMovement _spaceShipMovement;
        [SerializeField] private SpaceShipGun _spaceShipGun;
        [SerializeField] private SpaceShipLaser _spaceShipLaser;

        private InputSystem_Actions _inputSystem;
        private bool _isInitialized;

        private void OnEnable()
        {
            _inputSystem.Enable();
            
            _inputSystem.Player.Shoot.performed += OnShootPerformed;
            _inputSystem.Player.Laser.performed += OnLaserPerformed;
        }
        
        private void Awake()
        {
            _inputSystem = new InputSystem_Actions();
            
            if (_spaceShipMovement == null)
                _spaceShipMovement = GetComponent<SpaceShipMovement>();
            
            _isInitialized = true;
        }

        private void Update()
        {
            if (!_isInitialized || _inputSystem == null) return;
            
            ReadAccelerationMovement();
            ReadRotationMovement();
        }
        
        private void OnDisable()
        {
            if (_inputSystem == null) return;
            
            _inputSystem.Player.Shoot.performed -= OnShootPerformed;
            _inputSystem.Player.Laser.performed -= OnLaserPerformed;
            
            _inputSystem.Disable();
        }

        private void OnDestroy()
        {
            if (_inputSystem != null)
            {
                _inputSystem.Dispose();
                _inputSystem = null;
            }
        }

        private void ReadRotationMovement()
        {
            Vector2 inputDirection = _inputSystem.Player.Move.ReadValue<Vector2>();
            float direction = inputDirection.x;
            
            if (_spaceShipMovement != null)
            {
                _spaceShipMovement.HandleShipRotation(-direction);
            }
        }

        private void ReadAccelerationMovement()
        {
            Vector2 inputDirection = _inputSystem.Player.Move.ReadValue<Vector2>();
            float direction = inputDirection.y;
            
            if (_spaceShipMovement == null) return;
            
            if (direction >= 0.1f)
            {
                _spaceShipMovement.HandleShipAcceleration();
            }
            else
            {
                _spaceShipMovement.HandleShipStopAcceleration();
            }
        }

        private void OnShootPerformed(InputAction.CallbackContext obj)
        {
            if (_spaceShipGun != null)
            {
                _spaceShipGun.HandleShooting();
            }
        }

        private void OnLaserPerformed(InputAction.CallbackContext obj)
        {
            if (_spaceShipLaser != null)
            {
                _spaceShipLaser.HandleLaserActivation();
            }
        }
    }
}
