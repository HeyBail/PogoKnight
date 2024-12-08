using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;

    public static bool isPaused;
    public MonoBehaviour PlayerMovement;

    void Start()
    {
        pauseMenu.SetActive(false);
        isPaused = false;  // Reset pause state
        Time.timeScale = 1f;  // Ensure game is running at normal speed
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void OnPause(InputValue value)
    {
        if (isPaused)
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
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

        // Show the cursor
        Cursor.lockState = CursorLockMode.None; // Unlocks the cursor
        Cursor.visible = true; // Makes the cursor visible

        // Disable player movement
        if (PlayerMovement != null) 
        {
            PlayerMovement.enabled = false; // Disables the player movement script
        }
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

        // Hide the cursor
        Cursor.lockState = CursorLockMode.Locked; // Locks the cursor back to the center
        Cursor.visible = false; // Hides the cursor

        // Enable player movement
        if (PlayerMovement != null)
        {
            PlayerMovement.enabled = true; // Enables the player movement script again
        }
    }

    public void GoToMainMenu()
    {
        GameManager.Instance.setPlayerTransformData(FindFirstObjectByType<PlayerMovement>().gameObject.transform);
        GameManager.Instance.lastLevelName = SceneManager.GetActiveScene().name;

        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        isPaused = false;
    }

    public void QuitGame()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
