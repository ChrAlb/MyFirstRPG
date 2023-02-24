using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemButton : MonoBehaviour
{
    public ItemsManager itemOnButton;
    
    public void Press()

    {
        MenuManager.Instance.itemName.text = itemOnButton.itemName;
        MenuManager.Instance.itemDescription.text = itemOnButton.itemDescription;

        MenuManager.Instance.activeItem = itemOnButton;

    }
    
}
