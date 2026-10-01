using UnityEngine;

public class EnemyUI : MonoBehaviour
{
    [SerializeField] private RectTransform markerPrefab;

    private RectTransform marker;
    private Camera mainCamera;
    private Transform markerRoot;

    private void Awake()
    {
        mainCamera = Camera.main;

        GameObject rootObject = GameObject.Find("EnemyMarkers");

        if (rootObject != null)
        {
            markerRoot = rootObject.transform;
        }
    }

    private void OnEnable()
    {
        if (marker == null && markerPrefab != null && markerRoot != null)
        {
            marker = Instantiate(markerPrefab, markerRoot);
        }

        if (marker != null)
        {
            marker.gameObject.SetActive(true);
        }
    }

    private void LateUpdate()
    {
        if (mainCamera == null || marker == null)
        {
            return;
        }

        Vector3 screenPosition =
            mainCamera.WorldToScreenPoint(transform.position);

        if (screenPosition.z > 0)
        {
            marker.gameObject.SetActive(true);
            marker.position = screenPosition;
        }
        else
        {
            marker.gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        if (marker != null)
        {
            marker.gameObject.SetActive(false);
        }
    }
}