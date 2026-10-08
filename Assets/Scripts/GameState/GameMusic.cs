using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GameMusic : MonoBehaviour
{
    [SerializeField] private GameStateManager gameStateManager;
    [SerializeField] private AudioClip playingMusic;
    [SerializeField] private AudioClip gameOverMusic;
    [SerializeField] private AudioClip victoryMusic;

    private AudioSource musicSource;

    private void Awake()
    {
        musicSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        if (gameStateManager == null ||
            playingMusic == null ||
            gameOverMusic == null ||
            victoryMusic == null)
        {
            Debug.LogError("GameMusic 缺少引用", this);
            enabled = false;
            return;
        }

        gameStateManager.StateChanged += HandleStateChanged;
        HandleStateChanged(gameStateManager.CurrentState);
    }

    private void OnDisable()
    {
        if (gameStateManager != null)
            gameStateManager.StateChanged -= HandleStateChanged;
    }

    private void HandleStateChanged(GameState state)
    {
        if (state == GameState.Paused)
        {
            musicSource.Pause();
            return;
        }

        if (state == GameState.Playing)
        {
            if (musicSource.clip == playingMusic)
            {
                musicSource.UnPause();
                if (!musicSource.isPlaying)
                    musicSource.Play();
            }
            else
            {
                musicSource.Stop();
                musicSource.clip = playingMusic;
                musicSource.loop = true;
                musicSource.Play();
            }

            return;
        }

        if (state == GameState.GameOver)
        {
            musicSource.Stop();
            musicSource.clip = gameOverMusic;
            musicSource.loop = false;
            musicSource.Play();
        }

        if (state == GameState.Victory)
        {
            musicSource.Stop();
            musicSource.clip = victoryMusic;
            musicSource.loop = false;
            musicSource.Play();
        }
    }
}