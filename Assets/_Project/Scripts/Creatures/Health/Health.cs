using UnityEngine;
using UnityEngine.Events;

namespace _Project.Scripts.Creatures.Health
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private int _maxHealth = 1;
        [SerializeField] private UnityEvent _onDie;
        [SerializeField] private UnityEvent<int> _onHealthChanged; 

        [SerializeField] private int _currentHealth;

        private void Start()
        {
            _currentHealth = _maxHealth;
            _onHealthChanged?.Invoke(_currentHealth);
        }
        
        public void TakeDamage(int damage)
        {
            if (_currentHealth <= 0) return; 

            _currentHealth -= damage;
            _onHealthChanged?.Invoke(_currentHealth); 

            if (_currentHealth <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            _onDie?.Invoke(); 
        }
    }
}
