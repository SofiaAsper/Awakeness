using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLevelUp : MonoBehaviour
{
    int enemiesDead = 0;
    public int initialEnemiesToKill = 30;
    private int enemiesToKill;

    private void Start()
    {
        enemiesToKill = initialEnemiesToKill;
    }

    public void EnemyDied()
    {
        enemiesDead++;
        if (enemiesDead >= enemiesToKill)
        {
            BroadcastMessage("ZombieGainHealth");
            enemiesToKill += 5;
            enemiesDead = 0;
        }
    }


}
