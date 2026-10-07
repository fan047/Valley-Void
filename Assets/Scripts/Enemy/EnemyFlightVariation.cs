using UnityEngine;

public enum EnemyRoute
{
    Forward,
    Split,
    Weave,
    Sweep
}

// Moves the pooled enemy root; its child Timeline keeps playing its authored flight.
public class EnemyFlightVariation : MonoBehaviour
{
    private Vector3 spawnPosition;
    private Quaternion spawnRotation;
    private EnemyRoute route;
    private int slot;
    private int count;
    private int sweepDirection;
    private float elapsed;
    private bool configured;

    public void Configure(EnemyRoute newRoute, int newSlot, int newCount, int newSweepDirection)
    {
        spawnPosition = transform.position;
        spawnRotation = transform.rotation;
        route = newRoute;
        slot = newSlot;
        count = Mathf.Max(1, newCount);
        sweepDirection = Mathf.Clamp(newSweepDirection, -1, 1);
        elapsed = 0f;
        configured = true;
    }

    private void Update()
    {
        if (!configured || Time.deltaTime <= 0f)
            return;

        elapsed += Time.deltaTime;
        float progress = Mathf.Clamp01(elapsed / 10f);
        float side = count == 1 ? (slot % 2 == 0 ? -1f : 1f)
            : (slot - (count - 1) * 0.5f) / Mathf.Max(0.5f, (count - 1) * 0.5f);
        float lateral = 0f;
        float height = 0f;
        float yaw = 0f;
        float bank = 0f;

        switch (route)
        {
            case EnemyRoute.Split:
                lateral = side * 55f * Smooth(progress);
                yaw = side * 18f * Mathf.Sin(progress * Mathf.PI);
                bank = -side * 12f * Mathf.Sin(progress * Mathf.PI);
                break;
            case EnemyRoute.Weave:
                lateral = (side == 0f ? 1f : side) * 30f * Mathf.Sin(progress * Mathf.PI * 2f);
                height = 8f * Mathf.Sin(progress * Mathf.PI * 2f + slot);
                yaw = (side == 0f ? 1f : side) * 14f * Mathf.Cos(progress * Mathf.PI * 2f);
                bank = -yaw * 0.5f;
                break;
            case EnemyRoute.Sweep:
                float travelSide = sweepDirection == 0 ? -side : sweepDirection;
                lateral = travelSide * 105f * Smooth(progress);
                yaw = travelSide * 26f * Mathf.Sin(progress * Mathf.PI);
                bank = -travelSide * 16f * Mathf.Sin(progress * Mathf.PI);
                break;
        }

        transform.SetPositionAndRotation(
            spawnPosition + spawnRotation * new Vector3(lateral, height, 0f),
            spawnRotation * Quaternion.Euler(0f, yaw, bank));
    }

    private void OnDisable()
    {
        configured = false;
    }

    private static float Smooth(float t)
    {
        return t * t * (3f - 2f * t);
    }
}
