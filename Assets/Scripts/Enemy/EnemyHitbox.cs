using UnityEngine;

public class EnemyHitbox : MonoBehaviour
{
    private Enemy enemy;

    private void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    private void OnParticleCollision(GameObject other)
    {
        enemy.ProcessHit();
    }
}