using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class MenuManager : MonoBehaviour
{
    [SerializeField] Image imageToFade;
    [SerializeField] GameObject menu;
    

    public static MenuManager Instance;

    private PlayerStats[] playerStats;
    [SerializeField] TextMeshProUGUI[] nameText, hpText, manaText, currentXPtext, xpText;
    [SerializeField] Slider[] xpSlider;
    [SerializeField] Image[] characterImage;
    [SerializeField] GameObject[] characterPanel;

    
    public void FadeImage()
    {
        imageToFade.GetComponent<Animator>().SetTrigger("StartFading");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            
            
            if (menu.activeInHierarchy)
            {
                //UpdateStats(); // Im Tutorial hier. Beim ersten Aufruf kommen aber Stats nicht!
                menu.SetActive(false);
                GameManager.instance.gameMenuOpened = false;
                
            }
            else
            {
                UpdateStats(); // Hier in korrigiert
                menu.SetActive(true);
                GameManager.instance.gameMenuOpened = true;
            }
        }


    }

    public void UpdateStats()
    {
        playerStats = GameManager.instance.GetPlayerStats();
        
        for (int i = 0; i < playerStats.Length; i++)
        {
            characterPanel[i].SetActive(true);

            nameText[i].text = playerStats[i].PlayerName;
            hpText[i].text = "HP: " + playerStats[i].currentHP + "/" +playerStats[i].maxHP;
            manaText[i].text = "Mana: " + playerStats[i].currentMana + "/" +playerStats[i].maxMana;
            currentXPtext[i].text = "Current XP: " + playerStats[i].currentXP ;

            characterImage[i].sprite = playerStats[i].characterImage;

            xpText[i].text = playerStats[i].currentXP.ToString() + "/" + playerStats[i].xpForNextLevel[playerStats[i].playerLevel];
            xpSlider[i].maxValue = playerStats[i].xpForNextLevel[playerStats[i].playerLevel];
            xpSlider[i].value = playerStats[i].currentXP;
        }
    }

    
}
