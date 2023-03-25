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

    [SerializeField] int currentTurn;
    [SerializeField] bool waitingForTurn;
    [SerializeField] GameObject UIButtonHolder;

    
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
            StartBattle(new string[] { "Mage Master", "Warlock"});
        }

        if(Input.GetKeyDown(KeyCode.N))
        {
            NextTurn();
        }

        if (isBattleActive)
        {
            if(waitingForTurn)
            {
                if(activeCharacters[currentTurn].IsPlayer())
                    UIButtonHolder.SetActive(true);
                else
                    UIButtonHolder.SetActive(false);
                    StartCoroutine(EnemyMoveCoroutine());

                }




            }
        }
    }

    public void StartBattle(string[] enemiesToSpawn)
    {
        if (!isBattleActive)
        {
            SettingUpBattle();
            AddingPlayers();
            AddingEnemies(enemiesToSpawn);

            waitingForTurn = true;
            currentTurn = 0; //Random.RandomRange(0,activeCharacters.Count);
        }

    }

    private void AddingEnemies(string[] enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn.Length; i++)
        {
            if (enemiesToSpawn[i] != "")
            {
                for (int j = 0; j < enemiesPrefabs.Length; j++)
                {
                    if (enemiesPrefabs[j].characterName == enemiesToSpawn[i])
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
        
        
            isBattleActive = true;
            GameManager.instance.battleIsActive = true;

            transform.position = new Vector3(
                Camera.main.transform.position.x,
                Camera.main.transform.position.y,
                transform.position.z);

            battleScene.SetActive(true);
        
    }

    private void NextTurn()
    {
        currentTurn++;
        if(currentTurn >=  activeCharacters.Count)
            currentTurn = 0;
    }

    private void UpdateBattle()
    {
        bool allEnemiesAreDead = true;
        bool allPlayersAreDead = true;

        for (int i = 0; i < activeCharacters.Count; i++)
        {
            if(activeCharacters[i].currentHP < 0 )
            {
                activeCharacters[i].currentHP = 0;
            }

            if (activeCharacters[i].currentHP == 0)
            {
                //kill Character
            }
            else 
            {
                if(activeCharacters[i].IsPlayer())
                {
                    allPlayersAreDead = false;
                }
                else
                {
                    allEnemiesAreDead = false;
                }
            }

            if(allEnemiesAreDead || allPlayersAreDead)
            {
                if (allEnemiesAreDead)
                     print("We won !!!!");
                else if(allPlayersAreDead)
                    print("We lost !!");
                battleScene.SetActive(false);
                GameManager.instance.battleIsActive = false;
                isBattleActive = false;
            }
               
        }

    }

    public IEnumerator EnemyMoveCoroutine()
    {
        waitingForTurn=false;

        yield return new WaitForSeconds(1f);

        EnemyAttack();

        yield return new WaitForSeconds(1f);

        NextTurn();
    }

    private void EnemyAttack()
    {
        List<int> players = new List<int>();
        

        for (int i = 0; i < activeCharacters.Count; i++)
        {
            if(activeCharacters[i].IsPlayer() && activeCharacters[i].currentHP > 0 )
            {
                players.Add(i);
            }

        }
        int selectedPlayerToAttack = players[Random.Range(0, players.Count)];
    }

}

