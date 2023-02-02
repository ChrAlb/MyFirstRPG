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
    [SerializeField] TextMeshProUGUI[] nameText, hpText, manaText, lvlText, xpText;
    [SerializeField] Slider[] xpSlider;
    [SerializeField] Image[] charImage;
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
                UpdateStats();
                menu.SetActive(false);
                GameManager.instance.gameMenuOpened = false;
                
            }
            else
            {
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
            print(i);
            characterPanel[i].SetActive(true);
        }
    }

    
}
