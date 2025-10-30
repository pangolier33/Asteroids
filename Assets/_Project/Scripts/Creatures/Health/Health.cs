using UnityEngine;

namespace _Project.Scripts.Creatures.Health
{
    [RequireComponent(typeof(ICreatureDied))]
    public class Health : MonoBehaviour
    {
        [SerializeField] private int _maxHealth = 1;
        [SerializeField, Min(0)] private int _currentHealth;

        private ICreatureDied _creatureDied;

        private void Start()
        {
            _currentHealth = _maxHealth;
            _creatureDied = GetComponent<ICreatureDied>();
        }
        
        public void TakeDamage(int damage)
        {
            if (_currentHealth <= 0) return; 

            _currentHealth -= damage;

            if (_currentHealth <= 0)
            {
                _creatureDied.CreatureDied();
            }
        }
    }
}
