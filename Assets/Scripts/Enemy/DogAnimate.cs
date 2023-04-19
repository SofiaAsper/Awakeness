using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DogAnimate : MonoBehaviour
{
    [HideInInspector]
    public bool getTheDog = false;
    private float distanceToPlayer;
    // float distanceToEnemy;
    private GameObject player;
    private NavMeshAgent navMesh;
    private Animator anim;

    private Animation animationD;
    private PlayerInput playerStatus;
    float time;


    private float move = 0f;

    public Transform enemyZombie;
    bool stopFollowing = false;

    NavMeshPath navMeshPath;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        navMesh = GetComponent<NavMeshAgent>();
        navMeshPath = new NavMeshPath();
        anim = GetComponent<Animator>();
        animationD = GetComponent<Animation>();
        playerStatus = player.GetComponent<PlayerInput>();
    }
    bool ZombieDead = false;
    bool StopAttacking = false;
    void FixedUpdate()
    {
        distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        // distanceToEnemy = Vector3.Distance(enemy.transform.position, transform.position);
        if (!getTheDog && stopFollowing==false) FirstInteraction();
        if (getTheDog && stopFollowing==false) Movinganimations();

        if (enemyZombie != null && getTheDog && StopAttacking == false)
        {
            StartChasingZombie();
            ZombieDead = false;
            if (distanceToPlayer>=15)
            {
                StopAttackingToAll();
            }
        }
        StopAttackingZombie();
    }
    void StopAttackingZombie()
    {
        if (enemyZombie != null)
            if (enemyZombie.GetComponent<TargetDamageable>().isDead() && ZombieDead == false)
            {
                enemyZombie = null;
                stopFollowing = false;
                time = Time.time;
                anim.SetFloat("Move", 0);
                ZombieDead = true;
                anim.SetBool("Attack", false);
            }
    }
    void StopAttackingToAll()
    {
        if (enemyZombie != null)
            enemyZombie = null;
        stopFollowing = false;
        ZombieDead = true;
        anim.SetBool("Attack", false);
        anim.SetBool("Lay", false);

    }
    public void AttackZombie(Transform zombie)
    {
        if (enemyZombie == null)
        enemyZombie = zombie;
    }
    public void StopMovingDog()
    {
        if (!navMesh.isStopped)
        {
            navMesh.isStopped = true;
        }
    }

    public void StartMovingDog()
    {
        if (navMesh.isStopped)
        {
            navMesh.isStopped = false;
        }
    }
    void StartChasingZombie()
    {
        stopFollowing = true;

        navMesh.SetDestination(enemyZombie.transform.position);
        transform.LookAt(enemyZombie.position);
        if (Vector3.Distance(enemyZombie.transform.position, transform.position)<2)
        {
            anim.SetBool("Attack", true);
            anim.SetBool("Lay", false);
            navMesh.stoppingDistance = 4;
            anim.SetFloat("Move", 0.4f);
            anim.SetBool("Sit", false);
            anim.SetBool("Bark", false);
        }
        else
        {
            navMesh.stoppingDistance = 1.2f;

            anim.SetFloat("Move", 4);
            anim.SetBool("Attack", false);
        }
    }
    public void FirstInteraction()
    {
        // player far away -> idle sitting \ laying
        if (distanceToPlayer > 10)
        {

        }
        // player near -> look at the player and stand idle
        if (distanceToPlayer <= 10)
        {
            transform.LookAt(player.transform.position);
        }
        // the dog is out -> follow the player
        if (distanceToPlayer <= 4)
        {
            getTheDog = true;
            navMesh.SetDestination(player.transform.position);
            player.GetComponent<PlayerAspects>().SetDogAlly(this.transform);
            Movinganimations();
        }
    }

    public void Movinganimations()
    {
        navMesh.SetDestination(player.transform.position);
        if (distanceToPlayer >= 10)
        {
            move = 5f;
            anim.SetFloat("Move", move);
            navMesh.speed = 8f;
        }
        else if (distanceToPlayer >= 8)
        {
            move = 4f;
            anim.SetFloat("Move", move);
            navMesh.speed = 6f;
        }
        else if (distanceToPlayer >= 6)
        {
            move = 3f;
            anim.SetFloat("Move", move);
            navMesh.speed = 2f;
        }
        else if (distanceToPlayer >= 4)
        {
            StopIdleAnimation();
            move = 2f;
            anim.SetFloat("Move", move);
            navMesh.speed = 1f;
            time = Time.time;
        }
        else
        {

            navMesh.speed = 0f;
            move = 0f;
            anim.SetFloat("Move", move);
            if (Time.time >= (time + 5f) && Time.time < (time + 7f))
            {
                int randomBark = Random.Range(0, 100);
                if (randomBark > 98)
                {
                    anim.SetBool("Bark", true);
                }
            }
            if (Time.time >= (time + 7f) && Time.time <= (time + 10f))
            {
                if (anim.GetBool("Bark")==true)
                {
                    anim.SetBool("Bark", false);
                }
            }
            if (Time.time >= (time + 10f) && Time.time <= (time + 15f))
            {
                anim.SetBool("Sit", true);
                anim.SetBool("Bark", false);
            }

            if (Time.time >= time + 15f)
            {
                anim.SetBool("Lay", true);
                anim.SetBool("Sit", false);
                anim.SetBool("Bark", false);
            }
        }


    }

    public void EnemyEngaging()
    {
        // if enemy close - set destination to the enemy - attack
        // if enemy gone keep following player
    }

    public void StopIdleAnimation()
    {
        anim.SetBool("Lay", false);
        anim.SetBool("Sit", false);
        anim.SetBool("Bark", false);
    }
}