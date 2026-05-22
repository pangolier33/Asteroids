using System.Collections;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Creatures.Player.SpaceShipWeapon
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _speed = 15f;
        [SerializeField] private float _bulletLifeTime = 5f;

        private Rigidbody2D _rb;
        private Coroutine _lifeCoroutine;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            _rb.linearVelocity = Vector2.zero;

            if (_lifeCoroutine != null)
                StopCoroutine(_lifeCoroutine);

            _lifeCoroutine = StartCoroutine(StartLifeTimer());
        }

        private IEnumerator StartLifeTimer()
        {
            yield return new WaitForSeconds(_bulletLifeTime);

            Deactivate();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            Deactivate();
        }

        public void Launch(Vector3 direction)
        {
            direction.Normalize();

            transform.up = direction;

            _rb.linearVelocity = direction * _speed;
        }

        private void Deactivate()
        {
            _rb.linearVelocity = Vector2.zero;

            gameObject.SetActive(false);
        }

        public class Pool : MonoMemoryPool<Bullet>
        {
            protected override void OnSpawned(Bullet item)
            {
                item.gameObject.SetActive(true);
            }

            protected override void OnDespawned(Bullet item)
            {
                item.gameObject.SetActive(false);
            }
        }
    }
}
