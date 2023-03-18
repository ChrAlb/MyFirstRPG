using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleFolder : MonoBehaviour
{

    [SerializeField] bool isPlayer;
    [SerializeField] string[] attacksAvailable;

    public string characterName;
    public int currentHP, maxHP, currentMana, maxMana, dexterity, defence, wpnwpower, armorDefence;
    public bool isDead;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
