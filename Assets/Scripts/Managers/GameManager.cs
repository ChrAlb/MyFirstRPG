using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] PlayerStats[] playerStats;

    public bool gameMenuOpened, dialogBoxOpended, shopOpened;

    public int currentBitcoins;
    
    
    // Start is called before the first frame update
    void Start()
    {
        currentBitcoins = 35;
        
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(gameObject);

        playerStats = FindObjectsOfType<PlayerStats>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("Data has been saved");
            SaveData();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log("Data has been loaded");
            LoadData();
        }

        if (gameMenuOpened || dialogBoxOpended || shopOpened)

        {
            Player.instance.deactivateMovement = true;
        }
        else
        {
            Player.instance.deactivateMovement = false;
        }

    }

    public PlayerStats[] GetPlayerStats()
    {
        return playerStats;
    }

    public void SaveData()
    {
        PlayerPrefs.SetFloat("Player_Pos_X", Player.instance.transform.position.x);
        PlayerPrefs.SetFloat("Player_Pos_Y", Player.instance.transform.position.y);
        PlayerPrefs.SetFloat("Player_Pos_Z", Player.instance.transform.position.z);

        for  (int i = 0; i< playerStats.Length; i++)
        {
            if (playerStats[i].gameObject.activeInHierarchy)
            {
                PlayerPrefs.SetInt("Player_" + playerStats[i].PlayerName + "_active",1);
            }
            else
            {
                PlayerPrefs.SetInt("Player_" + playerStats[i].PlayerName + "_active", 0);
            }

            PlayerPrefs.SetInt("Player_" + playerStats[i].PlayerName + "_Level", playerStats[i].playerLevel);
            PlayerPrefs.SetInt("Player_" + playerStats[i].PlayerName + "_CurrentXP", playerStats[i].currentXP);

            PlayerPrefs.SetInt("Player_" + playerStats[i].PlayerName + "_MaxHP", playerStats[i].maxHP);
            PlayerPrefs.SetInt("Player_" + playerStats[i].PlayerName + "_CurrentHP", playerStats[i].currentHP);

            PlayerPrefs.SetInt("Player_" + playerStats[i].PlayerName + "_MaxMana", playerStats[i].maxMana);
            PlayerPrefs.SetInt("Player_" + playerStats[i].PlayerName + "_CurrentMana", playerStats[i].currentMana);

            PlayerPrefs.SetInt("Player_" + playerStats[i].PlayerName + "_Dexterity", playerStats[i].dexterity);
            PlayerPrefs.SetInt("Player_" + playerStats[i].PlayerName + "_Defence", playerStats[i].defence);

            PlayerPrefs.SetString("Player_" + playerStats[i].PlayerName + "_EquipedWeapon", playerStats[i].equippedWeaponName);
            PlayerPrefs.SetString("Player_" + playerStats[i].PlayerName + "_EquipedArmor", playerStats[i].equippedArmorName);

            PlayerPrefs.SetInt("Player_" + playerStats[i].PlayerName + "_WeaponPower", playerStats[i].weaponPower);
            PlayerPrefs.SetInt("Player_" + playerStats[i].PlayerName + "_ArmorDefence", playerStats[i].armorDefence);

        }


    }

    public void LoadData()
    {
        Player.instance.transform.position = new Vector3(
        PlayerPrefs.GetFloat("Player_Pos_X"),
        PlayerPrefs.GetFloat("Player_Pos_Y"),
        PlayerPrefs.GetFloat("Player_Pos_Z")
        );
    }
                  

}
