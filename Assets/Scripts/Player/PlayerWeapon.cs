using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] lasers;
    [SerializeField] private RectTransform crosshair;
    [SerializeField] private Transform targetPoint;
    [SerializeField] private float targetDistance = 100f;
    [SerializeField] private GameStateManager gameStateManager;

    private bool isFiring;
    private Camera mainCamera;

    private void Awake()
    {
        if (gameStateManager == null)
        {
            Debug.LogError("PlayerWeapon 尚未指定 GameStateManager", this);
        }
    }

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (gameStateManager == null ||
            gameStateManager.CurrentState != GameState.Playing)
        {
            isFiring = false;
            ProcessFiring();
            return;
        }

        ProcessFiring();
        MoveCrosshair();
        MoveTargetPoint();
        AimLasers();
    }

    private void OnFire(InputValue value)
    {
        isFiring = value.isPressed &&
                   gameStateManager != null &&
                   gameStateManager.CurrentState == GameState.Playing;
    }

    private void ProcessFiring()
    {
        foreach (ParticleSystem laser in lasers)
        {
            var emissionModule = laser.emission;
            emissionModule.enabled = isFiring;
        }
    }

    private void MoveCrosshair()
    {
        crosshair.position = Mouse.current.position.ReadValue();
    }

    private void MoveTargetPoint()
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        mousePosition.z = targetDistance;
        targetPoint.position = mainCamera.ScreenToWorldPoint(mousePosition);
    }

    private void AimLasers()
    {
        foreach (ParticleSystem laser in lasers)
        {
            Vector3 fireDirection = targetPoint.position - transform.position;
            laser.transform.rotation = Quaternion.LookRotation(fireDirection);
        }
    }
}