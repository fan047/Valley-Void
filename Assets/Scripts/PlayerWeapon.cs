using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] GameObject[] lasers;
    [SerializeField] RectTransform crosshair;
    [SerializeField] Transform targetPoint;
    [SerializeField] float targetDistance = 100f;



    bool isFiring = false;

    Camera mainCamera;

    void Start()
    {
        Cursor.visible = false;
        mainCamera = Camera.main;
    }


    void Update()
    {
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
        foreach (var laser in lasers)
        {
            var emmisionModule = laser.GetComponent<ParticleSystem>().emission;
            emmisionModule.enabled = isFiring;
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
        foreach (GameObject laser in lasers)
        {
            Vector3 fireDirection = targetPoint.position - this.transform.position;
            Quaternion rotationToTarget = Quaternion.LookRotation(fireDirection);
            laser.transform.rotation = rotationToTarget;
        }
    }
}
