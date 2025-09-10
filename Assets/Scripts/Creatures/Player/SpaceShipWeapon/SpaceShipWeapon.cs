using UnityEngine;

public class SpaceShipWeapon : MonoBehaviour
{
    private const int PROJECTILE_PRELOAD_COUNT = 20;
    private const int MAX_LASER_CHARGES = 3;

    [SerializeField] private Transform _firePoint;
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private GameObject _laser;
    [SerializeField] private float _delayBullet = 0.25f;
    [SerializeField] private float _laserDuration = 2f;
    [SerializeField] private float _chargeRechargeTime = 5f;

    private PoolBase<Bullet> _bulletPool;
    private float _nextFireTime;
    private float _nextChargeTime;
    private float _laserEndTime;
    private bool _isLaserActive;
    private int _laserCharges = MAX_LASER_CHARGES;
    public int MaxLaserCharges => MAX_LASER_CHARGES;

    private void Awake()
    {
        _bulletPool = new PoolBase<Bullet>(
            Preload,
            GetAction,
            ReturnAction,
            PROJECTILE_PRELOAD_COUNT);

        _laser.SetActive(false);
    }

    private void Update()
    {
        UpdateLaserDuration();

        UpdateChargesRecharge();
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
        if (_laserCharges < MAX_LASER_CHARGES && Time.time >= _nextChargeTime)
        {
            AddLaserCharge();
            _nextChargeTime = Time.time + _chargeRechargeTime;
        }
    }

    private Bullet Preload()
    {
        Bullet bullet = Instantiate(_bulletPrefab);
        bullet.gameObject.SetActive(false);
        return bullet;
    }

    private void GetAction(Bullet bullet) => bullet.gameObject.SetActive(true);
    private void ReturnAction(Bullet bullet) => bullet.gameObject.SetActive(false);
    private void ReturnAllBullets() => _bulletPool.ReturnAll();

    private bool IsCooldownReady(float nextTime) => Time.time >= nextTime;

    public void HandleShooting()
    {
        if (!IsCooldownReady(_nextFireTime))
            return;

        ShootBullet();
        SetCooldown(ref _nextFireTime, _delayBullet);
    }

    public void HandleLaserActivation()
    {
        if (_laserCharges > 0 && !_isLaserActive)
        {
            UseLaserCharge();
            EnableLaser();
        }
    }

    private void ShootBullet()
    {
        Bullet bullet = _bulletPool.Get();

        bullet.transform.position = _firePoint.position;
        bullet.transform.rotation = _firePoint.rotation;
        bullet.Launch(transform.up);
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
        _laserCharges--;
    }

    private void AddLaserCharge()
    {
        if (_laserCharges < MAX_LASER_CHARGES)
        {
            _laserCharges++;
        }
    }

    private void SetCooldown(ref float nextTime, float delay)
    {
        nextTime = Time.time + delay;
    }

    public float GetNextChargeProgress()
    {
        if (_laserCharges >= MAX_LASER_CHARGES)
            return 0f;

        return _nextChargeTime - Time.time;
    }

    public int GetLaserCharges() => _laserCharges;
    public bool IsLaserReady() => _laserCharges > 0 && !_isLaserActive;
    public bool IsShootingReady() => IsCooldownReady(_nextFireTime);
}
