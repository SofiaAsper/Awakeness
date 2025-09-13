using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoading : MonoBehaviour
{
    [SerializeField] Slider loadingBar;
    // Start is called before the first frame update
    void Start()
    {
        loadingBar.value = 0f;
        // start async operation
        Invoke("LoadScene", 0.5f);
    }

    void LoadScene()
    {
        StartCoroutine(LoadAsyncScene());
    }

    IEnumerator LoadAsyncScene()
    {
        //create an async operation
        AsyncOperation gamePlay = SceneManager.LoadSceneAsync(2);
        while (gamePlay.progress < 1)
        {
            // take the progress bar and fill with async operation progress
            loadingBar.value = gamePlay.progress;
            yield return new WaitForEndOfFrame();
        }
    }




}
