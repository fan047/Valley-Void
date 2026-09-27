using UnityEngine;
using UnityEngine.Pool;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;

    [SerializeField] private int defaultCapacity = 10;
    [SerializeField] private int maxSize = 30;

    private ObjectPool<GameObject> pool;

    private void Awake()
    {
        pool = new ObjectPool<GameObject>(
            CreateEnemy,
            OnGetEnemy,
            OnReleaseEnemy,
            OnDestroyEnemy,
            true,
            defaultCapacity,
            maxSize
        );
    }

    private GameObject CreateEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab);

        PooledEnemy pooledEnemy = enemy.GetComponent<PooledEnemy>();

        pooledEnemy.SetPool(this);

        enemy.SetActive(false);

        return enemy;
    }

    private void OnGetEnemy(GameObject enemy)
    {
        enemy.SetActive(true);
    }

    private void OnReleaseEnemy(GameObject enemy)
    {
        enemy.SetActive(false);
    }

    private void OnDestroyEnemy(GameObject enemy)
    {
        if (Application.isPlaying)
        {
            Destroy(enemy);
        }
        else
        {
            DestroyImmediate(enemy);
        }
    }

    public GameObject Get()
    {
        return pool.Get();
    }

    public void Release(GameObject enemy)
    {
        pool.Release(enemy);
    }
}