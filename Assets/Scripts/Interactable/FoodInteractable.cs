using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodInteractable : Interactable
{
    [SerializeField] private float increaseHealth;
    GameObject player;
    PlayerAspects playerAspects;

    public override void Start()
    {

        base.Start();
        player = GameObject.FindGameObjectWithTag("Player");
        playerAspects = player.GetComponent<PlayerAspects>();
    }

    public override void Interact()
    {
        base.Interact();
        HealthBar health = null;

        if ((health = FindObjectOfType<HealthBar>()) != null)
        {
            if (playerAspects.playerHealth < playerAspects.initialHealth)
            {
                onInteract.AddListener(delegate { health.GetHealth(IncreaseHealth(increaseHealth)); Destroy(gameObject, 0.2f); });
            }
        }
    }   
    private float IncreaseHealth(float increaseHealth)
    {
        float health = 0;
        if(playerAspects.playerHealth + increaseHealth <= playerAspects.initialHealth)
        {
            health = increaseHealth;
        }
        else
        {
            health = playerAspects.initialHealth - playerAspects.playerHealth;
        }
        return health;
    }
}
