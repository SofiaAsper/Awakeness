using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxInteractable : Interactable
{

    Animator animator;
    bool isOpen = false;

    public override void Start()
    {
        animator = GetComponent<Animator>();
        base.Start();
        if (isOpen == false)
        {
            onInteract.AddListener(delegate { animator.SetTrigger("open"); });
            isOpen = true;
        }
        // when box is open,  hide ui



    }
}
