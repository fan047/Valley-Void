using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [Serializable]
    public class WaveData
    {
        public Transform waveRoot;
        public EnemyType enemyType;
        public EnemyRoute route;
        [Range(-1, 1)] public int sweepDirection;
        [Min(0f)] public float warningSeconds = 2f;
        [Min(0f)] public float staggerSeconds = 0.35f;
    }

    [SerializeField] private EnemyPool enemyPool;
    [SerializeField] private List<WaveData> waves;
    [SerializeField] private RectTransform warningMarkerPrefab;

    private int currentWaveIndex;
    private Canvas warningCanvas;
    private Camera mainCamera;

    private void Awake()
    {
        warningCanvas = FindAnyObjectByType<Canvas>();
        mainCamera = Camera.main;
    }

    public void SpawnNextWave()
    {
        if (waves == null || currentWaveIndex >= waves.Count)
        {
            Debug.LogWarning("所有波次已经生成完毕");
            return;
        }

        WaveData wave = waves[currentWaveIndex++];
        if (wave.waveRoot != null && enemyPool != null)
            StartCoroutine(SpawnWave(wave));
    }

    private IEnumerator SpawnWave(WaveData wave)
    {
        // Ghost is the only type that materializes without an approach cue.
        bool surprise = wave.enemyType == EnemyType.Ghost;
        float warningTime = surprise ? 0f : Mathf.Max(0f, wave.warningSeconds);
        RectTransform marker = null;
        Image image = null;

        if (warningTime > 0f && warningMarkerPrefab != null && warningCanvas != null)
        {
            marker = Instantiate(warningMarkerPrefab, warningCanvas.transform);
            marker.name = "Incoming enemy wave";
            image = marker.GetComponent<Image>();
            if (image != null)
                image.raycastTarget = false;
        }

        float elapsed = 0f;
        while (elapsed < warningTime)
        {
            UpdateWarning(marker, image, wave.waveRoot, elapsed);
            elapsed += Time.deltaTime;
            yield return null;
        }

        if (marker != null)
            Destroy(marker.gameObject);

        int count = wave.waveRoot.childCount;
        for (int index = 0; index < count; index++)
        {
            SpawnEnemy(wave.waveRoot.GetChild(index), wave.enemyType, wave.route,
                wave.sweepDirection, index, count);
            if (!surprise && index < count - 1 && wave.staggerSeconds > 0f)
                yield return new WaitForSeconds(wave.staggerSeconds);
        }
    }

    private void UpdateWarning(RectTransform marker, Image image, Transform waveRoot, float elapsed)
    {
        if (marker == null || mainCamera == null || waveRoot.childCount == 0)
            return;

        Vector3 center = Vector3.zero;
        foreach (Transform point in waveRoot)
            center += point.position;
        center /= waveRoot.childCount;

        Vector3 screen = mainCamera.WorldToScreenPoint(center);
        if (screen.z < 0f)
        {
            screen.x = Screen.width - screen.x;
            screen.y = Screen.height - screen.y;
        }
        marker.position = new Vector3(
            Mathf.Clamp(screen.x, 55f, Screen.width - 55f),
            Mathf.Clamp(screen.y, 55f, Screen.height - 55f), 0f);
        marker.localScale = Vector3.one * (0.42f + 0.08f * Mathf.Sin(elapsed * 12f));
        if (image != null)
            image.color = new Color(1f, 0.65f, 0.16f, 0.65f + 0.3f * Mathf.Sin(elapsed * 12f));
    }

    private void SpawnEnemy(Transform spawnPoint, EnemyType enemyType, EnemyRoute route,
        int sweepDirection, int index, int count)
    {
        GameObject enemy = enemyPool.Get(enemyType);
        enemy.transform.SetPositionAndRotation(spawnPoint.position, spawnPoint.rotation);
        EnemyFlightVariation flight = enemy.GetComponent<EnemyFlightVariation>();
        if (flight == null)
            flight = enemy.AddComponent<EnemyFlightVariation>();
        flight.Configure(route, index, count, sweepDirection);
    }
}
