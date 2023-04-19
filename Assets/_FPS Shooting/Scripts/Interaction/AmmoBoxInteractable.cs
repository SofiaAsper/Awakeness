using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBoxInteractable : Interactable
{
    [SerializeField]
    Animator animator;

    public override void Start()
    {
        animator = GetComponent<Animator>();
        base.Start();
        GunController guns = null;
        if ((guns = FindObjectOfType<GunController>()) != null)
        {
            onInteract.AddListener(guns.RefillAmmo);
            if (animator!=null)
            {
            onInteract.AddListener(delegate { animator.SetTrigger("open"); });
            }

        }
    }
}
