using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(TargetDamageable))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioManager))]

public class EnemyAI : MonoBehaviour
{
    [SerializeField] float chaseRange = 20f;
    [SerializeField] float turnSpeed = 5f;
    public float damage = 40f;
    [SerializeField] bool enableSitting;

    GameObject player;
    NavMeshAgent navMeshAgent;
    Animator anim;
    Damageable damageable;
    PlayerAspects playerAspects;
    LevelUp levelUp;
    public AudioManager audioManager;
    float distanceToPlayer = Mathf.Infinity;
    bool isProvoked = false;
    public float wanderRadius = 50;
    public float wanderTimer = 5;
    public bool isBossBrute = false;
    public bool isBossPrincess = false;
    public bool isBossSloober = false;
    public bool isPrgBoss = false;
    
    private Transform target;
    private float timer = 10f;

    NavMeshPath navMeshPath;
    Vector3 unpausedSpeed;
    float speed;
    float stoppingDistance;
    bool delay = true;
    // bool farFromPlayer = true;

    float updateTime = 0.5f;

    void OnEnable()
    {
        
        navMeshAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
        player = GameObject.FindGameObjectWithTag("Player");
        levelUp = player.GetComponent<LevelUp>();
        playerAspects = player.GetComponent<PlayerAspects>();
        navMeshPath = new NavMeshPath();
        damageable.health = damageable.maxHealth;
        anim.SetBool("dead", false);
        navMeshAgent.enabled = true;
        anim.SetBool("wonder", true);
    }
    void Start()
    {
        timer = wanderTimer;
        if (enableSitting) anim.SetBool("sit", true);
        speed = Random.Range(0.8f, 1.2f);
        stoppingDistance = navMeshAgent.stoppingDistance;
        InvokeRepeating("MineUpdate", updateTime, updateTime);
    }

    void MineUpdate()
    {
        // stop any animation when the enemy is dead
        if (damageable.isDead())
        {
            audioManager.StopAllAudio();
            isProvoked = false;
            anim.SetBool("provoked", false);
            return;
        }
        distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        //manage enemy behavior
        if (isProvoked) EngagePlayer();
        else if (distanceToPlayer <= chaseRange)
            isProvoked = true;
        else WonderZombie();        

    }
    bool dogStop = false;
    IEnumerator DogAttackTiming()
    {
        yield return new WaitForSeconds(10f);
        dogStop = false;
    }
    void EngagePlayer()
    {
        if (distanceToPlayer > 1 * chaseRange) audioManager.StopAllAudio();


        if (distanceToPlayer >= stoppingDistance && !damageable.isDead())
        {
            ChasePlayer();
            anim.SetBool("provoked", true);
            if (dogStop == false && distanceToPlayer < 5)
            {
                if ((playerAspects.CurrentAlly != null))
                {
                    playerAspects.ZombieComing(this.transform);
                    dogStop = true;
                    StartCoroutine(DogAttackTiming());
                }
            }

            anim.SetFloat("speed", speed);
            audioManager.Pause("Wondering");
            audioManager.Play("Running");
        }
        if (distanceToPlayer < stoppingDistance +0.5f && !damageable.isDead())
        {
            anim.SetBool("provoked", false);
            AttackPlayerAnimation();
            FaceTarget();
        }
        //if (!damageable.isDead())
        //{
        //    RaycastHit hit;
        //    // Does the ray intersect any objects excluding the player layer
        //    if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 100f))
        //    {
        //        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
        //        Debug.Log("ray hit");
        //        if (hit.transform.tag == "Player")
        //        {
        //            anim.SetBool("provoked", false);
        //            AttackPlayerAnimation();
        //            FaceTarget();
        //        }
        //    }
        //}

    }
    // provoking the enemy when we shoot it.. needs to be called (can be by broadcast massage from target damagable so we know that the enemy took damage)
    public void OnDamageTaken()
    {
        isProvoked = true;
    }
    void ChasePlayer()
    {
        navMeshAgent.enabled = true;
        if (!navMeshAgent.isOnNavMesh) return;
        CheckPath();
        anim.SetBool("attack", false);

        if (isBossPrincess) PrincessBossHandler();

    }

