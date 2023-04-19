using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    [SerializeField] Canvas gameOverCanvas;

    private void Start() 
    {
        gameOverCanvas.enabled = false;        
    }

    public void HandleDeath()
    {
        gameOverCanvas.enabled = true;

        // For a PC game - make the cursor visible and avilable for clicking the buttons - not relevat for phone game..//
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }



}
