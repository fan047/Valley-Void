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
        ParticleDamageSource source = other.GetComponent<ParticleDamageSource>();
        if (source == null || enemy == null)
            return;

        enemy.ProcessHit(source.Damage);
    }
}