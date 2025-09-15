using System;
using _Project.Scripts.Creatures.Player.SpaceShipWeapon;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private SpaceShipMovement _spaceShipMovement;
    [SerializeField] private SpaceShipWeapon _spaceShipWeapon;

    private InputSystem_Actions _inputSystem;


    private void OnEnable()
    {
        _inputSystem.Player.Shoot.performed += OnShootPermormed;
        _inputSystem.Player.Laser.performed += OnLaserEnabled;
    }

    private void OnDisable()
    {
        _inputSystem.Player.Shoot.performed -= OnShootPermormed;
        _inputSystem.Player.Laser.performed -= OnLaserEnabled;
    }

    private void Awake()
    {
        _spaceShipMovement = GetComponent<SpaceShipMovement>();
        _inputSystem = new InputSystem_Actions();
        _inputSystem.Enable();
        
    }

    private void Update()
    {
        ReadAccelerationMovement();
        
        ReadRotationMovement();
    }

    private void ReadRotationMovement()
    {
        Vector2 inputDirection = _inputSystem.Player.Move.ReadValue<Vector2>();
        float direction = inputDirection.x;
        
        _spaceShipMovement.HandleShipRotation(direction);
    }

    private void ReadAccelerationMovement()
    {
        Vector2 inputDirection = _inputSystem.Player.Move.ReadValue<Vector2>();
        float direction = inputDirection.y;
        if (direction >= 0.1f)
        {
            _spaceShipMovement.HandleShipAcceleration();
        }
        else
        {
            _spaceShipMovement.HandleShipStopAcceleration();
        }
            
    }

    private void OnShootPermormed(InputAction.CallbackContext obj)
    {
        _spaceShipWeapon.HandleShooting();
    }

    private void OnLaserEnabled(InputAction.CallbackContext obj)
    {
        _spaceShipWeapon.HandleLaserActivation();
    }
}
