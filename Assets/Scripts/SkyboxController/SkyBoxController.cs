using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkyBoxController : MonoBehaviour
{
    public float dayNightCycle;
    public bool IsDayTime;
    public Color dayColor = new Color(1, .7f, .6f, 1);
    public Color nightColor = new Color(.2f, .0f, .2f, 1);
    [SerializeField] private TextMeshProUGUI dateText;

    public int hour;
    public int min;
    public int day;
    public int week;
    public int month;

    void Start()
    {
        //dayCycle
        min = 0;
        hour = 8;
        IsDayTime = true;
        //grad
        dayNightCycle = 0f;
        RenderSettings.fogColor = Color.LerpUnclamped(dayColor, nightColor, dayNightCycle);
        RenderSettings.skybox.SetFloat("_CubemapTransition", dayNightCycle);
        InvokeRepeating("Tick", 7.2f, 7.2f);
        InvokeRepeating("whatTime", 1f, 1f);
        // Debug.Log(RenderSettings.skybox.GetFloat("_CubemapTransition"));
    }

    void whatTime()
    {
        min += 1;
        if (min % 60 == 0)
        {
            hour += 1;
            min = 0;
            if (hour == 24)
            {
                day += 1;
                hour = 0;
                float negative = Random.Range(0f, .2f);
                float positive = Random.Range(.7f, 1f);
                dayColor = new Color(positive, positive, positive, 1);
                nightColor = new Color(negative, .0f, negative, 1);
            }
            if (day % 7 == 0)
            {
                week += 1;
            }
            if (week % 4 == 0)
            {
                month += 1;
            }
        }
        string timeText = string.Format("{0:D2}:{1:D2}", hour, min);
        dateText.text = timeText;
    }

    void Tick()
    {
        if (IsDayTime)
        {
            if (dayNightCycle > 0.99f)
                IsDayTime = false;

            RenderSettings.fogColor = Color.LerpUnclamped(dayColor, nightColor, dayNightCycle);
            RenderSettings.skybox.SetFloat("_CubemapTransition", dayNightCycle += 0.01f);
        }
        else if (!IsDayTime)
        {
            if (dayNightCycle < 0.1f)
                IsDayTime = true;

            RenderSettings.fogColor = Color.LerpUnclamped(nightColor, dayColor, 1 - dayNightCycle);
            RenderSettings.skybox.SetFloat("_CubemapTransition", dayNightCycle -= 0.01f);
        }
    }
}
