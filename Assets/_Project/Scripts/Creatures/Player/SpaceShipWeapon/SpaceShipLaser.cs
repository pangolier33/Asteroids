using UnityEngine;

namespace _Project.Scripts.Creatures.Player.SpaceShipWeapon
{
    public class SpaceShipLaser : MonoBehaviour
    {
        private const int MAX_LASER_CHARGES = 3;

        [SerializeField] private GameObject _laser;
        [SerializeField] private float _laserDuration = 2f;
        [SerializeField] private float _chargeRechargeTime = 5f;
        
        [field: SerializeField] public int LaserCharges { get; private set; }

        private float _nextChargeTime;
        private float _laserEndTime;
        private bool _isLaserActive;

        private void Awake()
        {
            _laser.SetActive(false);
        }

        private void Update()
        {
            UpdateLaserDuration();
            UpdateChargesRecharge();
        }

        public void HandleLaserActivation()
        {
            if (LaserCharges > 0 && !_isLaserActive)
            {
                UseLaserCharge();
                EnableLaser();
            }
        }
        
        public float GetNextChargeProgress()
        {
            if (LaserCharges >= MAX_LASER_CHARGES)
                return 0f;

            return _nextChargeTime - Time.time;
        }

        private void UpdateLaserDuration()
        {
            if (_isLaserActive && Time.time >= _laserEndTime)
            {
                DisableLaser();
            }
        }

        private void UpdateChargesRecharge()
        {
            if (LaserCharges < MAX_LASER_CHARGES && Time.time >= _nextChargeTime)
            {
                AddLaserCharge();
                _nextChargeTime = Time.time + _chargeRechargeTime;
            }
        }

        private void EnableLaser()
        {
            _laser.SetActive(true);
            _isLaserActive = true;
            _laserEndTime = Time.time + _laserDuration;
        }

        private void DisableLaser()
        {
            _laser.SetActive(false);
            _isLaserActive = false;
        }

        private void UseLaserCharge()
        {
            LaserCharges--;
        }

        private void AddLaserCharge()
        {
            if (LaserCharges < MAX_LASER_CHARGES)
            {
                LaserCharges++;
            }
        }
    }
}
