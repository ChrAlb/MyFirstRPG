using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsManager : MonoBehaviour
{

    public enum ItemType { Item, Weapon, Armor }
    public ItemType itemType;

    public string itemName, itemDescription;
    public int valueInCoins;
    public Sprite itemsImage;

    public int amountOfAffect;
   
    public AffectType affectType;
    public enum AffectType { HP, Mana }

    public int weaponDexterity;
    public int armorDefense;

    public bool isStackable;
    public int amount;


    public void UseItem()
    {
        if (itemType == ItemType.Item)
        {
            if(affectType == AffectType.HP)
            {
                PlayerStats.instance.AddHP(amountOfAffect);
            }
            else if (affectType == AffectType.Mana)
            {
                PlayerStats.instance.AddMana(amountOfAffect);
            }
        } 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //print("This item is: " + itemName);
            Inventory.instance.AddItems(this);
            SelfDestroy();
        }
    }

    public void SelfDestroy()
    {
        gameObject.SetActive(false);
    }
}
