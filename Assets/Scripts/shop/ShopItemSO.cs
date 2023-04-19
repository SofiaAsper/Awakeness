using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ShopItemSO", menuName = "FPS/ShopItemSO", order = 0)]
public class ShopItemSO : ScriptableObject
{

    public enum PayType { points, coins, money };
    public enum ItemType { gun, shield, boosts };

    public string itemName;
    public string itemDescription;
    public int itemPrice;
    public Sprite itemIcon;
    public GunObject itemPrefab;
    public PayType payType;
    public ItemType itemType;

}
