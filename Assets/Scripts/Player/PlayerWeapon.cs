using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] lasers;
    [SerializeField] private AudioSource firingAudioSource;
    [SerializeField] private RectTransform crosshair;
    [SerializeField] private Transform targetPoint;
    [SerializeField] private float targetDistance = 100f;
    [SerializeField] private GameStateManager gameStateManager;
    [SerializeField] private Transform beamMuzzle;
    [SerializeField] private LineRenderer beamLine;
    [SerializeField] private ParticleSystem beamMuzzleGlow;
    [SerializeField, Min(0.05f)] private float beamHitInterval = 0.25f;
    [SerializeField, Min(1f)] private float beamRange = 1000f;
    private float nextBeamHitTime;

    private bool isFiring;
    private Camera mainCamera;
    private WeaponSelectionModel weaponSelectionModel;

    private void Awake()
    {
        if (gameStateManager == null)
        {
            Debug.LogError("PlayerWeapon 尚未指定 GameStateManager", this);
        }

        weaponSelectionModel = GetComponent<WeaponSelectionModel>();

        if (weaponSelectionModel == null)
        {
            Debug.LogError("PlayerWeapon 找不到 WeaponSelectionModel", this);
        }
        if (beamMuzzle == null || beamLine == null)
        {
            Debug.LogError("PlayerWeapon 未指定光束发射点或 Line Renderer", this);
        }
        else
        {
            beamLine.positionCount = 2;
            beamLine.useWorldSpace = true;
            beamLine.enabled = false;
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
            ProcessBeam();
            return;
        }

        ProcessFiring();
        MoveCrosshair();
        MoveTargetPoint();
        AimLasers();
        ProcessBeam();
    }

    private void OnFire(InputValue value)
    {
        isFiring = value.isPressed &&
                   gameStateManager != null &&
                   gameStateManager.CurrentState == GameState.Playing;
    }

    private void ProcessFiring()
    {
        bool fireDualLaser =
            isFiring &&
            weaponSelectionModel != null &&
            weaponSelectionModel.CurrentWeapon == WeaponType.DualLaser;

        foreach (ParticleSystem laser in lasers)
        {
            var emission = laser.emission;
            emission.enabled = fireDualLaser;
        }

        if (firingAudioSource == null) return;

        if (fireDualLaser && !firingAudioSource.isPlaying)
            firingAudioSource.Play();
        else if (!fireDualLaser && firingAudioSource.isPlaying)
            firingAudioSource.Stop();
    }
    private void ProcessBeam()
    {
        if (beamMuzzle == null || beamLine == null)
            return;

        bool showBeam =
            isFiring &&
            gameStateManager != null &&
            gameStateManager.CurrentState == GameState.Playing &&
            weaponSelectionModel != null &&
            weaponSelectionModel.CurrentWeapon == WeaponType.BeamLaser;

        beamLine.enabled = showBeam;
        UpdateBeamMuzzleGlow(showBeam);

        if (!showBeam)
            return;

        Vector3 start = beamMuzzle.position;
        Vector3 toAim = targetPoint.position - start;

        if (toAim.sqrMagnitude < 0.0001f)
        {
            beamLine.enabled = false;
            UpdateBeamMuzzleGlow(false);
            return;
        }

        Vector3 direction = toAim.normalized;
        Vector3 end = start + direction * beamRange;

        if (Physics.Raycast(
                start,
                direction,
                out RaycastHit hit,
                beamRange,
                Physics.DefaultRaycastLayers,
                QueryTriggerInteraction.Ignore))
        {
            end = hit.point;

            Enemy enemy = hit.collider.GetComponentInParent<Enemy>();

            if (enemy != null && Time.time >= nextBeamHitTime)
            {
                enemy.ProcessHit();
                nextBeamHitTime = Time.time + Mathf.Max(0.05f, beamHitInterval);
            }
        }

        beamLine.SetPosition(0, start);
        beamLine.SetPosition(1, end);
    }

    private void UpdateBeamMuzzleGlow(bool active)
    {
        if (beamMuzzleGlow == null)
            return;

        if (active && !beamMuzzleGlow.isPlaying)
        {
            beamMuzzleGlow.Play();
        }
        else if (!active && beamMuzzleGlow.isPlaying)
        {
            beamMuzzleGlow.Stop(
                true,
                ParticleSystemStopBehavior.StopEmittingAndClear);
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