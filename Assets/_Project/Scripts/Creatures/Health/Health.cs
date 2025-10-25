using UnityEngine;

namespace _Project.Scripts.Creatures.Health
{
    [RequireComponent(typeof(ICreatureDeath))]
    public class Health : MonoBehaviour
    {
        [SerializeField] private int _maxHealth = 1;
        [SerializeField, Min(0)] private int _currentHealth;

        private ICreatureDeath _creatureDeath;

        private void Start()
        {
            _currentHealth = _maxHealth;
            _creatureDeath = GetComponent<ICreatureDeath>();
        }
        
        public void TakeDamage(int damage)
        {
            if (_currentHealth <= 0) return; 

            _currentHealth -= damage;

            if (_currentHealth <= 0)
            {
                _creatureDeath.CreatureDeath();
            }
        }
    }
}
