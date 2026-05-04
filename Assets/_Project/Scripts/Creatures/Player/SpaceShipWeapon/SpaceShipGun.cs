using System;
using _Project.Scripts.Factories;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace _Project.Scripts.Creatures.Player.SpaceShipWeapon
{
    public class SpaceShipGun : MonoBehaviour
    {
        private const int PROJECTILE_PRELOAD_COUNT = 20;
        public event Action clickShoot;
        
        public Bullet bulletPrefab;

        [SerializeField] private Transform _firePoint;
        [SerializeField] private float _delayBullet = 0.25f;
        
        private BaseFactory<Bullet> _bulletFactory;
        private float _nextFireTime;
        private IInstantiator _instantiator;

        public void Construct(Bullet bullet, IInstantiator instantiator)
        {
            bulletPrefab = bullet;
            _instantiator = instantiator; 
            
            SetBulletSpawner();
        }
        
        private void SetBulletSpawner()
        {
            _bulletFactory = new BaseFactory<Bullet>(bulletPrefab, PROJECTILE_PRELOAD_COUNT, _instantiator);
            _bulletFactory.PoolInitialize();
        }

        public void HandleShooting()
        {
            if (_bulletFactory == null)
            {
                Debug.LogError("BulletFactory is NULL!");
                return;
            }

            if (!IsCooldownReady(_nextFireTime))
                return;

            clickShoot?.Invoke();
            ShootBullet();
            SetCooldown(ref _nextFireTime, _delayBullet);
        }

        private bool IsCooldownReady(float nextTime) => Time.time >= nextTime;

        private void ShootBullet()
        {
            Bullet bullet = _bulletFactory.GetPooledObject();

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
