using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private SpaceShipMovement _spaceShipMovement;
    [SerializeField] private SpaceShipWeapon _spaceShipWeapon;


    private void Awake()
    {
        _spaceShipMovement = GetComponent<SpaceShipMovement>();
    }

    private void Update()
    {
        _spaceShipMovement.HandleShipAcceleration();

        if (Input.GetKeyDown(KeyCode.C))
        {
            _spaceShipWeapon.HandleLaserActivation();
        }

        if (Input.GetKey(KeyCode.Space))
        {
            _spaceShipWeapon.HandleShooting();
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _spaceShipMovement.HandleShipRotationLeft();
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            _spaceShipMovement.HandleShipRotationRight();
        }
    }

}
