using System.Collections;
using UnityEngine;

namespace _Project.Scripts.Creatures.Player.SpaceShipWeapon
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _speed = 15f;
        [SerializeField] private float _bulletLifeTime = 5f;

        private void OnEnable()
        {
            StartCoroutine(StartLifeTimer());
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
            GetComponent<Rigidbody2D>().linearVelocity = direction * _speed;
        }

        private void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}
