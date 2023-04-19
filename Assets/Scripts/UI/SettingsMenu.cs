using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using System.IO;


public class SettingsMenu : MonoBehaviour
{

    [HideInInspector] public string platform;
    public AudioMixer mainMixer;
    public AudioMixer SFXMixer;
    [SerializeField] private GameObject resOBJ;
    [SerializeField] private GameObject resToggle;
    Resolution[] resolutions;
    [SerializeField] Slider resSilider;
    [SerializeField] Slider sensSilider;
    [SerializeField] Slider sensShootSilider;
    [SerializeField] TextMeshProUGUI TMPtext;
    int currentResolutionIndex = 0;
    public CameraMovement cameraMovement;
    public SaveData saveData;


    private void Start()
    {
        platform = SystemInfo.operatingSystem;
        resolutions = Screen.resolutions;
        resSilider.maxValue = resolutions.Length - 1;
        // Resolution(.1f);
        resSilider.value = currentResolutionIndex;
        if (cameraMovement != null) sensSilider.value = cameraMovement.sensitivity.x;
        TMPtext.text = resolutions[currentResolutionIndex].width + "x" + resolutions[currentResolutionIndex].height;

        if ((platform.Contains("iOS") || platform.Contains("Android")))
        {
            resOBJ.SetActive(false);
            resToggle.SetActive(true);
        }
        else if (platform.Contains("Windows"))
        {
            resOBJ.SetActive(true);
            resToggle.SetActive(false);
        }
        if (cameraMovement != null)
        {
            sensSilider.onValueChanged.AddListener(delegate { SetSensitivity(); });
            sensShootSilider.onValueChanged.AddListener(delegate { SetShootSensitivity(); });
        }
    }

    public void ResolutionForPC(float res)
    {
        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
            if (i == res)
            {
                TMPtext.text = resolutions[i].width + "x" + resolutions[i].height;
                Screen.SetResolution(resolutions[i].width, resolutions[i].height, true);
            }
        }
    }
    public void ResolutionForMobile(float res)
    {
        QualitySettings.resolutionScalingFixedDPIFactor = res * 0.25f;
    }

    public void SetGSound(float value)
    {
        mainMixer.SetFloat("Volume", value);
    }
    public void SetSFX(float value)
    {
        SFXMixer.SetFloat("Volume", value);
    }

    public void Toggler(int type)
    {
        QualitySettings.SetQualityLevel(type);
    }

    // public void Resolution(float res)
    // {
    //     if ((platform.Contains("iOS") || platform.Contains("Android")))
    //     {
    //         ResolutionForMobile(.4f);
    //     }
    // if (platform.Contains("Windows"))
    // {
    //     ResolutionForPC(res);
    // }
    // }


    public void SetSensitivity()
    {
        float value = sensSilider.value;
        Debug.Log("set Sensitivity");
        if (Input.GetJoystickNames().Length > 0)
        {
            cameraMovement.joystickSensitivity = new Vector2(value, value);
        }
        else if (platform.Contains("Windows"))
        {
            cameraMovement.mouseSensitivity = new Vector2(value, value);
        }
        else if (platform.Contains("iOS") || platform.Contains("Android"))
        {
            cameraMovement.touchFieldSensitivity = new Vector2(value, value);
        }

    }
    public void SetShootSensitivity()
    {
        float value = sensShootSilider.value;
        cameraMovement.shootingSensitivity = new Vector2(value, value);
    }

}
