using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject destroyedVFX;

    private void OnParticleCollision(GameObject other) 
    {
        Instantiate(destroyedVFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
