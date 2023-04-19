using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    private Joystick joystick;
    public Button jumpButton;
    public Button shootButton;
    public Button crouchButton;
    public Button reloadButton;
    public bool toChange = true;
    private string platform;

    public GunController gunController;


    private void Awake()
    {
        platform = SystemInfo.operatingSystem;
    }
    public Vector2 input
    {
        get
        {
            Vector2 i = Vector2.zero;

            if (platform.Contains("Windows") || (Input.GetJoystickNames().Length > 0))
            {
                i.x = Input.GetAxis("Horizontal");
                i.y = Input.GetAxis("Vertical");
            }
            else if (platform.Contains("iOS") || platform.Contains("Android"))
            {
                i.x = joystick.Horizontal;
                i.y = joystick.Vertical;
            }
            i *= (i.x != 0.0f && i.y != 0.0f) ? .7071f : 1.0f;
            return i;
        }
    }

    public Vector2 down
    {
        get { return _down; }
    }

    public Vector2 raw
    {
        get
        {
            Vector2 i = Vector2.zero;
            if (platform.Contains("Windows") || (Input.GetJoystickNames().Length > 0))
            {
                i.x = Input.GetAxis("Horizontal");
                i.y = Input.GetAxis("Vertical");
            }
            else if (platform.Contains("iOS") || platform.Contains("Android"))
            {
                i.x = joystick.Horizontal;
                i.y = joystick.Vertical;
            }
            i *= (i.x != 0.0f && i.y != 0.0f) ? .7071f : 1.0f;
            return i;
        }
    }

    public float elevate
    {
        get
        {
            return Input.GetAxis("Elevate");
        }
    }


    [HideInInspector]
    public bool run
    {
        get
        {
            if (Input.GetJoystickNames().Length > 0)
            {
                return Input.GetKey(KeyCode.JoystickButton8);
            }
            else if (platform.Contains("Windows"))
            {
                return Input.GetKey(KeyCode.LeftShift);
            }
            else if (platform.Contains("iOS") || platform.Contains("Android"))
            {
                return joystick.Vertical >= 0.7f ? true : false;
            }
            else return joystick.Vertical >= 0.7f ? true : false;
        }
    }

    private bool Crouch;
    public bool crouch
    {
        get
        {
            if (Input.GetJoystickNames().Length > 0)
            {
                return Input.GetKey(KeyCode.JoystickButton1);
            }
            else if (platform.Contains("Windows"))
            {
                return Input.GetKey(KeyCode.LeftControl);
            }
            else if (platform.Contains("iOS") || platform.Contains("Android"))
            {
                return Crouch;
            }
            else return false;

        }
        set { Crouch = value; }
    }

    [HideInInspector]
    // public bool crouch;
    public void IsCrouch()
    {
        Crouch = !Crouch;
        crouchButton.image.color = (crouchButton.image.color == new Color(0, 0, 0, 0.4f)) ? new Color(1, 0.9f, 0, 0.4f) : new Color(0, 0, 0, 0.4f);
    }

    public bool crouching
    {
        get
        {
            if (Input.GetJoystickNames().Length > 0)
            {
                return Input.GetKeyDown(KeyCode.JoystickButton1);
            }
            else if (platform.Contains("Windows"))
            {
                return Input.GetKeyDown(KeyCode.LeftControl);
            }
            else { return false; }
        }
    }


    public KeyCode interactKey
    {
        get { return KeyCode.E; }
    }


    private bool Interact;
    public bool interact
    {
        get
        {
            if (Input.GetJoystickNames().Length > 0)
            {
                return Input.GetKey(KeyCode.JoystickButton6);
            }
            else if (platform.Contains("Windows"))
            {
                return Input.GetKeyDown(interactKey);
            }
            else if (platform.Contains("iOS") || platform.Contains("Android"))
            {
                return Interact;
            }
            else return false;

        }
        set { Interact = value; }
    }

    public void OnInteraction()
    {
        Interact = true;
    }
    public void StopInteraction()
    {
        Interact = false;
    }


    private bool Reload;
    public bool reload
    {
        get
        {
            if (Input.GetJoystickNames().Length > 0)
            {
                return Input.GetKey(KeyCode.JoystickButton2);
            }
            else if (platform.Contains("Windows"))
            {
                return Input.GetKey(KeyCode.R);
            }
            else if (platform.Contains("iOS") || platform.Contains("Android"))
            {
                return Reload;
            }
            else return false;

        }
        set { Reload = value; }
    }


    public void IsReload()
    {
        Reload = true;
        Invoke("StopReload", 0.2f);
    }
    void StopReload()
    {
        Reload = false;
        aim = false;
    }


    private bool Aim;
    public bool aim
    {
        get
        {
            if (Input.GetJoystickNames().Length > 0)
            {
                return Input.GetKey(KeyCode.JoystickButton4);
            }
            else if (platform.Contains("Windows"))
            {
                return Input.GetMouseButtonDown(1);
            }
            else if (platform.Contains("iOS") || platform.Contains("Android"))
            {
                return Aim;
            }
            else return false;

        }
        set { Aim = value; }
    }
    public void IsAim()
    {
        Aim = !Aim;
    }

    private bool Aiming;
    public bool aiming
    {
        get
        {
            if (Input.GetJoystickNames().Length > 0)
            {
                return Input.GetKey(KeyCode.JoystickButton4);
                // if (Input.GetAxisRaw("AimJ") != 0)
                // {
                //     return true;
                // }
                // if (Input.GetAxisRaw("AimJ") == 0)
                // {
                //     return false;
                // }
                // else return false;
            }
            else if (platform.Contains("Windows"))
            {
                return Input.GetMouseButton(1);
            }
            else if (platform.Contains("iOS") || platform.Contains("Android"))
            {
                return Aiming;
            }
            else return false;
        }
        set { Aiming = value; }
    }
    public void StartAiming() 
    {
        if (gunController.GetGunAim() == true)
        {
            return;
        }
        Aiming = true; 
    }
    public void StopAiming() 
    {
        if (gunController.GetGunAim() == true)
        {
            return;
        }
        Aiming = false; 
    }


    private bool Shooting;
    public bool shooting
    {
        get
        {
            if (Input.GetJoystickNames().Length > 0)
            {
                return Input.GetKey(KeyCode.JoystickButton5);
            }
            else if (platform.Contains("Windows"))
            {
                return Input.GetMouseButton(0);
            }
            else if (platform.Contains("iOS") || platform.Contains("Android"))
            {
                return Shooting;
            }
            else return false;

        }
        set
        {
            Shooting = value;
        }
    }
    public void StartShooting()
    {
        if (gunController.GetDelayShootOnMobile() > 0 && !Aim) Invoke("StartShootingOnMobile", gunController.GetDelayShootOnMobile() * 0.6f);
        else Shooting = true;
    }
    void StartShootingOnMobile()
    {
        Shooting = true;
    }

    public void StopShooting()
    {
        Shooting = false;
    }

    public float mouseScroll
    {
        get { return Input.GetAxisRaw("Mouse ScrollWheel"); }
    }

    public float gunSwitch;
    public void NextGunSwitch() { gunSwitch = 1; }
    public void StopGunSwitch()
    {
        gunSwitch = 0;
        toChange = true;
    }
    public void PrevGunSwitch() { gunSwitch = -1; }


    private Vector2 previous;
    private Vector2 _down;

    private int jumpTimer;
    private bool jump;

    void Start()
    {
        jumpTimer = -1;
    }

    void Update()
    {

        _down = Vector2.zero;
        if (raw.x != previous.x)
        {
            previous.x = raw.x;
            if (previous.x != 0)
                _down.x = previous.x;
        }
        if (raw.y != previous.y)
        {
            previous.y = raw.y;
            if (previous.y != 0)
                _down.y = previous.y;
        }
        
    }

    bool jumpPressed;
    public void JumpPressed()
    {
        jumpPressed = true;
        Crouch = false;
        crouchButton.image.color = new Color(0, 0, 0, 0.4f);
    }
    public void JumpReleased()
    {
        jumpPressed = false;
    }

    public void FixedUpdate()
    {
        if (Input.GetJoystickNames().Length > 0)
        {
            if (!Input.GetKey(KeyCode.JoystickButton0))
            {
                jump = false;
                jumpTimer++;
            }
            else if (jumpTimer > 0)
                jump = true;
        }
        else if (platform.Contains("Windows"))
        {
            if (!Input.GetKey(KeyCode.Space))
            {
                jump = false;
                jumpTimer++;
            }
            else if (jumpTimer > 0)
                jump = true;
        }
        else if (platform.Contains("iOS") || platform.Contains("Android"))
        {
            if (!jumpPressed)
            {
                jump = false;
                jumpTimer++;
            }
            else if (jumpTimer > 0)
                jump = true;
        }
    }

    public bool Jump()
    {
        return jump;
    }

    public void ResetJump()
    {
        jumpTimer = -1;
    }
}
