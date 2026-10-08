using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private GameStateManager gameStateManager;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private RectTransform crosshair;

    private void OnEnable()
    {
        if (gameStateManager == null || gameOverPanel == null || victoryPanel == null)
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
        bool gameEnded = state == GameState.GameOver ||
                        state == GameState.Victory;

        if (crosshair != null)
            crosshair.gameObject.SetActive(!gameEnded);

        gameOverPanel.SetActive(false);
        victoryPanel.SetActive(false);

        if (state == GameState.GameOver)
            StartCoroutine(ShowPanelAfterDelay(gameOverPanel, state));
        else if (state == GameState.Victory)
            StartCoroutine(ShowPanelAfterDelay(victoryPanel, state));
    }

   private IEnumerator ShowPanelAfterDelay(GameObject panel, GameState expectedState)
    {
        yield return new WaitForSecondsRealtime(1f);

        if (gameStateManager.CurrentState == expectedState)
            panel.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
