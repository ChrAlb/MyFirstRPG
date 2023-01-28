using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] string PlayerName;

    [SerializeField] int maxLevel = 20;
    [SerializeField] int playerLevel;
    [SerializeField] int currentXP;

    [SerializeField] int maxHP = 100;
    [SerializeField] int currentHP;

    [SerializeField] int[] xpForEachLevel;
    [SerializeField] int baseLevelXP = 100;

    [SerializeField] int maxMana = 30;
    [SerializeField] int currentMana;

    [SerializeField] int dexterity;
    [SerializeField] int defence;

    // Start is called before the first frame update
    void Start()
    {
        xpForEachLevel = new int[maxLevel];
        xpForEachLevel[1] = baseLevelXP;

        for (int i = 2; i < xpForEachLevel.Length; i++)
        {
            //print("We are at: " + i);
            xpForEachLevel [i] = baseLevelXP *i;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
