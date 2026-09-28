using UnityEngine;

public class PooledEnemy : MonoBehaviour
{
    private EnemyPool enemyPool;

    private EnemyType enemyType;

    public void SetPool(EnemyPool pool, EnemyType type)
    {
        enemyPool = pool;
        enemyType = type;
    }

    public void ReturnToPool()
    {
        if (enemyPool != null)
        {
            enemyPool.Release(gameObject, enemyType);
        }
    }
}