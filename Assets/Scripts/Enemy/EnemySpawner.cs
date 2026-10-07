using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Serializable]
    public class WaveData
    {
        public Transform waveRoot;
        public EnemyType enemyType;
    }

    [SerializeField] private EnemyPool enemyPool;
    [SerializeField] private List<WaveData> waves;

    private int currentWaveIndex = 0;

    public void SpawnNextWave()
    {
        if (currentWaveIndex >= waves.Count)
        {
            Debug.LogWarning("所有波次已经生成完毕");
            return;
        }

        WaveData wave = waves[currentWaveIndex];

        if (wave.waveRoot != null)
        {
            foreach (Transform spawnPoint in wave.waveRoot)
            {
                SpawnEnemy(spawnPoint, wave.enemyType);
            }
        }

        currentWaveIndex++;
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
