using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInteractable : Interactable
{
    [SerializeField] ShopManager shopManager;
    Animator animator;


public override void Start()
    {
        animator = GetComponent<Animator>();
        base.Start();

       onInteract.AddListener(shopManager.OpenShopMenu);

    }
}
