using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] string PlayerName;

    [SerializeField] int maxLevel = 20;
    [SerializeField] int playerLevel = 1;
    [SerializeField] int currentXP;

    [SerializeField] int maxHP = 100;
    [SerializeField] int currentHP;

    [SerializeField] int[] xpForNextLevel;
    [SerializeField] int baseLevelXP = 100;

    [SerializeField] int maxMana = 30;
    [SerializeField] int currentMana;

    [SerializeField] int dexterity;
    [SerializeField] int defence;

    // Start is called before the first frame update
    void Start()
    {
        xpForNextLevel = new int[maxLevel];
        xpForNextLevel[1] = baseLevelXP;

        for (int i = 2; i < xpForNextLevel.Length; i++)
        {
            //print("We are at: " + i);
            xpForNextLevel [i] = (int) (0.02f*i*i*i + 3.06f*i*i + 105.6f*i);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            AddXP(100);
        }
        
    }

    public void AddXP(int amountOfXP)
    {
        currentXP += amountOfXP;
        Debug.Log(currentXP);
        if(currentXP > xpForNextLevel[playerLevel])
        {
            currentXP -= xpForNextLevel[playerLevel];
            playerLevel++;

            if (playerLevel %2 == 0)
            {
                dexterity++;
            }
            else
            {
                defence++;
            }

            maxHP = Mathf.FloorToInt(maxHP * 1.18f);
            currentHP = maxHP;

            maxMana = Mathf.FloorToInt(maxMana * 1.06f);
            currentMana = maxMana;



        }
    }
}
