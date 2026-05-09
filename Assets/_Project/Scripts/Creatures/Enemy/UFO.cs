using _Project.Scripts.Creatures.Enemy;
using Zenject;

public class UFO : Enemy
{
    public class Pool : MonoMemoryPool<UFO>
    {
    }
}
