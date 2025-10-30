using _Project.Scripts.Creatures.Enemy;
using _Project.Scripts.Creatures.Factories;
using UnityEngine;

namespace _Project.Scripts.Creatures.Player.SpaceShipWeapon
{
    public class SpaceShipGun : MonoBehaviour
    {
        private const int PROJECTILE_PRELOAD_COUNT = 20;

        [SerializeField] private Transform _firePoint;
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private float _delayBullet = 0.25f;

        private BaseFactory<Bullet> _bulletFactory;
        private float _nextFireTime;

        private void Awake()
        {
            _bulletFactory = new BaseFactory<Bullet>(_bulletPrefab, PROJECTILE_PRELOAD_COUNT);
            _bulletFactory.PoolInitialize();
        }

        public void HandleShooting()
        {
            if (!IsCooldownReady(_nextFireTime))
                return;

            ShootBullet();
            SetCooldown(ref _nextFireTime, _delayBullet);
        }

        private bool IsCooldownReady(float nextTime) => Time.time >= nextTime;

        private void ShootBullet()
        {
            Bullet bullet = _bulletFactory.GetPrefab();

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
