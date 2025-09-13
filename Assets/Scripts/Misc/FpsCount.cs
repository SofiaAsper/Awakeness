using UnityEngine;
using UnityEngine.UI;

public class FpsCount : MonoBehaviour
{
    [SerializeField] private Text fpsCounter;
    private float deltaTime;

    private void Awake()
    {
        if (fpsCounter == null)
        {
            fpsCounter = GetComponentInChildren<Text>();
        }
    }

    private void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1f / Mathf.Max(deltaTime, 0.00001f);
        if (fpsCounter != null)
            fpsCounter.text = Mathf.CeilToInt(fps).ToString();
    }
}