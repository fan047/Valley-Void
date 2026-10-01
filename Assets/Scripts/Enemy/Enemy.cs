using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject destroyedVFX;
    [SerializeField] private Transform vfxPoint;

    private int maxHitPoints;
    private int scoreValue;
    private int currentHitPoints;
    private bool isDead;
    private bool isConfigured;

    private Scoreboard scoreboard;
    private PooledEnemy pooledEnemy;

    private void Awake()
    {
        scoreboard = FindAnyObjectByType<Scoreboard>();
        pooledEnemy = GetComponent<PooledEnemy>();
    }

    private void OnEnable()
    {
        if (isConfigured)
        {
            ResetState();
        }
    }

    public void ApplyBalance(EnemyBalanceData balance)
    {
        maxHitPoints = balance.maxHitPoints;
        scoreValue = balance.scoreValue;
        isConfigured = true;
        ResetState();
    }

    private void ResetState()
    {
        currentHitPoints = maxHitPoints;
        isDead = false;
    }

    public void ProcessHit()
    {
        if (!isConfigured || isDead)
        {
            return;
        }

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