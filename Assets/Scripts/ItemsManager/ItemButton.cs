using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemButton : MonoBehaviour
{
    public ItemsManager itemOnButton;
    
    public void Press()

    {
       
        if(MenuManager.Instance.menu.activeInHierarchy)
        {
            // This is bad coding. Better to send the name and descrciption to the menu manager script.
            MenuManager.Instance.itemName.text = itemOnButton.itemName;
            MenuManager.Instance.itemDescription.text = itemOnButton.itemDescription;

            MenuManager.Instance.activeItem = itemOnButton;

        }

       

        if (ShopManager.instance.shopMenu.activeInHierarchy)
        {

            if (ShopManager.instance.buyPanel.activeInHierarchy)
            {

                ShopManager.instance.SelectedBuyItem(itemOnButton);

            }
            else if (ShopManager.instance.sellPanel.activeInHierarchy)
            {

                ShopManager.instance.SelctedSellItem(itemOnButton);

            }
        }

        if (BattleManager.instance.itemsToUseMenu.activeInHierarchy)
        {
            //Debug.Log("Im Battle Manager drin");
            BattleManager.instance.SelectedItemToUse(itemOnButton);

        }
        

    }
    
}
