using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseGame : MonoBehaviour
{
   public bool pausedGame = false;
   
   private void Update() {
    if (Input.GetKeyDown(KeyCode.Escape)|| Input.GetKeyDown(KeyCode.F))
       {
             if (pausedGame == false)
             {
                Time.timeScale = 0;
                pausedGame = true;
             }
             else
             {
                Time.timeScale = 1;
                pausedGame = false;
             }
       }


   }


}









