using System.Collections;
using _Project.Scripts.Creatures.Player;
using UnityEngine;

namespace _Project.Scripts.Creatures.Enemy
{
    public class UfoMovement : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 2f;
        [SerializeField] private float _updateInterval = 0.5f;
        
        private Transform _spaceShip;
        private Rigidbody2D _rigidbody2D;
        private Vector3 _moveDirection;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>(); 
            _spaceShip = FindFirstObjectByType<SpaceShipMovement>().gameObject.transform;
            
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
