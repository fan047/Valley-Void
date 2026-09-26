using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyPool enemyPool;
    [SerializeField] private Transform[] spawnPoints;

    public void SpawnAt(int index)
    {
        if (index < 0 || index >= spawnPoints.Length)
        {
            Debug.LogWarning("SpawnPoint index 越界");
            return;
        }

        SpawnEnemy(spawnPoints[index]);
    }

    public void SpawnAll()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            SpawnEnemy(spawnPoint);
        }
    }

    private void SpawnEnemy(Transform spawnPoint)
    {
        GameObject enemy = enemyPool.Get();

        enemy.transform.SetPositionAndRotation(
            spawnPoint.position,
            spawnPoint.rotation
        );
    }
}