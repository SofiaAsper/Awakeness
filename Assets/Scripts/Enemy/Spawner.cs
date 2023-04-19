using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemyRegular;
    public GameObject enemyRunner;
    public GameObject enemyFemale1;
    public GameObject enemyFemale2;
    public GameObject enemyMale1;
    public GameObject enemyMale2;
    public GameObject enemyBossPrinces;
    public GameObject enemyBossBrute;
    public GameObject enemyBossPregSit;
    public GameObject enemyBossPreg;
    public GameObject enemyBossSlobber;
    public Vector3 spawnOffset;
    public Vector3 spawnValue;
    public float spawnTime;
    // bool stop = false;

    int randEnemy;
    // [SerializeField] int maxEnemy = 40;
    // [SerializeField] float spawnTimer = 1f;

    GameObject[] poolRegular;
    GameObject[] poolRunner;
    GameObject[] poolBossPrinces;
    GameObject[] poolBossBrute;
    GameObject[] poolBossPregSit;
    GameObject[] poolBossPreg;
    GameObject[] poolBossSlobber;
    GameObject[] poolFemale1;
    GameObject[] poolFemale2;
    GameObject[] poolMale1;
    GameObject[] poolMale2;



    public int regular_zombies = 10;
    public int runner_zombies = 10;
    public int boss_zombies = 1;
    public int female_one = 10;
    public int female_two = 10;
    public int male_one = 10;
    public int male_two = 10;


    void Start()
    {
        PopulatePool();
        StartCoroutine(SpawnEnemy());
    }
    void PopulatePool()
    {
        poolRegular = new GameObject[regular_zombies];
        poolRunner = new GameObject[runner_zombies];
        poolFemale1 = new GameObject[female_one];
        poolFemale2 = new GameObject[female_two];
        poolMale1 = new GameObject[male_one];
        poolMale2 = new GameObject[male_two];
        poolBossPrinces = new GameObject[boss_zombies];
        poolBossBrute = new GameObject[boss_zombies];
        poolBossPregSit = new GameObject[boss_zombies];
        poolBossPreg = new GameObject[boss_zombies];
        poolBossSlobber = new GameObject[boss_zombies];

        for (int i = 0; i < poolRegular.Length; i++)
        {
            poolRegular[i] = Instantiate(enemyRegular, transform);
            poolRegular[i].SetActive(false);
        }
        for (int i = 0; i < poolRunner.Length; i++)
        {
            poolRunner[i] = Instantiate(enemyRunner, transform);
            poolRunner[i].SetActive(false);
        }
        for (int i = 0; i < poolBossPrinces.Length; i++)
        {
            poolBossPrinces[i] = Instantiate(enemyBossPrinces, transform);
            poolBossPrinces[i].SetActive(false);
        }
        for (int i = 0; i < poolBossBrute.Length; i++)
        {
            poolBossBrute[i] = Instantiate(enemyBossBrute, transform);
            poolBossBrute[i].SetActive(false);
        }
        for (int i = 0; i < poolBossPregSit.Length; i++)
        {
            poolBossPregSit[i] = Instantiate(enemyBossPregSit, transform);
            poolBossPregSit[i].SetActive(false);
        }
        for (int i = 0; i < poolBossPreg.Length; i++)
        {
            poolBossPreg[i] = Instantiate(enemyBossPreg, transform);
            poolBossPreg[i].SetActive(false);
        }
        for (int i = 0; i < poolBossSlobber.Length; i++)
        {
            poolBossSlobber[i] = Instantiate(enemyBossSlobber, transform);
            poolBossSlobber[i].SetActive(false);
        }
        for (int i = 0; i < poolFemale1.Length; i++)
        {
            poolFemale1[i] = Instantiate(enemyFemale1, transform);
            poolFemale1[i].SetActive(false);
        }
        for (int i = 0; i < poolFemale2.Length; i++)
        {
            poolFemale2[i] = Instantiate(enemyFemale2, transform);
            poolFemale2[i].SetActive(false);
        }
        for (int i = 0; i < poolMale1.Length; i++)
        {
            poolMale1[i] = Instantiate(enemyMale1, transform);
            poolMale1[i].SetActive(false);
        }
        for (int i = 0; i < poolMale2.Length; i++)
        {
            poolMale2[i] = Instantiate(enemyMale2, transform);
            poolMale2[i].SetActive(false);
        }


    }

    void EnableObjectInPool()
    {
        for (int i = 0; i < poolRegular.Length; i++)
        {
            if (poolRegular[i].activeInHierarchy == false)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnOffset.x, spawnOffset.x), 0f, Random.Range(-spawnOffset.z, spawnOffset.z));
                poolRegular[i].transform.position = spawnPosition + transform.TransformPoint(0, 0, 0);
                poolRegular[i].SetActive(true);
                return;
            }
        }
        for (int i = 0; i < poolRunner.Length; i++)
        {
            if (poolRunner[i].activeInHierarchy == false)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnOffset.x, spawnOffset.x), 1f, Random.Range(-spawnOffset.z, spawnOffset.z));
                poolRunner[i].transform.position = spawnPosition + transform.TransformPoint(0, 0, 0);
                poolRunner[i].SetActive(true);
                return;
            }
        }
        for (int i = 0; i < poolBossPrinces.Length; i++)
        {
            if (poolBossPrinces[i].activeInHierarchy == false)
            {
                poolBossPrinces[i].SetActive(true);
                return;
            }
        }
        for (int i = 0; i < poolBossBrute.Length; i++)
        {
            if (poolBossBrute[i].activeInHierarchy == false)
            {
                poolBossBrute[i].SetActive(true);
                return;
            }
        }
        for (int i = 0; i < poolBossPregSit.Length; i++)
        {
            if (poolBossPregSit[i].activeInHierarchy == false)
            {
                poolBossPregSit[i].SetActive(true);
                return;
            }
        }
        for (int i = 0; i < poolBossPreg.Length; i++)
        {
            if (poolBossPreg[i].activeInHierarchy == false)
            {
                poolBossPreg[i].SetActive(true);
                return;
            }
        }
        for (int i = 0; i < poolBossSlobber.Length; i++)
        {
            if (poolBossSlobber[i].activeInHierarchy == false)
            {
                poolBossSlobber[i].SetActive(true);
                return;
            }
        }
        for (int i = 0; i < poolFemale1.Length; i++)
        {
            if (poolFemale1[i].activeInHierarchy == false)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnOffset.x, spawnOffset.x), 1f, Random.Range(-spawnOffset.z, spawnOffset.z));
                poolFemale1[i].transform.position = spawnPosition + transform.TransformPoint(0, 0, 0);
                poolFemale1[i].SetActive(true);
                return;
            }
        }
        for (int i = 0; i < poolFemale2.Length; i++)
        {
            if (poolFemale2[i].activeInHierarchy == false)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnOffset.x, spawnOffset.x), 1f, Random.Range(-spawnOffset.z, spawnOffset.z));
                poolFemale2[i].transform.position = spawnPosition + transform.TransformPoint(0, 0, 0);
                poolFemale2[i].SetActive(true);
                return;
            }
        }
        for (int i = 0; i < poolMale1.Length; i++)
        {
            if (poolMale1[i].activeInHierarchy == false)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnOffset.x, spawnOffset.x), 1f, Random.Range(-spawnOffset.z, spawnOffset.z));
                poolMale1[i].transform.position = spawnPosition + transform.TransformPoint(0, 0, 0);
                poolMale1[i].SetActive(true);
                return;
            }
        }
        for (int i = 0; i < poolMale2.Length; i++)
        {
            if (poolMale2[i].activeInHierarchy == false)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnOffset.x, spawnOffset.x), 1f, Random.Range(-spawnOffset.z, spawnOffset.z));
                poolMale2[i].transform.position = spawnPosition + transform.TransformPoint(0, 0, 0);
                poolMale2[i].SetActive(true);
                return;
            }
        }

    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            EnableObjectInPool();
        }
    }

    // void Update()
    // {
    //     // Debug.Log(enemySpawner.childCount);
    //     if (enemySpawner.childCount >= maxEnemy && !stop)
    //     {
    //         StartCoroutine(waitSpawner());
    //         stop = true;
    //     }
    //     spawnWait = Random.Range(spawnLeastWait, spawnMostWait);
    // }

    // IEnumerator waitSpawner()
    // {
    //     yield return new WaitForSeconds(startWait);

    //     while (enemySpawner.childCount < maxEnemy)
    //     {
    //         randEnemy = Random.Range(0, 2);

    //         for (int i = 0; i < 5; i++)
    //         {
    //             Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), 1f, Random.Range(-spawnValues.z, spawnValues.z));
    //             Instantiate(enemieReg, spawnPosition + transform.TransformPoint(0, 0, 0), gameObject.transform.rotation, gameObject.transform);
    //         }
    //         Vector3 spawnPosition1 = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), 1f, Random.Range(-spawnValues.z, spawnValues.z));
    //         Instantiate(enemieRun, spawnPosition1 + transform.TransformPoint(0, 0, 0), gameObject.transform.rotation, gameObject.transform);

    //         yield return new WaitForSeconds(spawnWait);
    //     }
    //     stop = false;
    // }
    // void OnDrawGizmosSelected()
    // {
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawWireSphere(transform.position, spawnValues.x);
    // }

}