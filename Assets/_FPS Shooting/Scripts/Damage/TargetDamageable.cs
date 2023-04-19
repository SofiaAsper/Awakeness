using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TargetDamageable : Damageable
{
    public float dissapearTime = 8f;
    Animator ani;
    LiveTextSpawner liveTextSpawner;
    PlayerAspects playerAspects;
    GameObject player;
    EnemyLevelUp enemyLevelUp;

    float hurt = 0f;
    float previousHealth;
    public float points = 120;

    Vector3 hitPosition;

    [SerializeField] ParticleSystem explosionBlood;
    [SerializeField] ParticleSystem bloodSmall;
    [SerializeField] ParticleSystem bloodSplat;

    // Start is called before the first frame update
    public override void Start()
    {
        hitPosition = transform.position; // for now
        base.Start();
        previousHealth = health;
        ani = GetComponent<Animator>();
        liveTextSpawner = FindObjectOfType<LiveTextSpawner>();
        enemyLevelUp = FindObjectOfType<EnemyLevelUp>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerAspects = player.GetComponent<PlayerAspects>();
        AddCallOnDamage(HurtTarget);
        AddCallOnDeath(TargetDied);
    }

    //public override void Update()
    //{

    //}

    void HurtTarget()
    {
        float dmgDone = (previousHealth - health);
        hurt = dmgDone / (maxHealth * 2);
        if (health>0)
        BloodEffect(dmgDone);
        if (isDead()) return;
        else if (gameObject.tag == "Boss")
        {
            BroadcastMessage("OnDamageTaken");
            if (dmgDone >= 0.75 * maxHealth && dmgDone < maxHealth)
            {
                ani.SetBool("hit", true);
            };

        }
        else
        {
            BroadcastMessage("OnDamageTaken");
            ani.SetBool("hit", true);
        }

    }
    //public bool zombieDead = false;
    void TargetDied()
    {
        //zombieDead= true;
        //BloodEffect(30);
        ani.SetBool("wonder", false);
        ani.SetBool("hit", false);
        playerAspects.GainXP(points);
        liveTextSpawner.SpawnFloatText(points.ToString());
        ani.SetBool("attack", false);
        ani.SetBool("dead", true);
        enemyLevelUp.EnemyDied();
        StartCoroutine(EnemyDissapear());
        IEnumerator EnemyDissapear()
        {
            hurt = 1f;
            yield return new WaitForSeconds(dissapearTime);
            gameObject.SetActive(false);
        }
        //GameObject particleSpawn = Instantiate(explosionBlood.gameObject, hitPosition, transform.rotation);
        //particleSpawn.GetComponent<ParticleSystem>().Play();
    }

    public void SelfHitFalse()
    {
        ani.SetBool("hit", false);
    }
    public void SetHitPos(Vector3 position)
    {
        hitPosition = position;
    }


    public void BloodEffect(float dmgDone)
    {
        //if (dmgDone > 1 && dmgDone <= 15)
        //{
            GameObject particleSpawn = Instantiate(bloodSmall.gameObject, hitPosition, transform.rotation);
            particleSpawn.GetComponent<ParticleSystem>().Play();
            //bloodSmall.transform.position = hitPosition;
            //bloodSmall.Play();
        //}
        //if (dmgDone > 15 && dmgDone <= 70)
        //{
        //    //bloodSplat.transform.position = hitPosition;
        //    //bloodSplat.transform.LookAt(player.transform.position);
        //    //bloodSplat.Play();
        //    GameObject particleSpawn = Instantiate(bloodSplat.gameObject, hitPosition, transform.rotation);
        //    particleSpawn.transform.LookAt(player.transform.position);
        //    particleSpawn.GetComponent<ParticleSystem>().Play();

        //}
        //if (dmgDone > 70)
        //{

        //    GameObject particleSpawn = Instantiate(explosionBlood.gameObject, hitPosition, transform.rotation);
        //    particleSpawn.GetComponent<ParticleSystem>().Play();
        //    //explosionBlood.transform.position = hitPosition;
        //    //explosionBlood.Play();

        //}
    }




}
