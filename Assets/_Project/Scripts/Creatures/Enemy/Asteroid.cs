using Zenject;

namespace _Project.Scripts.Creatures.Enemy
{
    public class Asteroid : Enemy
    {
        public bool isParentObject = true;
        public class Pool : MonoMemoryPool<Asteroid>
        {
        }
    }
}