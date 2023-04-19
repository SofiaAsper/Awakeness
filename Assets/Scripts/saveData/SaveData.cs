using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public bool fileExist = false;
    public string platform;
    public string path;

    public bool isPC;
    public PlayerAspects playerAspects;
    public SkyBoxController skyBoxController;
    public LevelUp levelUp;
    public Coins coinsScript;
    PlayerController playerController;
    public CameraMovement cameraMovement;
    [SerializeField] public GameObject skyBoxStats;
    // here you declare new variable so you can use it on fetchData 

    // player stats
    public float health;
    public float experience;
    public float levelXP;
    public Vector3 playerPosition;
    public float points;
    public int coins;
    public int level;

    // skybox variables
    public float daynightcycle;
    public bool isdaytime;
    public Color daycolor;
    public Color nightcolor;
    public float stamina;
    public Vector2 sensitivity;
    public Vector2 shootingControllerSensitivity;


    private void Awake()
    {
        levelUp = GetComponent<LevelUp>();
        platform = SystemInfo.operatingSystem;
        playerAspects = gameObject.GetComponent<PlayerAspects>();
        playerController = gameObject.GetComponent<PlayerController>();
        coinsScript = gameObject.GetComponent<Coins>();
        // skyBoxController = skyBoxStats.GetComponent<SkyBoxController>(); // TODO : uncomment this later


        if (platform.Contains("Windows"))
        {
            isPC = true;
            path = Application.dataPath;
        }
        else if (platform.Contains("iOS") || platform.Contains("Android"))
        {
            isPC = false;
            path = Application.persistentDataPath;
        }
        if (File.Exists(path + "/PlayerData.json"))
        {
            fileExist = true;
            LoadFromJson();
        }
        InvokeRepeating("SaveIntoJson", 60, 300);
    }

    // each new param needs to be added from fetchData function!
    private void fetchData()
    {
        //player stats
        health = playerAspects.initialHealth;
        experience = playerAspects.playerXP;
        levelXP = levelUp.levelXP;
        playerPosition = transform.position;
        level = levelUp.level;
        stamina = playerController.sprintTime;
        points = playerAspects.playerPoints;
        coins = coinsScript.sumCoins;
        sensitivity = cameraMovement.savedRegSensitivity;
        shootingControllerSensitivity = cameraMovement.savedShootSensitivity;
        //skybox 
        daynightcycle = skyBoxController.dayNightCycle;
        isdaytime = skyBoxController.IsDayTime;
        daycolor = skyBoxController.dayColor;
        nightcolor = skyBoxController.nightColor;
    }
// TODO : uncomment this function later

    public void SaveIntoJson()
    {
        // fetchData();
        // string json = JsonUtility.ToJson(this);
        // if (isPC)
        // {
        //     File.WriteAllText(path + "/PlayerData.json", json);
        // }
        // else
        // {
        //     System.IO.File.WriteAllText(path + "/PlayerData.json", json);
        // }
        // Debug.Log(json + "im Saved");
    }

    public void LoadFromJson()
    {
        // string json;
        // if (isPC)
        // {
        //     json = File.ReadAllText(path + "/PlayerData.json");
        // }
        // else
        // {
        //     json = System.IO.File.ReadAllText(path + "/PlayerData.json");
        // }
        // PlayerData data = JsonUtility.FromJson<PlayerData>(json);
        // platform = data.platform;
        // path = data.path;
        // isPC = data.isPC;
        // sensitivity = data.sensitivity;
        // shootingControllerSensitivity = data.shootingControllerSensitivity;
        // //player stats
        // health = data.health;
        // experience = data.experience;
        // levelXP = data.levelXP;
        // playerPosition = data.playerPosition;
        // level = data.level;
        // stamina = data.stamina;
        // points = data.points;
        // coins = data.coins;
        // //skybox 
        // daynightcycle = data.daynightcycle;
        // isdaytime = data.isdaytime;
        // daycolor = data.daycolor;
        // nightcolor = data.nightcolor;
    }
}