using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunInteract : Interactable
{
    public override void Start()
    {
        base.Start();
        GunPickup pickUp;
        if ((pickUp = gameObject.GetComponent<GunPickup>()) != null)
        {
            onInteract.AddListener(pickUp.PickupAndGivePlayerGun);

        }
    }
}
