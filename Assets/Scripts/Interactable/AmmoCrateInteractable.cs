using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCrateInteractable : Interactable
{
    Animator animator;

    
   public override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        GunController guns = null;
        if ((guns = FindObjectOfType<GunController>()) != null)
        {
            onInteract.AddListener(guns.RefillAmmo);
            onInteract.AddListener(delegate { animator.SetTrigger("open"); });   
        }
    }
}
