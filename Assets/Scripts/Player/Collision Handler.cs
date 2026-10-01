using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private GameObject destroyedVFX;
    [SerializeField] private Transform vfxPoint;
    [SerializeField] private GameStateManager gameStateManager;

    private bool hasCollided;

    private void Awake()
    {
        if (gameStateManager == null)
        {
            Debug.LogError("CollisionHandler 尚未指定 GameStateManager", this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasCollided ||
            gameStateManager == null ||
            gameStateManager.CurrentState != GameState.Playing)
        {
            return;
        }

        hasCollided = true;

        Debug.Log("撞到了：" + other.name);
        Instantiate(destroyedVFX, vfxPoint.position, Quaternion.identity);

        gameStateManager.EndGame();
        Destroy(gameObject);
    }
}