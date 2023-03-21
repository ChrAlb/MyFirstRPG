using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    
    public static BattleManager instance;
    private bool isBattleActive;

    [SerializeField] GameObject battleScene;
    [SerializeField] List<BattleCharacters> activeCharacters = new List<BattleCharacters>();

    [SerializeField] Transform[] playerPositions, enemyPositions;

    [SerializeField] BattleCharacters[] playerPrefabs, enemiesPrefabs;

    
    // Start is called before the first frame update
    void Start()
    {
        instance = this; 
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            StartBattle(new string[] { "Mage Master", "Warlock", "Mage", "Blueface" });
        }
    }

    public void StartBattle(string[] enemiesToSpawn)
    {
        SettingUpBattle();
        AddingPlayers();

        for (int i = 0; i < enemiesToSpawn.Length; i++)
        {
            if(enemiesToSpawn[i] != "")
            {
             for (int j = 0; j < enemiesPrefabs.Length; j++)
                {
                    if(enemiesPrefabs[j].characterName == enemiesToSpawn[i])
                    {
                        BattleCharacters newEnemey = Instantiate( 
                            enemiesPrefabs[j],
                            enemyPositions[i].position,
                            enemyPositions[i].rotation,
                            enemyPositions[i]);
                        activeCharacters.Add(newEnemey);
                    }

                }
            }
        }

    }

    private void AddingPlayers()
    {
        for (int i = 0; i < GameManager.instance.GetPlayerStats().Length; i++)
        {


            if (GameManager.instance.GetPlayerStats()[i].gameObject.activeInHierarchy)
            {

                for (int j = 0; j < playerPrefabs.Length; j++)
                {
                    //Debug.Log("bevor" + playerPrefabs[j].characterName);
                    if (playerPrefabs[j].characterName == GameManager.instance.GetPlayerStats()[i].PlayerName)
                    {
                        //Debug.Log(GameManager.instance.GetPlayerStats()[i].PlayerName);

                        BattleCharacters newPlayer = Instantiate(
                            playerPrefabs[j],
                            playerPositions[i].position,
                            playerPositions[i].rotation,
                            playerPositions[i]
                            );

                        activeCharacters.Add(newPlayer);
                        ImportPlayerStats(i);

                    }
                }
            }
        }
    }

    private void ImportPlayerStats(int i)
    {
        PlayerStats player = GameManager.instance.GetPlayerStats()[i];

        activeCharacters[i].currentHP = player.currentHP;
        activeCharacters[i].maxHP = player.maxHP;

        activeCharacters[i].currentMana = player.currentHP;
        activeCharacters[i].maxMana = player.maxMana;

        activeCharacters[i].dexterity = player.dexterity;
        activeCharacters[i].defence = player.defence;

        activeCharacters[i].wpnwpower = player.weaponPower;
        activeCharacters[i].armorDefence = player.armorDefence;
    }

    private void SettingUpBattle()
    {
        if (!isBattleActive)
        {
            isBattleActive = true;
            GameManager.instance.battleIsActive = true;

            transform.position = new Vector3(
                Camera.main.transform.position.x,
                Camera.main.transform.position.y,
                transform.position.z);

            battleScene.SetActive(true);
        }
    }
}
