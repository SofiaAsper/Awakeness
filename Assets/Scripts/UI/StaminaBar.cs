using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public Slider staminaBar;

    public PlayerController playerController;
    float smoothing = 25;
    float currentStamina;

    private void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        staminaBar = GetComponent<Slider>();
        // staminaBar.maxValue = PlayerController.sprintTime;
        // currentStamina = PlayerController.stamina;
    }

    void Update()
    {
        if (staminaBar.value != currentStamina)
        {
            if (staminaBar.value <= currentStamina)
                staminaBar.value = Mathf.Lerp(currentStamina, staminaBar.value, smoothing * Time.deltaTime);
            else staminaBar.value = Mathf.Lerp(staminaBar.value, currentStamina, smoothing * Time.deltaTime);
        }

    }


    public void SetStamina(float stamina, float maxValue)
    {
        staminaBar.maxValue = maxValue;
        currentStamina = stamina;
    }
}
