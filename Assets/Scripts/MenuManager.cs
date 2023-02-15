using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class MenuManager : MonoBehaviour
{
    [SerializeField] Image imageToFade;
    [SerializeField] GameObject menu;

    [SerializeField] GameObject[] statsButtons;

    public static MenuManager Instance;

    private PlayerStats[] playerStats;
    [SerializeField] TextMeshProUGUI[] nameText, hpText, manaText, currentXPtext, xpText;
    [SerializeField] Slider[] xpSlider;
    [SerializeField] Image[] characterImage;
    [SerializeField] GameObject[] characterPanel;

    [SerializeField] TextMeshProUGUI statName, statHP, statMana, statDex, statDef;
    [SerializeField] Image characterStatImage;
 
    private void Start()
    {

        Instance = this;
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
                UpdateStats(); // Hierhin korrigiert
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

            //Bei Szenenwechsel von 2 zu 1 geht das nicht mehr?
            xpText[i].text = playerStats[i].currentXP.ToString() + "/" + playerStats[i].xpForNextLevel[playerStats[i].playerLevel];
            xpSlider[i].maxValue = playerStats[i].xpForNextLevel[playerStats[i].playerLevel];
            xpSlider[i].value = playerStats[i].currentXP;
        }
    }

    public void StatsMenu()
    {
        for (int i = 0; i< playerStats.Length; i++)
        {
            statsButtons[i].SetActive(true);

            statsButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = playerStats[i].PlayerName;
        }

        StatsMenuUpdate(0);
    }

    public void StatsMenuUpdate(int playerSelectedNumber)
    {
        PlayerStats playerSelected = playerStats[playerSelectedNumber];

        statName.text = playerSelected.PlayerName;
        statHP.text = playerSelected.currentHP.ToString() + "/" + playerSelected.maxHP;
        statMana.text = playerSelected.currentMana.ToString() + "/" + playerSelected.maxMana;
        statMana.text = playerSelected.currentMana.ToString() + "/" + playerSelected.maxMana;

        statDex.text = playerSelected.dexterity.ToString();
        statDef.text = playerSelected.defence.ToString();

        characterStatImage.sprite = playerSelected.characterImage;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void FadeImage()
    {
        imageToFade.GetComponent<Animator>().SetTrigger("StartFading");
    }
}
