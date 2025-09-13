using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{

    [SerializeField] SaveData saveData;
    [SerializeField] LevelUp levelUp;
    [SerializeField] PlayerAspects playerAspects;

    public int sumCoins;

    void Start()
    {
       if (saveData.fileExist) sumCoins = saveData.coins;
       else sumCoins = 0;
    }

    public void AddCoins(int coins)
    {
        sumCoins += coins;
    }
}
