using UnityEngine;

public class PooledEnemy : MonoBehaviour
{
    private EnemyPool enemyPool;

    public void SetPool(EnemyPool pool)
    {
        enemyPool = pool;
    }

    public void ReturnToPool()
    {
        Debug.Log("ReturnToPool 被调用了");
        if (enemyPool != null)
        {
            enemyPool.Release(gameObject);
        }
    }
}