using _Project.Scripts.Creatures.Player;
using _Project.Scripts.Creatures.Player.SpaceShipWeapon;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.UI
{
    public class HUDView : MonoBehaviour, IHUDView
    {
        [SerializeField] private TMP_Text _coordinatesText;
        [SerializeField] private TMP_Text _rotationText;
        [SerializeField] private TMP_Text _velocityText;
        [SerializeField] private TMP_Text _chargesText;
        [SerializeField] private TMP_Text _nextLaserText;
    
        private HUDPresenter _presenter;

        public void Initialize(SpaceShipMovement spaceShipMovement, SpaceShipLaser spaceShipLaser)
        {
            _presenter = new HUDPresenter(this, spaceShipMovement, spaceShipLaser);
        }

        private void Update()
        {
            _presenter?.Update();
        }

        // ----- View Implementation -----
        public void SetCoordinates(Vector2 position)
        {
            _coordinatesText.text = $"Position:\nX:{position.x:F1}\nY:{position.y:F1}";
        }

        public void SetRotation(float angle)
        {
            _rotationText.text = $"Rotation: {angle:F0}°";
        }

        public void SetSpeed(float speed)
        {
            _velocityText.text = $"Speed: {speed:F1}";
        }

        public void SetLaserCharges(int charges)
        {
            _chargesText.text = $"Laser charges: {charges}";
        }

        public void SetNextLaserTime(float progress)
        {
            _nextLaserText.text = $"Next laser time: {progress:F0}";
        }
    }
}