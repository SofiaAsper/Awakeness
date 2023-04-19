using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEditor;
public class PlatformSelectUI : MonoBehaviour
{
    private string platform;
    [SerializeField]
    bool TurnOnMobileSetting = false;
    // Start is called before the first frame update
    void Start()
    {
        platform = SystemInfo.operatingSystem;

        if (platform.Contains("Windows") || (Input.GetJoystickNames().Length > 0))
        {
            // EditorUtility.DisplayDialog("hi im conntroller", Input.GetJoystickNames().ToString(), "OK", "Cancel");
            gameObject.SetActive(false);
        }
        else if (platform.Contains("iOS") || platform.Contains("Android"))
        {
            gameObject.SetActive(true);
        }
        if (TurnOnMobileSetting)
        {
            gameObject.SetActive(true);
        }
    }


}
