using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LiveTextSpawner : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI liveText;
    [SerializeField] TextMeshProUGUI LevelUptxt;
    Coroutine lvlUpCo;
    Coroutine liveUpCo;
    private void Start()
    {
        //lvlUpCo = StartCoroutine(ShowLevelUp());
        //LevelUptxt.gameObject.SetActive(false);

        //liveUpCo = StartCoroutine(ShowLevelUp());
        //liveText.gameObject.SetActive(false);
    }
    public void SpawnFloatText(string text)
    {
        Vector3 randomPosition = RandomVec();
        liveText.text = text;
        liveText.color = Color.green;
        liveText.fontSize = 30;
        StartCoroutine(ShowLiveUp());
        //Instantiate(liveText, transform.position + randomPosition, Quaternion.identity, gameObject.transform);
    }
    IEnumerator ShowLiveUp()
    {
        liveText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        liveText.gameObject.SetActive(false);
    }
    Vector3 RandomVec()
    {
        float Offset = Random.Range(-100f, 100f);
        Vector3 randomPosition = new Vector3(Offset, Offset, Offset);
        return randomPosition;
    }

    public void LevelUpText()
    {
        LevelUptxt.text = "Level Up!";
        LevelUptxt.color = Color.magenta;
        LevelUptxt.fontSize = 60;
        //StopCoroutine(lvlUpCo);
        StartCoroutine(ShowLevelUp());
        //Instantiate(liveText, transform.position, Quaternion.identity, gameObject.transform);
    }
    IEnumerator ShowLevelUp()
    {
        LevelUptxt.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        LevelUptxt.gameObject.SetActive(false);
    }

}
