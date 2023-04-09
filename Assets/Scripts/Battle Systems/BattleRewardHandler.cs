using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleRewardHandler : MonoBehaviour
{
    public static BattleRewardHandler instance;

    [SerializeField] TextMeshProUGUI XPText, itemsText;
    [SerializeField] GameObject rewardScreen;

    [SerializeField] ItemsManager[] rewardItems;
    [SerializeField] int xpReward;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Y))
        {
            OpenRewardScreen(15000, rewardItems);

        }
    }
    public void OpenRewardScreen(int xpEarned, ItemsManager[] itemsEarned)
    {
        xpReward = xpEarned;
        rewardItems = itemsEarned;

        XPText.text = xpEarned + " XP";
        itemsText.text = "";

        foreach(ItemsManager rewardItemIText in rewardItems)
        {
            itemsText.text += rewardItemIText.itemName + "  ";
        }

        rewardScreen.SetActive(true);
    }

    public void CloseButton()
    {
        rewardScreen.SetActive(false);
    }

}
    
