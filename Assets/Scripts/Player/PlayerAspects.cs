using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAspects : MonoBehaviour
{
    private InterpolatedTransform InterpolatedTransform;
    [HideInInspector] public SaveData saveData;
    public float playerHealth;
    public float initialHealth = 1000f; // TODO : change this to 100f before releasing - just for animations testing 
    public HealthBar healthBar;
    public LevelUp levelUp;
    DeathHandler deathHandler;
    private IEnumerator coroutine;
    public float playerXP = 0;
    public float playerPoints = 0;
    public GameObject player;
    // private float damage_fx_timer = 0f;
    public DamageUI damage_fx;

    public Transform CurrentAlly = null;


    // private void Awake()
    // {
    //     saveData = GetComponent<SaveData>();
    // }

    void Start()
    {
        // TODO : uncomment this later - SaveDate is no relevant right now

        // if (saveData.fileExist)
        // {
        //     InterpolatedTransform = GetComponent<InterpolatedTransform>();
        //     InterpolatedTransform.ResetPositionTo(saveData.playerPosition);
        //     playerXP = saveData.experience;
        //     initialHealth = saveData.health;
        //     playerPoints = saveData.points;
        // }
        playerHealth = initialHealth;
        deathHandler = GetComponent<DeathHandler>();
        coroutine = RegainHealthWithTime();
    }
    public void SetDogAlly(Transform ally)
    {
        CurrentAlly = ally;
    }
    public void ZombieComing(Transform zombie)
    {
        if (CurrentAlly != null &&
            CurrentAlly.GetComponent<DogAnimate>().enemyZombie == null)
        {
            CurrentAlly.GetComponent<DogAnimate>().AttackZombie(zombie);
        }
    }
    public void DamagePlayer(float damagePoints,Transform zombie)
    {

        DoDamageFX();
        StopCoroutine(coroutine);
        playerHealth -= damagePoints;
        healthBar.SetHealth(playerHealth);
        if (playerHealth <= 0)
        {
            deathHandler.HandleDeath();
            return;
        }
        Invoke("DelayRegainHealth", 5f);

        if (CurrentAlly!=null)
        {
            CurrentAlly.GetComponent<DogAnimate>().AttackZombie(zombie);
        }
        
    }
    
    public void DoDamageFX()
    {
        if (damage_fx != null)
            StartCoroutine(DamageFXRun());
    }


    private IEnumerator DamageFXRun()
    {
        // damage_fx_timer = -3f;
        damage_fx.Show();
        yield return new WaitForSeconds(1f);
        damage_fx.Hide();
    }

    void DelayRegainHealth()
    {
        StartCoroutine(coroutine);
    }

    IEnumerator RegainHealthWithTime()
    {
        while (playerHealth < 100)
        {
            playerHealth += 0.5f;
            healthBar.SetHealth(playerHealth);
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void GainXP(float points)
    {
        playerXP += 0.5f * points;
        playerPoints += points;
        levelUp.SetXP(0.5f * points);
    }

    public void AddPlayerHealth(float health)
    {
        playerHealth += health;
    }

}
