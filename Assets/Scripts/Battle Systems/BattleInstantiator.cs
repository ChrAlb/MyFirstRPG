using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleInstantiator : MonoBehaviour
{
    [SerializeField] BattleTypeManager[] availableBattles;

    [SerializeField] bool activateOnEnter;
    private bool inArea;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(activateOnEnter)
        {
            print("Start the battle");
        }
        else
        {
            inArea = true;
        }
    }

}
