using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private GameStateManager gameStateManager;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private RectTransform crosshair;

    private void OnEnable()
    {
        if (gameStateManager == null || gameOverPanel == null)
        {
            Debug.LogError("GameOverUI 缺少场景引用", this);
            enabled = false;
            return;
        }

        if (crosshair == null)
            Debug.LogWarning("GameOverUI 尚未指定 Crosshair", this);

        gameStateManager.StateChanged += HandleStateChanged;
        HandleStateChanged(gameStateManager.CurrentState);
    }

    private void OnDisable()
    {
        if (gameStateManager != null)
            gameStateManager.StateChanged -= HandleStateChanged;

        StopAllCoroutines();
    }

    private void HandleStateChanged(GameState state)
    {
        if (crosshair != null)
            crosshair.gameObject.SetActive(state != GameState.GameOver);

        gameOverPanel.SetActive(false);

        if (state == GameState.GameOver)
            StartCoroutine(ShowPanelAfterDelay());
    }

    private IEnumerator ShowPanelAfterDelay()
    {
        yield return new WaitForSecondsRealtime(1f);

        if (gameStateManager.CurrentState == GameState.GameOver)
            gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
