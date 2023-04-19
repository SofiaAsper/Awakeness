using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUp : MonoBehaviour
{
    //level up system
    public PlayerAspects playerAspects;
    // private EnemyAI enemyAI;
    [SerializeField] PlayerController playerController;
    public SaveData saveData;
    public TextMeshProUGUI levelText;
    public LiveTextSpawner liveTextSpawner;
    [SerializeField] Coins coins;

    [SerializeField] private Image backgroundLevelImg;


    public int level = 0;
    public float levelXP;
    public int experienceToNextLevel;
    public int experienceOfPrevLevel;
    [SerializeField] int maxLevel = 100;





    void Awake()
    {
        playerAspects = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAspects>();
    }

    void Start()
    {
        level = saveData.level;
        levelXP = saveData.levelXP;
        experienceToNextLevel = (level + 1) * (level + 1) * 100;
        levelText.text = level.ToString();
        // backgroundLevelImg.color = new Color(0.3607155f, 0.3730624f, 0.3962264f, 1f);
        //InvokeRepeating("MineUpdate", 0.5f, 0.5f);
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if (levelXP >= (float)experienceToNextLevel && level <= maxLevel)
    //    {
    //        RankUp();
    //        LevelProperties();
    //    }
    //}
    public void LevelXpIncreased()
    {
        if (levelXP >= (float)experienceToNextLevel && level <= maxLevel)
        {
            RankUp();
            LevelProperties();
        }
    }

    void RankUp()
    {
        levelXP -= experienceToNextLevel;
        level++;
        levelText.text = level.ToString();
        AddPointsToPlayer(level * 10);
        AddHealth(level);
        AddStamina(level);
        experienceToNextLevel = level * level * 100;
        liveTextSpawner.LevelUpText();
        // spawn bonus points and coins txt

    }

    public void SetXP(float xp)
    {
        levelXP += xp;
        LevelXpIncreased();
    }




    // add health to the player
    public void AddHealth(int level)
    {
        playerAspects.initialHealth +=  2;
    }
    //add stamina to the player
    public void AddStamina(int level)
    {
        playerController.sprintTime += 2;
    }

    public void AddCoinsToPlayer(int coins)
    {
        this.coins.AddCoins(coins);
    }
    public void AddPointsToPlayer(int points)
    {
        this.playerAspects.playerPoints += points;
    }


    void LevelProperties()
    {
        if (level % 10 == 0)
        {
            // backgroundLevelImg.color = new Color(0.3584906f, 0.1662972f, 0.01521893f, 1f);
            AddCoinsToPlayer(30 * level);
        }

        if (level == maxLevel - 2) //bronze color
        {
            backgroundLevelImg.color = new Color(0.3584906f, 0.1662972f, 0.01521893f, 1f);
        }
        if (level == maxLevel - 1) //silver color
        {
            backgroundLevelImg.color = new Color(1f, 1f, 1f, 1f);
        }
        if (level == maxLevel) //gold color
        {
            backgroundLevelImg.color = new Color(1f, 0.635695f, 0f, 1f);
        }
    }


}
