using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fpscount : MonoBehaviour
{
    Text fpscounter;
    public float deltaTime;
    private void Start()
    {
        fpscounter = transform.GetChild(0).GetComponent<Text>();
    }

    private void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        fpscounter.text = Mathf.Ceil(fps).ToString();
    }
}