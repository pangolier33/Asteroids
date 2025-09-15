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
        [SerializeField] private SpaceShipMovement _spaceShipMonement;
        [SerializeField] private SpaceShipWeapon _spaceShipWeapon;

        private Rigidbody2D shipRigidbody;
        private int _score;
        
        private void Start()
        {
            shipRigidbody = _spaceShipMonement.GetRigidbody;
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
            Vector2 position = shipRigidbody.position;
            _coordinatesText.text = $"Position:\nX:{position.x:F1}\nY:{position.y:F1}";
        }

        private void UpdateRotationUI()
        {
            float rotation = NormalizeAngle(shipRigidbody.rotation);
            _rotationText.text = $"Rotation: {rotation:F0}°";
        }

        private void UpdateSpeedUI()
        {
            Vector2 velocity = shipRigidbody.linearVelocity;
            _velocityText.text = $"Speed: {velocity.magnitude:F1}";
        }

        private void UpdateLaserChargesUI()
        {
            int laserCharges = _spaceShipWeapon.GetLaserCharges();
            _chargesText.text = $"Laser charges: {laserCharges}";
        }

        private void UpdateNextLaserTimeUI()
        {
            float nextLaserTime = _spaceShipWeapon.GetNextChargeProgress();
            _nextLaserText.text = $"Next laser time: {nextLaserTime:F0}";
        }
    }
}
