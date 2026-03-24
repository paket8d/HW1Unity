using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject hudPanel;
    [SerializeField] private GameObject pausePanel;
    
    [Header("HUD Text")]
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text bestScoreText;
    
    [Header("Main Menu Text")]
    [SerializeField] private TMP_Text mainMenuBestScoreText;
    
    [Header("References")]
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private GameManagerScript gameManager;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Spawner spawner;
    
    private bool _isPaused = false;
    private bool _isPlaying = false;

    private void Start()
    {
        MainMenu();
    }

    private void Update()
    {
        if (_isPlaying && !_isPaused)
        {
            UpdateHUD();
        }
    }
    
    public void StartGame()
    {
        if (spawner)
        {
            spawner.enabled = true;
        }

        if (playerMovement)
        {
            playerMovement.enabled = true;
        }
        
        _isPaused = false;
        _isPlaying = true;
        Time.timeScale = 1f;
        
        if (gameManager != null)
            gameManager.ResetRun();
        
        UpdateHUD();
        UpdateBestInMenu();
        
        mainMenuPanel.SetActive(false);
        pausePanel.SetActive(false);
        hudPanel.SetActive(true);
    }
    
    public void OnPause(InputAction.CallbackContext context)
    {
        if (!context.performed || !_isPlaying)
        {
            return;
        }
        
        if (_isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        if (!_isPlaying)
        {
            return;
        }
        
        if (spawner)
        {
            spawner.enabled = false;
        }

        if (playerMovement)
        {
            playerMovement.enabled = false;
        }
        
        _isPaused = true;
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        if (!_isPlaying)
        {
            return;
        }
        
        if (spawner)
        {
            spawner.enabled = true;
        }

        if (playerMovement)
        {
            playerMovement.enabled = true;
        }

        _isPaused = false;
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }

    public void MainMenu()
    {
        if (spawner)
        {
            spawner.enabled = false;
        }

        if (playerMovement)
        {
            playerMovement.enabled = false;
        }
        
        _isPlaying = false;
        _isPaused = false;
        Time.timeScale = 1f;
        
        mainMenuPanel.SetActive(true);
        pausePanel.SetActive(false);
        hudPanel.SetActive(false);
        
        UpdateBestInMenu();
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    private void UpdateHUD()
    {
        if (playerHealth && healthText)
        {
            healthText.text = $"Health: {playerHealth.CurrentHealth}";
        }
        
        if (gameManager)
        {
            if (scoreText)
            {
                scoreText.text = $"Score: {gameManager.CurrentScore}";
            }
            if (bestScoreText)
            {
                bestScoreText.text = $"Best: {gameManager.BestScore}";
            }
        }
    }
    
    private void UpdateBestInMenu()
    {
        if (mainMenuBestScoreText && gameManager)
        {
            mainMenuBestScoreText.text = $"Best Score: {gameManager.BestScore}";
        }
    }
}
