using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

namespace _Project.Scripts.Creatures.Player.SpaceShipWeapon
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _speed = 15f;
        [SerializeField] private float _bulletLifeTime = 5f;
    
        private CancellationTokenSource _cancellationTokenSource;

        private void OnEnable()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            StartLifeTimer(_cancellationTokenSource.Token);
        }

        private void OnDisable()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = null;
        }

        private async void StartLifeTimer(CancellationToken token)
        {
            try
            {
                await Task.Delay((int)(_bulletLifeTime * 1000), token);
                if (!token.IsCancellationRequested)
                {
                    Deactivate();
                }
            }
            catch (TaskCanceledException)
            {
            }
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
