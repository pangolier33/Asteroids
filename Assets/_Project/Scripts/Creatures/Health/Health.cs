using UnityEngine;

namespace _Project.Scripts.Creatures.Health
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private int _maxHealth = 1;
        private int _currentHealth;

        private void Start()
        {
            _currentHealth = _maxHealth;
        }
        
        public void TakeDamage(int damage)
        {
            if (_currentHealth <= 0) return; 

            _currentHealth -= damage;

            if (_currentHealth <= 0)
            {
                Die();
            }
        }

        protected virtual void Die()
        {
            
        }
    }
}
