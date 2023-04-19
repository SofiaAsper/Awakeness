using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XpBar : MonoBehaviour
{
    public Slider XPBar;
    public PlayerAspects playerAspects;

    [SerializeField] private LevelUp levelUp;
    [SerializeField] private SaveData saveData;

    // float smoothing = 25;

    private void Start()
    {
        UpdateXP();
    }

    void Update()
    {
        UpdateXP();
    }



    void UpdateXP()
    {
        XPBar.maxValue = levelUp.experienceToNextLevel;
        XPBar.value = levelUp.levelXP;
    }
}
