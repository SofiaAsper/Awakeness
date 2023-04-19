using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class ShopManager : MonoBehaviour
{
    public ShopItemSO[] shopItemsSO;
    public ShopTemplate[] shopTemplates;
    public GameObject[] shopPanelsGO;
    public Button[] purchaseBtn;

    [SerializeField] PlayerAspects playerAspects;
    [SerializeField] GunController gunController;
    [SerializeField] Coins coins;
    public Canvas shopCanvas;

    public static bool isShopOpen = false;
    public PauseGame pauseGame;

    [SerializeField] TMP_Text CoinsTxt;
    [SerializeField] TMP_Text PointsTxt;

//*********************************************************************************************************************
    void Start() 
    {
        LoadPanels();
        shopCanvas.enabled = false;
    }

    void LateUpdate() 
    {
        if (Input.GetKeyDown(KeyCode.F) && pauseGame.pausedGame == true)
        {
            OpenShopMenu();
        }
        else if ((Input.GetKeyDown(KeyCode.F)||(Input.GetKeyDown(KeyCode.Escape))) && pauseGame.pausedGame == false)
        {
            CloseShopMenu();
        }
    }

//**********************************************************************************************************************

    public void LoadPanels()
    {
        for (int i = 0; i < shopItemsSO.Length; i++)
        {
            shopTemplates[i].itemName.text = shopItemsSO[i].itemName;
            shopTemplates[i].itemDescription.text = shopItemsSO[i].itemDescription;
            shopTemplates[i].itemPrice.text = shopItemsSO[i].itemPrice.ToString() +" "+ shopItemsSO[i].payType;
            shopTemplates[i].itemIcon.sprite = shopItemsSO[i].itemIcon;
        }
    }

    public void OpenShopMenu()
    {
        CoinsPointsUpdate();
        Time.timeScale = 0;
        Cursor.visible = true;
        pauseGame.pausedGame = true;
        CheckPurchaseable();
        shopCanvas.enabled = true;
        for (int i = 0; i < shopItemsSO.Length; i++) {
            shopPanelsGO[i].SetActive(true);
        }
    }
    public void CloseShopMenu()
    {
        pauseGame.pausedGame = false;
        Time.timeScale = 1;
        Cursor.visible = false;
        shopCanvas.enabled = false;
    }

    public void CheckPurchaseable()
    {
        for (int i = 0; i < shopItemsSO.Length; i++)
        {
            if (shopItemsSO[i].itemPrice > playerAspects.playerPoints && shopItemsSO[i].payType.ToString() == "points")
            {
                purchaseBtn[i].interactable = false;
            }
            else if (shopItemsSO[i].itemPrice > coins.sumCoins && shopItemsSO[i].payType.ToString() == "coins")
            {
                purchaseBtn[i].interactable = false;
            }
            else
            {
                purchaseBtn[i].interactable = true;
            }
        }
    }

    public void PurchaseItem(int btnNum)
    {

        if(shopItemsSO[btnNum].payType.ToString() == "points")
        {
            if (playerAspects.playerPoints >= shopItemsSO[btnNum].itemPrice)
            {
                Debug.Log("Purchased with points" + shopItemsSO[btnNum].itemName);
                playerAspects.playerPoints -= shopItemsSO[btnNum].itemPrice;

                // add buying sequence here - add to inventory, remove from shop, etc.
                BuyingSequence(btnNum);
            }
        }
        else if(shopItemsSO[btnNum].payType.ToString() == "coins")
        {
            if (coins.sumCoins >= shopItemsSO[btnNum].itemPrice)
            {
                Debug.Log("Purchased with coins" + shopItemsSO[btnNum].itemName);
                coins.sumCoins -= shopItemsSO[btnNum].itemPrice;

                // add buying sequence here - add to inventory, remove from shop, etc.
                BuyingSequence(btnNum);
            }
        }
        else if(shopItemsSO[btnNum].payType.ToString() == "money")
        {
            // buying with paypal or credit card
        }
    }

    void BuyingSequence(int btnNum)
    {
        // if we are buying guns
        shopItemsSO[btnNum].itemPrefab.AddTempGunToPlayer();
        shopPanelsGO[btnNum].SetActive(false);
        // if we are buying shields

        // if we are buying boosts
        CoinsPointsUpdate();
        CheckPurchaseable(); 
    }

    void CoinsPointsUpdate()
    {
        CoinsTxt.text = "Coins: " + coins.sumCoins.ToString();
        PointsTxt.text = "Points: " + playerAspects.playerPoints.ToString();
    }
}
