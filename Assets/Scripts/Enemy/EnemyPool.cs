using System;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyPool : MonoBehaviour
{
    [Serializable] public class EnemyPoolData
    {
        public EnemyType type;
        public GameObject prefab;
        public int defaultCapacity = 5;
        public int maxSize = 20;

        [HideInInspector] public ObjectPool<GameObject> pool;
    }
    
    [SerializeField] private EnemyPoolData[] enemyPools;
    
    private Dictionary<EnemyType, EnemyPoolData> poolDictionary;

    private void Awake()
    {
        poolDictionary = new Dictionary<EnemyType, EnemyPoolData>();

        foreach(EnemyPoolData data in enemyPools)
        {
            data.pool = new ObjectPool<GameObject>(
                () => CreateEnemy(data),
                OnGetEnemy,
                OnReleaseEnemy,
                OnDestroyEnemy,
                true,
                data.defaultCapacity,
                data.maxSize
                );
            
            poolDictionary.Add(data.type, data);
        }
    }

    private GameObject CreateEnemy(EnemyPoolData data)
    {
        GameObject enemy = Instantiate(data.prefab);
        PooledEnemy pooledEnemy = enemy.GetComponent<PooledEnemy>();
        pooledEnemy.SetPool(this, data.type);
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

    public GameObject Get(EnemyType type)
    {
        return poolDictionary[type].pool.Get();
    }

    public void Release(GameObject enemy, EnemyType type)
    {
        poolDictionary[type].pool.Release(enemy);
    }
}