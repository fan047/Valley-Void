using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameStateManager gameStateManager;
    
   
    private void OnEnable()
    {
        if (gameStateManager == null || pauseMenu == null)
        {
            Debug.LogError("PauseMenu 缺少场景引用", this);
            enabled = false;
            return;
        }

        gameStateManager.StateChanged += HandleStateChanged;
        HandleStateChanged(gameStateManager.CurrentState);
    }

    private void OnDisable()
    {
        if (gameStateManager != null)
        {
            gameStateManager.StateChanged -= HandleStateChanged;
        }
    }

    private void Update()
    {
        if (Keyboard.current == null ||
            !Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            return;
        }

        if (gameStateManager.CurrentState == GameState.Playing)
        {
            PauseGame();
        }
        else if (gameStateManager.CurrentState == GameState.Paused)
        {
            ResumeGame();
        }
    }

    public void PauseGame()
    {
        gameStateManager.PauseGame();
    }

    public void ResumeGame()
    {
        gameStateManager.ResumeGame();
    }

    public void ReturnToTitle()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("Title Screen");
    }

        private void HandleStateChanged(GameState state)
    {
        bool isPaused = state == GameState.Paused;
        pauseMenu.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;

        Cursor.visible = state != GameState.Playing;
        Cursor.lockState = state == GameState.Playing
            ? CursorLockMode.Confined
            : CursorLockMode.None;
    }
}