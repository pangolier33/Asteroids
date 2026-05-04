using System.Collections;
using _Project.Scripts.Creatures.Player;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Creatures.Enemy
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class UfoMovement : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 2f;
        [SerializeField] private float _updateInterval = 0.5f;
        
        private Transform _spaceShip;
        private Rigidbody2D _rigidbody2D;
        private Vector3 _moveDirection;

        [Inject]
        public void Construct(SpaceShipMovement spaceShip)
        {
            _spaceShip = spaceShip.transform;
        }

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            StartCoroutine(UpdateMovementCoroutine());
        }

        private void FixedUpdate()
        {
            if (_spaceShip != null)
                _rigidbody2D.linearVelocity = _moveDirection * _moveSpeed;
        }

        private IEnumerator UpdateMovementCoroutine()
        {
            while (true)
            {
                if (_spaceShip != null)
                {
                    _moveDirection = (_spaceShip.position - transform.position).normalized;
                }

                yield return new WaitForSeconds(_updateInterval);
            }
        }
    }
}
