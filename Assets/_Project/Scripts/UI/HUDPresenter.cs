using _Project.Scripts.Creatures.Player;
using _Project.Scripts.Creatures.Player.SpaceShipWeapon;
using UnityEngine;

namespace _Project.Scripts.UI
{
    public class HUDPresenter
    {
        private readonly IHUDView _view;
        private readonly SpaceShipMovement _movement;
        private readonly SpaceShipLaser _laser;
        private readonly Rigidbody2D _shipRigidbody;

        public HUDPresenter(IHUDView view, SpaceShipMovement movement, SpaceShipLaser laser)
        {
            _view = view;
            _movement = movement;
            _laser = laser;
            _shipRigidbody = movement.ShipRigidbody;
        }

        public void Update()
        {
            if (_shipRigidbody == null || _laser == null) return;

            UpdateCoordinates();
            UpdateRotation();
            UpdateSpeed();
            UpdateLaserCharges();
            UpdateNextLaserTime();
        }

        private void UpdateCoordinates()
        {
            _view.SetCoordinates(_shipRigidbody.position);
        }

        private void UpdateRotation()
        {
            float rotation = NormalizeAngle(_shipRigidbody.rotation);
            _view.SetRotation(rotation);
        }

        private void UpdateSpeed()
        {
            float speed = _shipRigidbody.linearVelocity.magnitude;
            _view.SetSpeed(speed);
        }

        private void UpdateLaserCharges()
        {
            _view.SetLaserCharges(_laser.LaserCharges);
        }

        private void UpdateNextLaserTime()
        {
            float progress = _laser.GetNextChargeProgress();
            _view.SetNextLaserTime(progress);
        }

        private float NormalizeAngle(float angle)
        {
            angle %= 360;
            if (angle < 0) angle += 360;
            return angle;
        }
    }
}