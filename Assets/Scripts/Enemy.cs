using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject destroyedVFX;
    [SerializeField] Transform vfxPoint;
    [SerializeField] int hitPoints = 3;
    [SerializeField] int scoreValue = 10;



    Scoreboard scoreboard;

    void Start()
    {
        scoreboard = FindAnyObjectByType<Scoreboard>();
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }

    private void ProcessHit()
    {
        hitPoints--;

        if (hitPoints <= 0)
        {
            scoreboard.IncreaseScore(scoreValue);
            Instantiate(destroyedVFX, vfxPoint.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
