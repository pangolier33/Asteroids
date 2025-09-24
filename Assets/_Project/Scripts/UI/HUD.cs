using _Project.Scripts.Creatures.Player;
using _Project.Scripts.Creatures.Player.SpaceShipWeapon;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.UI
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] private TMP_Text _coordinatesText;
        [SerializeField] private TMP_Text _rotationText;
        [SerializeField] private TMP_Text _velocityText;
        [SerializeField] private TMP_Text _chargesText;
        [SerializeField] private TMP_Text _nextLaserText;
        
        private SpaceShipMovement _spaceShipMovement;
        private SpaceShipWeapon _spaceShipWeapon;
        private Rigidbody2D _shipRigidbody;
        private int _score;

        public void Initialize(SpaceShipMovement spaceShipMovement, SpaceShipWeapon spaceShipWeapon)
        {
            _spaceShipMovement = spaceShipMovement;
            _spaceShipWeapon = spaceShipWeapon;
        }
        
        private void Start()
        {
            _shipRigidbody = _spaceShipMovement.ShipRigidbody;
        }

        private void Update()
        {
            UpdateShipInfoUI();
        }

        private void UpdateShipInfoUI()
        {
            UpdateCoordinatesUI();

            UpdateRotationUI();

            UpdateSpeedUI();

            UpdateLaserChargesUI();

            UpdateNextLaserTimeUI();
        }

        private float NormalizeAngle(float angle)
        {
            angle %= 360;
            if (angle < 0) angle += 360;
            return angle;
        }

        private void UpdateCoordinatesUI()
        {
            Vector2 position = _shipRigidbody.position;
            _coordinatesText.text = $"Position:\nX:{position.x:F1}\nY:{position.y:F1}";
        }

        private void UpdateRotationUI()
        {
            float rotation = NormalizeAngle(_shipRigidbody.rotation);
            _rotationText.text = $"Rotation: {rotation:F0}°";
        }

        private void UpdateSpeedUI()
        {
            Vector2 velocity = _shipRigidbody.linearVelocity;
            _velocityText.text = $"Speed: {velocity.magnitude:F1}";
        }

        private void UpdateLaserChargesUI()
        {
            int laserCharges = _spaceShipWeapon._laserCharges;
            _chargesText.text = $"Laser charges: {laserCharges}";
        }

        private void UpdateNextLaserTimeUI()
        {
            float nextLaserTime = _spaceShipWeapon.GetNextChargeProgress();
            _nextLaserText.text = $"Next laser time: {nextLaserTime:F0}";
        }
    }
}
