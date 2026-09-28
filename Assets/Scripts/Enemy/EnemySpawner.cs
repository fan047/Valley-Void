using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Serializable] public class WaveData
    {
        public Transform waveRoot;
        public EnemyType enemyType;
    }

    [SerializeField] private EnemyPool enemyPool;
    [SerializeField] private List<WaveData> waves;

    public void SpawnWave(int index)
    {
        if (index < 0 || index >= waves.Count)
        {
            Debug.LogWarning("Wave index 越界：" + index);
            return;
        }

        WaveData wave = waves[index];

        if (wave.waveRoot == null)
        {
            return;
        }

        foreach (Transform spawnPoint in wave.waveRoot)
        {
            SpawnEnemy(spawnPoint, wave.enemyType);
        }
    }

    private void SpawnEnemy(Transform spawnPoint, EnemyType enemyType)
    {
        GameObject enemy = enemyPool.Get(enemyType);

        enemy.transform.SetPositionAndRotation(
            spawnPoint.position,
            spawnPoint.rotation
        );
    }
}