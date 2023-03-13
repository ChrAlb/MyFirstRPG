using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    
    private List<ItemsManager> itemsList;
    
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        
        itemsList = new List<ItemsManager>();
        //Debug.Log("Hey a new Inventory has been created");
    }

   public List<ItemsManager> GetItemList()
    {
        return itemsList;
    }

    public void AddItems(ItemsManager item)
    {
        if(item.isStackable)
        {

            bool itemAlreadyInInventory = false;

            

            foreach(ItemsManager itemInInventory in itemsList)
            {
                if (itemInInventory.itemName == item.itemName)
                {
                    itemInInventory.amount += item.amount;
                    itemAlreadyInInventory = true;
                    
                }
                              
            }

            if (!itemAlreadyInInventory)
            {
                itemsList.Add(item);

            }

        }
        else
                {
                  itemsList.Add(item);
                }     
        
    }

    public void RemoveItem(ItemsManager item)

    {
        if (item.isStackable)
        {
            ItemsManager inventoryItem = null;

            foreach(ItemsManager itemInInventory in itemsList)
            {
                if(itemInInventory.itemName == item.itemName)
                {
                    itemInInventory.amount--;
                    inventoryItem = itemInInventory;
                }

            }

            if(inventoryItem != null && inventoryItem.amount <= 0)
            {
                itemsList.Remove(inventoryItem);
            }
        }
        else
        {
            itemsList.Remove(item);
        }

    }

}
