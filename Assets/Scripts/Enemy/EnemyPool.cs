using System;
using System.Collections.Generic;
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
    [SerializeField] private GameBalanceConfig balanceConfig;
    
    private Dictionary<EnemyType, EnemyPoolData> poolDictionary;

    private void Awake()
    {
        if (balanceConfig == null)
            throw new InvalidOperationException("EnemyPool 未指定 GameBalanceConfig");

        if (!balanceConfig.TryValidateEnemies(out string error))
            throw new InvalidOperationException(error);

        poolDictionary = new Dictionary<EnemyType, EnemyPoolData>();

        foreach(EnemyPoolData data in enemyPools)
        {
            if (!balanceConfig.TryGetEnemyBalance(data.type, out _))
                throw new InvalidOperationException($"EnemyPool 的 {data.type} 缺少敌人数值配置");
                
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
        if (balanceConfig == null)
        {
            throw new InvalidOperationException("EnemyPool 未指定 GameBalanceConfig");
        }

        if (!balanceConfig.TryGetEnemyBalance(data.type, out EnemyBalanceData balance))
        {
            throw new InvalidOperationException($"配置中缺少 {data.type} 的敌人数据");
        }

        GameObject enemy = Instantiate(data.prefab);

        Enemy enemyComponent = enemy.GetComponent<Enemy>();
        PooledEnemy pooledEnemy = enemy.GetComponent<PooledEnemy>();

        enemyComponent.ApplyBalance(balance);
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