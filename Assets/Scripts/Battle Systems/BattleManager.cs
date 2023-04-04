//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    [SerializeField] BattleMoves[] battleMovesList;

    [SerializeField] ParticleSystem characterAttackEffect;

    [SerializeField] CharacterDamageGUI damageText;

    [SerializeField] GameObject[] playerBattleStats;
    [SerializeField] TextMeshProUGUI[] playersNameText;
    [SerializeField] Slider[] playerHealthSlider, playerManaSlider;

    [SerializeField] GameObject enemyTargetPanel;

    [SerializeField] BattleTargetButtons[] targetButtons;

    public GameObject magicChoicePanel;

    [SerializeField] BattleMagicButtons[] magicButtons;

    public BattleNotifications battleNotice;

    [SerializeField] float chanceToRunAway = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            StartBattle(new string[] { "Mage Master", "Warlock", "Blueface", "Mage" });
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            NextTurn();
        }

        CheckPlayerButtonHolders();
    }

    private void CheckPlayerButtonHolders()
    {
        if (isBattleActive)
        {
            if (waitingForTurn)
            {
                if (activeCharacters[currentTurn].IsPlayer())
                    UIButtonHolder.SetActive(true);
                else
                {
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
            UpdatePlayerStats();

            waitingForTurn = true;
            currentTurn = 0; // Random.Range(0, activeCharacters.Count);

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
                        BattleCharacters newEnemy = Instantiate(
                            enemiesPrefabs[j],
                            enemyPositions[i].position,
                            enemyPositions[i].rotation,
                            enemyPositions[i]
                            );
                        activeCharacters.Add(newEnemy);
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

        activeCharacters[i].currentMana = player.currentMana;
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
        if (currentTurn >= activeCharacters.Count)
        {
            currentTurn = 0;
        }

        waitingForTurn = true;
        UpdateBattle();
        UpdatePlayerStats();
    }

    private void UpdateBattle()
    {
        bool allEnemiesAreDead = true;
        bool allPlayerAreDead = true;

        for (int i = 0; i < activeCharacters.Count; i++)
        {
            if (activeCharacters[i].currentHP < 0)
            {
                activeCharacters[i].currentHP = 0;
            }

            if (activeCharacters[i].currentHP == 0)
            {
                //killCharacter;
            }
            else
            {
                if (activeCharacters[i].IsPlayer())
                    allPlayerAreDead = false;
                else
                    allEnemiesAreDead = false;
            }
        }

        if (allEnemiesAreDead || allPlayerAreDead)
        {
            if (allEnemiesAreDead)
                print("WE WON !!!!");
            else
                if (allPlayerAreDead)
                print("WE LOST!!");


            battleScene.SetActive(false);
            GameManager.instance.battleIsActive = false;
            isBattleActive = false;
        }
        else
        {
            while (activeCharacters[currentTurn].currentHP == 0)
            {
                currentTurn++;
                if (currentTurn >= activeCharacters.Count)
                {
                    currentTurn = 0;
                }
            }
        }
    }

    public IEnumerator EnemyMoveCoroutine()
    {
        waitingForTurn = false;
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
            if (activeCharacters[i].IsPlayer() && activeCharacters[i].currentHP > 0)
            {
                players.Add(i);
            }
        }
        int selectedPlayerToAttack = players[Random.Range(0, players.Count)];

        int selectedAttack = Random.Range(0, activeCharacters[currentTurn].AttackMovesAvailable().Length);

        int movePower = 0;

        for (int i = 0; i < battleMovesList.Length; i++)
        {
            if (battleMovesList[i].moveName == activeCharacters[currentTurn].AttackMovesAvailable()[selectedAttack])
            {
                movePower = GettingMovePowerAndEffectInstantion(selectedPlayerToAttack, i);
            }
        }

        // Instantiating the effect on the attacking character
        InstantiateEffectOnAttackingCharacter();

        DealDammageToCharacters(selectedPlayerToAttack, movePower);

        UpdatePlayerStats();
    }

    public void OpenTargetMenu(string moveName)
    {
        enemyTargetPanel.SetActive(true);

        List<int> Enemies  = new List<int>();

        for (int i=0; i < activeCharacters.Count;i++)
        {
            if(!activeCharacters[i].IsPlayer())
            {
                Enemies.Add(i);
            }
        }

        //Debug.Log(Enemies.Count);

        for (int i=0; i< targetButtons.Length; i++)
        {
            if(Enemies.Count > i)
            {
                targetButtons[i].gameObject.SetActive(true);
                targetButtons[i].moveName = moveName;
                targetButtons[i].activeBattleTarget = Enemies[i];
                targetButtons[i].targetName.text = activeCharacters[Enemies[i]].characterName;
            }
        }

    }

    private void InstantiateEffectOnAttackingCharacter()
    {
        Instantiate(
            characterAttackEffect,
            activeCharacters[currentTurn].transform.position,
            activeCharacters[currentTurn].transform.rotation
            );
    }

    private void DealDammageToCharacters(int selectedCharaterToAttack, int movePower)
    {
        float attackPower = activeCharacters[currentTurn].dexterity + activeCharacters[currentTurn].wpnwpower;
        float defenceAmount = activeCharacters[selectedCharaterToAttack].defence + activeCharacters[selectedCharaterToAttack].armorDefence;

        float damageAmout = (attackPower / defenceAmount) * movePower * Random.Range(0.9f, 1.1f);
        int damageToGive = (int)damageAmout;

        damageToGive = CalculateCritical(damageToGive);

        Debug.Log(activeCharacters[currentTurn].name + " just dealt " + damageAmout + "(" + damageToGive + ")"
            + ") to " + activeCharacters[selectedCharaterToAttack]);

        activeCharacters[selectedCharaterToAttack].TakeHPDamage(damageToGive);

        CharacterDamageGUI characterDamageText = Instantiate(
            damageText,
            activeCharacters[selectedCharaterToAttack].transform.position,
            activeCharacters[selectedCharaterToAttack].transform.rotation
                    );

        characterDamageText.SetDammage(damageToGive);

    }

    private int CalculateCritical(int damageToGive)
    {
        if (Random.value <= 0.1f)
        {
            Debug.Log("CRITICAL HIT!!! instead of" + damageToGive + " points. " + (damageToGive * 2) + " was dealt");
            return damageToGive * 2;
        }
        else
            return damageToGive;
    }

    public void UpdatePlayerStats()
    {
        for (int i = 0; i < playersNameText.Length; i++)
        {
            if (activeCharacters.Count > i)
            {
                if (activeCharacters[i].IsPlayer())
                {
                    BattleCharacters playerData = activeCharacters[i];

                    playersNameText[i].text = playerData.characterName;

                    playerHealthSlider[i].maxValue = playerData.maxHP;
                    playerHealthSlider[i].value = playerData.currentHP;

                    playerManaSlider[i].maxValue = playerData.maxMana;
                    playerManaSlider[i].value = playerData.currentMana;


                }
                else
                {
                    playerBattleStats[i].gameObject.SetActive(false);
                }
            }
            else
            {
                playerBattleStats[i].gameObject.SetActive(false);
            }
        }
    }


    public void PlayerAttack(string moveName, int selectEnemyTarget)
    {
        
        int movePower = 0;

        for (int i = 0; i < battleMovesList.Length; i++)
        {
            if(battleMovesList[i].moveName == moveName)
            {
                movePower = GettingMovePowerAndEffectInstantion(selectEnemyTarget, i);
            }

        }

        InstantiateEffectOnAttackingCharacter();

        DealDammageToCharacters(selectEnemyTarget, movePower);

        NextTurn();

        enemyTargetPanel.SetActive(false);
    }

    private int GettingMovePowerAndEffectInstantion(int selectCharacterTarget, int i)
    {
        int movePower;
        Instantiate(
               battleMovesList[i].theEffectToUse,
               activeCharacters[selectCharacterTarget].transform.position,
               activeCharacters[selectCharacterTarget].transform.rotation
               );
        movePower = battleMovesList[i].movePower;
        return movePower;
    }

    public void OpenMagicPanel()
    {
        magicChoicePanel.SetActive(true);

        for (int i = 0; i < magicButtons.Length; i++)
        {
            if (activeCharacters[currentTurn].AttackMovesAvailable().Length > i)
            {
                magicButtons[i].gameObject.SetActive(true);
                magicButtons[i].spellName = GetCurrentActiveCharacter().AttackMovesAvailable()[i];
                magicButtons[i].spellNameText.text = magicButtons[i].spellName;

                for (int j = 0; j < battleMovesList.Length; j++)
                {
                    if (battleMovesList[j].moveName == magicButtons[i].spellName)
                    {
                        magicButtons[i].spellCost = battleMovesList[j].manaCost;
                        magicButtons[i].spellCostText.text = magicButtons[i].spellCost.ToString();

                    }
                }
            }
            else
            {
                magicButtons[i].gameObject.SetActive(false);
            }
        }
    }

    public BattleCharacters GetCurrentActiveCharacter()
    {
        return activeCharacters[currentTurn];
    }

    public void RunAway()
    {
        if(Random.value > chanceToRunAway)
        {
            isBattleActive = false;
            battleScene.SetActive(false);
        }
        else
        {
            NextTurn();
            battleNotice.SetText("There is no escape !");
            battleNotice.Activate();
        }
    }
}
