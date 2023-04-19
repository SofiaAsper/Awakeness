using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "MeleObject", menuName = "Mele Object", order = 2)]
public class MeleObject : ScriptableObject
{
    public enum MeleType { two_hands, one_hand };
    // public enum ShootType { semi, auto, burst };

    public string prefabName;
    public GameObject prefabObj;
    public TransformData prefabLocalData;

    //[Header("IK Variables")]
    public TransformData IK_HandTarget;

    //[Header("Shoot Process")]
    // public MeleType shootType;
    public GameObject muzzleFlash;
    // public PooledObject impactEffect;
    // public float initialForce = 100f;
    // public float additiveForce = 10f;

    //[Header("Gun Variables")]


    //[Header("Aim Variables")]
    public float aimFOV = 60f;
    public float aimDownSpeed = 8f;
    public TransformData ironSightAim;

    //[Header("Damage Variables")]
    public float hitDamage = 10f;
    [Range(1f, 2f)]
    public float headshotMult = 1.25f;



    //[Header("Animation Variables")]
    public RuntimeAnimatorController animationController;
    public GunAnimations meleMotions;

    //[Header("UI Variables")]
    public Sprite meleIcon;


    //[Header("SFX Variables")]
    public AudioClip[] hitClips;


    //     public void AddTempGunToPlayer() //The difference here is that it will not change the ArmIk or Prefab varaibles
    //     {
    //         GunController controller = null;
    //         if ((controller = FindObjectOfType<GunController>()) != null)
    //             // controller.AddMeleTemporarily(this);
    //     }

    // #if UNITY_EDITOR
    //     public void GivePlayerGun()
    //     {
    //         GunController controller = null;
    //         if ((controller = FindObjectOfType<GunController>()) != null)
    //             // controller.AddMele(this);
    //     }
    // #endif
}
