using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject destroyedVFX;
    [SerializeField] Transform vfxPoint;

    [SerializeField] int maxHitPoints = 3;
    [SerializeField] int scoreValue = 10;

    int currentHitPoints;
    private bool isDead;

    Scoreboard scoreboard;
    PooledEnemy pooledEnemy;

    void Awake()
    {
        scoreboard = FindAnyObjectByType<Scoreboard>();
        pooledEnemy = GetComponent<PooledEnemy>();
    }

    void OnEnable()
    {
        currentHitPoints = maxHitPoints;
        isDead = false;
    }

  

    public void ProcessHit()
    {
        if (isDead)
            return;

        currentHitPoints--;

        if (currentHitPoints <= 0)
        {
            isDead = true;

            scoreboard.IncreaseScore(scoreValue);

            Instantiate(
                destroyedVFX,
                vfxPoint.position,
                Quaternion.identity
            );

            pooledEnemy.ReturnToPool();
        }
    }
}