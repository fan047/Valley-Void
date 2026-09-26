using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] ParticleSystem[] lasers;
    [SerializeField] RectTransform crosshair;
    [SerializeField] Transform targetPoint;
    [SerializeField] float targetDistance = 100f;



    bool isFiring = false;

    Camera mainCamera;

    void Start()
    {
        

        mainCamera = Camera.main;
    }


    void Update()
    {

        if (PauseMenu.IsPaused)
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

    void OnFire(InputValue value)
    {
        isFiring = value.isPressed;
        
    }

    void ProcessFiring()
    {
        foreach (ParticleSystem laser in lasers)
        {
            var emissionModule = laser.emission;
            emissionModule.enabled = isFiring;
        }
    }

    void MoveCrosshair()
    {
        crosshair.position = Mouse.current.position.ReadValue();
    }

    void MoveTargetPoint()
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        mousePosition.z = targetDistance;
        targetPoint.position = mainCamera.ScreenToWorldPoint(mousePosition);
    }

    void AimLasers()
    {
        foreach (ParticleSystem laser in lasers)
        {
            Vector3 fireDirection = targetPoint.position - this.transform.position;
            Quaternion rotationToTarget = Quaternion.LookRotation(fireDirection);
            laser.transform.rotation = rotationToTarget;
        }
    }
}
