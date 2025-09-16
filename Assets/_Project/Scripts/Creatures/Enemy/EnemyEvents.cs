using System;

public static class EnemyEvents
{
    public static event Action OnEnemyDestroyed;

    public static void EnemyDestroyed()
    {
        OnEnemyDestroyed?.Invoke();
    }
}