    void AttackPlayerAnimation()
    {
        anim.SetBool("attack", true);
    }

    void FaceTarget()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }

    public void AttackHitEvent()
    {
        if (player == null) return;
        if (distanceToPlayer <= stoppingDistance + 0.3 && !damageable.isDead())
        {
            playerAspects.DamagePlayer(damage,transform);
        }
    }

    private void CheckPath()
    {

        if (navMeshAgent.CalculatePath(player.transform.position, navMeshPath) && navMeshPath.status == NavMeshPathStatus.PathComplete)
        {
            anim.SetBool("wonder", false);
            if (isPrgBoss && enableSitting)
            {
                StartCoroutine(PrgZombieSitTime());
            }
            else
            {
                navMeshAgent.SetPath(navMeshPath);
            }
        }
        else WonderZombie();
    }
    IEnumerator PrgZombieSitTime()
    {
        yield return new WaitForSeconds(2f);
        navMeshAgent.SetPath(navMeshPath);
    }
    private void WonderZombie()
    {
        if (damageable.isDead()) return;
        if (!navMeshAgent.isOnNavMesh) 
        {
            gameObject.SetActive(false);
            Debug.Log("enemy is not on navmesh" + gameObject.name);
        }
        timer += updateTime;
        anim.SetBool("wonder", true);
        if (timer >= wanderTimer && navMeshAgent.isOnNavMesh)
        {
            if (!isBossBrute && !isPrgBoss && !isBossSloober)
            {
                Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                navMeshAgent.SetDestination(newPos);
            }
            BruteBossHandler();
            timer = 0;
        }
        //check if the distance to the player is great anougth to stop the audio
        if (distanceToPlayer > 2 * chaseRange) 
            audioManager.StopAllAudio();
        else
        {
            audioManager.Pause("Running");
            audioManager.Play("Wondering");
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.onUnitSphere * dist;
        randDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);
        return navHit.position;
    }

    public void StopMovingZombie()
    {
        if (!navMeshAgent.isStopped)
        {
            if (!isBossSloober)
            {
                unpausedSpeed = navMeshAgent.velocity;
                navMeshAgent.velocity = Vector3.zero;
            }
            navMeshAgent.isStopped = true;
        }
    }

    public void StartMovingZombie()
    {
        if (!navMeshAgent.isOnNavMesh)
        {
            Debug.Log("Not on navmesh : " + gameObject.name);
        }
        if (navMeshAgent.isStopped)
        {
            navMeshAgent.isStopped = false;
            if (!isBossSloober)
            {
                navMeshAgent.velocity = unpausedSpeed;
            }
        }
    }

    public void UpgradeEnemy(int level)
    {
        damage += (2 * levelUp.level);
        chaseRange += 5;
    }


    IEnumerator DelayMethod(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        delay = true;
    }

    void PrincessBossHandler()
    {
        if (delay && distanceToPlayer < 10f && distanceToPlayer > 5f)
        {
            anim.SetBool("jump", true);
            delay = false;
        }
        else anim.SetBool("jump", false);
        StartCoroutine(DelayMethod(30f));
    }
    void BruteBossHandler()
    {
        if (isBossBrute==false)
        {
            return;
        }
        if (anim.GetBool("walk") == true)
        {
            navMeshAgent.isStopped = true;
            anim.SetBool("walk", false);
        }
        else
        {
            navMeshAgent.isStopped = false;
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            navMeshAgent.SetDestination(newPos);
            anim.SetBool("walk", true);
        }
        //shout sound
    }
    void PregBossHandler()
    {
        //zombie aspects will be added later
    }


}


