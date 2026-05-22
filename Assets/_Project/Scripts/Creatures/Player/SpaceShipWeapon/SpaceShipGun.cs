using System;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace _Project.Scripts.Creatures.Player.SpaceShipWeapon
{
    public class SpaceShipGun : MonoBehaviour
    {
        public event Action ClickShoot;

        [SerializeField] private Transform _firePoint;
        [SerializeField] private float _delayBullet = 0.25f;

        private Bullet.Pool _bulletPool;
        private float _nextFireTime;

        [Inject]
        public void Construct(Bullet.Pool bulletPool)
        {
            _bulletPool = bulletPool;
        }

        public void HandleShooting()
        {
            if (!IsCooldownReady(_nextFireTime))
                return;

            ClickShoot?.Invoke();

            ShootBullet();

            SetCooldown(ref _nextFireTime, _delayBullet);
        }

        private bool IsCooldownReady(float nextTime)
        {
            return Time.time >= nextTime;
        }

        private void ShootBullet()
        {
            Bullet bullet = _bulletPool.Spawn();

            bullet.transform.position = _firePoint.position;
            bullet.transform.rotation = _firePoint.rotation;

            bullet.Launch(transform.up);
        }

        private void SetCooldown(ref float nextTime, float delay)
        {
            nextTime = Time.time + delay;
        }
    }
}
