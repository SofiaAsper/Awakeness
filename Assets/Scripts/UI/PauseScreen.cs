using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseScreen : MonoBehaviour
{

    public Canvas pauseScreenCanvas;
    public Canvas shopCanvas;
    public PauseGame pauseGame;
    public static bool isPaused = false;
    [SerializeField] GameObject settingsCanvas;

//************************************************************************************************    
    void Start() {
        pauseScreenCanvas = GetComponent<Canvas>();
        settingsCanvas.SetActive(false);
        pauseScreenCanvas.enabled = false;
    }

    void LateUpdate()
    {
        if (!shopCanvas.enabled)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && pauseGame.pausedGame == true)
            {
                EnableUI();
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && pauseGame.pausedGame == false)
            {
                DisableUI();
            }

        }


    }

//************************************************************************************************
    public void EnableUI()
    {
        Time.timeScale = 0;
        pauseGame.pausedGame = true;
        pauseScreenCanvas.enabled = true;
    }

    public void DisableUI()
    {
        Time.timeScale = 1;
        pauseGame.pausedGame = false;
        pauseScreenCanvas.enabled = false;
    }
//************************************************************************************************
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ToggleSettings()
    {
        if (!settingsCanvas.activeInHierarchy)
        {
            settingsCanvas.SetActive(true);
        }
        else
        {
            settingsCanvas.SetActive(false);
        }
    }
}
