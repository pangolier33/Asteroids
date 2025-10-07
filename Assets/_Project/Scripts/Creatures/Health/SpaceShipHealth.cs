using _Project.Scripts.Creatures.Player;

namespace _Project.Scripts.Creatures.Health
{
    public class SpaceShipHealth : Health
    {
        private SpaceShipDeath _spaceShipDeath;

        private void Awake()
        {
            _spaceShipDeath = GetComponent<SpaceShipDeath>();
        }

        protected override void Die()
        {
            _spaceShipDeath.Death();
        }
    }
}
