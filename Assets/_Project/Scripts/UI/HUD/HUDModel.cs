using _Project.Scripts.Creatures.Player;
using _Project.Scripts.Creatures.Player.SpaceShipWeapon;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.UI.HUD
{
    public class HUDModel : IInitializable, ITickable
    {
        private SpaceShipMovement _spaceShip;
        private Rigidbody2D _rb;
        private SpaceShipLaser _spaceShipLaser;

        private int _number;
        
        public Vector2 ShipPosition { get; private set; }
        public float ShipRotation { get; private set; }
        public float ShipSpeed { get; private set; }
        public int LaserCharges { get; private set; }
        public float NextLaserTime { get; private set; }
        
        public HUDModel(SpaceShipMovement spaceShip)
        {
            _spaceShip = spaceShip;
        }
        
        public void Initialize()
        {
            _rb = _spaceShip.GetComponent<Rigidbody2D>();
            _spaceShipLaser = _spaceShip.GetComponent<SpaceShipLaser>();
        }
        
        public void Tick()
        {
            SetModel();
        }

        private void SetModel()
        {
            ShipPosition = _rb.position;
            ShipRotation = NormalizeAngle(_rb.rotation);
            ShipSpeed = _rb.linearVelocity.magnitude;
            LaserCharges = _spaceShipLaser.LaserCharges;
            NextLaserTime = _spaceShipLaser.GetNextChargeProgress();
        }
        
        private float NormalizeAngle(float angle)
        {
            angle %= 360f;

            if (angle < 0)
                angle += 360f;

            return angle;
        }
    }
}