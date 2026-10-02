using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem), typeof(AudioSource))]
public sealed class ExplosionEffectLifetime : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnParticleSystemStopped()
    {
        StartCoroutine(DestroyAfterAudio());
    }

    private IEnumerator DestroyAfterAudio()
    {
        while (audioSource.isPlaying)
        {
            yield return null;
        }

        Destroy(gameObject);
    }
}
