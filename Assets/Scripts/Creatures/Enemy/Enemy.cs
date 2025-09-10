using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void OnDisable()
    {
        EnemyEvents.EnemyDestroyed();
    }

    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
