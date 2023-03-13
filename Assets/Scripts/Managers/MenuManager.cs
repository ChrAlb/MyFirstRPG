using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class MenuManager : MonoBehaviour
{
    [SerializeField] Image imageToFade;
    public GameObject menu;

    [SerializeField] GameObject[] statsButtons;

    [SerializeField] GameObject itemSlotContainer;
    [SerializeField] Transform itemSlotContainerParent;

    public static MenuManager Instance;

    private PlayerStats[] playerStats;
    [SerializeField] TextMeshProUGUI[] nameText, hpText, manaText, currentXPtext, xpText;
    [SerializeField] Slider[] xpSlider;
    [SerializeField] Image[] characterImage;
    [SerializeField] GameObject[] characterPanel;

    [SerializeField] TextMeshProUGUI statName, statHP, statMana, statDex, statDef, stateEquipedWeapons, stateEquipedArmor;
    [SerializeField] TextMeshProUGUI stateWeaponPower, stateArmorDefence;
    [SerializeField] Image characterStatImage;

    public TextMeshProUGUI itemName, itemDescription;

    public ItemsManager activeItem;

    [SerializeField] GameObject characerChoicePanel;
    [SerializeField] TextMeshProUGUI[] itemsCharacterChoiceNamnes;

    private void Start()
    {

        Instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log("Menu pressed");

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

        stateEquipedWeapons.text = playerSelected.equippedWeaponName;
        stateEquipedArmor.text = playerSelected.equippedArmorName;

        stateWeaponPower.text = playerSelected.weaponPower.ToString();
        stateArmorDefence.text = playerSelected.armorDefence.ToString();
        
    }

    public void UpdateItemInventory()
    {

        // bool firstItem = true;
        
        foreach (Transform itemSlot in itemSlotContainerParent)

        {  
              Destroy(itemSlot.gameObject);
        }
            
        
        foreach (ItemsManager item in Inventory.instance.GetItemList())
        {
            RectTransform itemSlot = Instantiate(itemSlotContainer, itemSlotContainerParent).GetComponent<RectTransform>();

            Image itemImage = itemSlot.Find("Image").GetComponent<Image>();
            itemImage.sprite = item.itemsImage;

            TextMeshProUGUI itemsamountText = itemSlot.Find("Amount Text").GetComponent<TextMeshProUGUI>();
            if (item.amount > 1)
                itemsamountText.text = item.amount.ToString();
            else
                itemsamountText.text = "";

            itemSlot.GetComponent<ItemButton>().itemOnButton = item;

        }
    }
 
    public void DiscardItem()
    {
        Inventory.instance.RemoveItem(activeItem);
        UpdateItemInventory();
        AudioManager.Instance.PlaySFX(3);
    }

    public void UseItem(int selectedCharacter)
    {
        activeItem.UseItem(selectedCharacter);
        OpenCharacterChoicePanel();
        //DiscardItem();   // This should be moved!!

        Inventory.instance.RemoveItem(activeItem);
        UpdateItemInventory();
        AudioManager.Instance.PlaySFX(8);


    }

    public void OpenCharacterChoicePanel()
    {
        characerChoicePanel.SetActive(true);

        for (int i=0; i < playerStats.Length; i++)
        {
            PlayerStats acticePlayer = GameManager.instance.GetPlayerStats()[i];
            itemsCharacterChoiceNamnes[i].text = acticePlayer.PlayerName;

            bool activePlayerAvailabe = acticePlayer.gameObject.activeInHierarchy;

            itemsCharacterChoiceNamnes[i].transform.parent.gameObject.SetActive(activePlayerAvailabe);

        }
    }

    public void CloseCharacterPanel()
    {
        characerChoicePanel.SetActive(false);
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
