using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;
    public PlayerAspects playerAspects;
    float smoothing = 30;
    float currentHealth;


    private void Start()
    {
        playerAspects = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAspects>();
        healthBar = GetComponent<Slider>();
        healthBar.maxValue = playerAspects.initialHealth;
        healthBar.value = playerAspects.initialHealth;
        currentHealth = playerAspects.initialHealth;
    }

    public static void DumpToConsole(object obj)
    {
        var output = JsonUtility.ToJson(obj, true);
        Debug.Log(output);
    }
    void Update()
    {
        if (healthBar.value >= currentHealth)
            healthBar.value = Mathf.Lerp(healthBar.value, currentHealth, smoothing * Time.deltaTime);
        if (healthBar.value <= currentHealth)
            healthBar.value = Mathf.Lerp(currentHealth, healthBar.value, smoothing * Time.deltaTime);

    }


    public void SetHealth(float hp)
    {
        if (hp >= 0)
        {
            currentHealth = hp;
        }
        else
        {
            currentHealth = 0;
        }


    }

    public void GetHealth(float health)
    {
        currentHealth += health;
        playerAspects.AddPlayerHealth(health);
        healthBar.value = currentHealth;
    }

}