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

    public bool markQuestComplete;
    public string questToComplete;

    private void Start()
    {
        instance = this;        
    }

    private void Update()
    {
       /* 
       if(Input.GetKeyDown(KeyCode.Y))
        {
            OpenRewardScreen(15000, rewardItems);

        }
       */
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

    public void CloseRewardScreen()
    {
        foreach(PlayerStats activePlayer in GameManager.instance.GetPlayerStats())
        {
            activePlayer.AddXP(xpReward);


        }

        foreach (ItemsManager itemsRewarded in rewardItems)
        {
            Inventory.instance.AddItems(itemsRewarded);
        }
        
        rewardScreen.SetActive(false);
        GameManager.instance.battleIsActive = false;

        if(markQuestComplete)
        {
            QuestManager.Instance.MarkQuestComplete(questToComplete);
        }
    }

}
    
