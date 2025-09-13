using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMovement : MonoBehaviour
{
    Vector2 _mouseAbsolute;
    Vector2 _smoothMouse;

    public GameObject characterBody;
    public FixedTouchField touchField;
    public FixedJoystick shootJoystick;
    private PlayerInput playerInput;
    public Vector2 sensitivity;
    public Vector2 savedRegSensitivity;
    public Vector2 savedShootSensitivity;
    public PauseGame pauseGame;
    public SaveData saveData;
    [SerializeField]
    private Vector2 clampInDegrees = new Vector2(360, 180);
    public Vector2 joystickSensitivity = new Vector2(2, 2);
    public Vector2 shootingSensitivity = new Vector2(2, 2);
    public Vector2 mouseSensitivity = new Vector2(2, 2);
    public Vector2 touchFieldSensitivity = new Vector2(2, 2);
    [SerializeField]
    private Vector2 smoothing = new Vector2(3, 3);
    [SerializeField]
    private Vector2 targetDirection;
    [SerializeField]
    private Vector2 targetCharacterDirection;
    public string platform;




    void Start()
    {
        playerInput = GetComponentInParent<PlayerInput>();
        // Set target direction to the camera's initial orientation.
        targetDirection = transform.localRotation.eulerAngles;

        // Set target direction for the character body to its inital state.
        if (characterBody)
            targetCharacterDirection = characterBody.transform.localRotation.eulerAngles;

        platform = SystemInfo.operatingSystem;
        LimitFPS();
        // if (saveData.fileExist && saveData.sensitivity.x != 0)
        // {
        //     joystickSensitivity = saveData.sensitivity;
        //     mouseSensitivity = saveData.sensitivity;
        //     touchFieldSensitivity = saveData.sensitivity;
        //     shootingSensitivity = saveData.shootingControllerSensitivity;

        // }
    }


    void Update()
    {
        if (!pauseGame.pausedGame)
        {
            UpdateSequance();
        }

    }

    void UpdateSequance()
    {
        Cursor.visible = false;
        // Cursor.lockState = CursorLockMode.Locked;                                         // disabled so the joystick works good!
        // Allow the script to clamp based on a desired target value.
        var targetOrientation = Quaternion.Euler(targetDirection);
        var targetCharacterOrientation = Quaternion.Euler(targetCharacterDirection);

        // Get raw mouse input for a cleaner reading on more sensitive mice.

        // var mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        var mouseDelta = new Vector2();
        if (Input.GetJoystickNames().Length > 0)
        {
            mouseDelta = new Vector2(Input.GetAxisRaw("JHorizontal"), Input.GetAxisRaw("JVertical"));
            sensitivity = joystickSensitivity;
            savedRegSensitivity = joystickSensitivity;
        }
        else if (platform.Contains("Windows") || platform.Contains("Mac"))
        {
            mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
            sensitivity = mouseSensitivity;
            savedRegSensitivity = mouseSensitivity;
        }
        else if (platform.Contains("iOS") || platform.Contains("Android"))
        {
            if (touchField.Pressed)
            {
                mouseDelta = new Vector2(touchField.TouchDist.x, touchField.TouchDist.y);
                sensitivity = touchFieldSensitivity;
                savedRegSensitivity = touchFieldSensitivity;
            }
            else
            {
                mouseDelta = new Vector2(shootJoystick.Horizontal, shootJoystick.Vertical);
                sensitivity = shootingSensitivity;
                savedShootSensitivity = shootingSensitivity;
            }
        }

        // Scale input against the sensitivity setting and multiply that against the smoothing value.
        mouseDelta = Vector2.Scale(mouseDelta, new Vector2(sensitivity.x * smoothing.x, sensitivity.y * smoothing.y));

        // Interpolate mouse movement over time to apply smoothing delta.
        _smoothMouse.x = Mathf.Lerp(_smoothMouse.x, mouseDelta.x, 1f / smoothing.x);
        _smoothMouse.y = Mathf.Lerp(_smoothMouse.y, mouseDelta.y, 1f / smoothing.y);

        // Find the absolute mouse movement value from point zero.
        _mouseAbsolute += _smoothMouse;
        // Clamp and apply the local x value first, so as not to be affected by world transforms.
        if (clampInDegrees.x < 360)
            _mouseAbsolute.x = Mathf.Clamp(_mouseAbsolute.x, -clampInDegrees.x * 0.5f, clampInDegrees.x * 0.5f);

        // Then clamp and apply the global y value.
        if (clampInDegrees.y < 360)
            _mouseAbsolute.y = Mathf.Clamp(_mouseAbsolute.y, -clampInDegrees.y * 0.5f, clampInDegrees.y * 0.5f);

        transform.localRotation = Quaternion.AngleAxis(-_mouseAbsolute.y, targetOrientation * Vector3.right) * targetOrientation;

        // If there's a character body that acts as a parent to the camera
        if (characterBody)
        {
            var yRotation = Quaternion.AngleAxis(_mouseAbsolute.x, Vector3.up);
            characterBody.transform.localRotation = yRotation * targetCharacterOrientation;
        }
        else
        {
            var yRotation = Quaternion.AngleAxis(_mouseAbsolute.x, transform.InverseTransformDirection(Vector3.up));
            transform.localRotation *= yRotation;
        }
    }

    void LimitFPS()
    {
        if (platform.Contains("Windows") || platform.Contains("Mac"))
        {
            Application.targetFrameRate = 60;
        }
        else if (platform.Contains("iOS") || platform.Contains("Android"))
        {
            Application.targetFrameRate = 30;
        }
    }

    public void AddRecoil(Vector3 recoil, float time)
    {
        if (!platform.Contains("iOS") || !platform.Contains("Android"))
        {
            float recoilElapsed = 0;
            StartCoroutine(recoilIncrease());
            IEnumerator recoilIncrease()
            {
                while (recoilElapsed < time)
                {
                    recoilElapsed += Time.deltaTime;
                    _mouseAbsolute += (Vector2)(recoil * Time.deltaTime / time);
                    yield return null;
                }
            }

        }
    }

}
