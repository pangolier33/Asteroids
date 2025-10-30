using _Project.Scripts.Creatures.Player.SpaceShipWeapon;
using UnityEngine;

namespace _Project.Scripts.Creatures.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpaceShipLaser))]
    public class SpaceShipMovement : MonoBehaviour
    {
        public Rigidbody2D ShipRigidbody { get; private set; }
        
        [SerializeField] private float _shipAcceleration = 10f;
        [SerializeField] private float _shipMaxVelocity = 10f;
        [SerializeField] private float _shipRotationSpeed = 180f;
        
        private bool _isAccelerating = false;

        private void Awake()
        {
            ShipRigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            if (_isAccelerating)
            {
                ShipRigidbody.AddForce(_shipAcceleration * transform.up);
                ShipRigidbody.linearVelocity = Vector2.ClampMagnitude(ShipRigidbody.linearVelocity, _shipMaxVelocity);
            }
        }

        public void HandleShipAcceleration()
        {
            _isAccelerating = true;
        }

        public void HandleShipStopAcceleration()
        {
            _isAccelerating = false;
        }

        public void HandleShipRotation(float direction)
        {
            ShipRigidbody.AddTorque(direction * _shipRotationSpeed * Time.deltaTime);
        }
    }
}
