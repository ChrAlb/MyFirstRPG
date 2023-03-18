using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    
    public static BattleManager instance;
    private bool isBattleActive;

    [SerializeField] GameObject battleScene;

    [SerializeField] Transform[] playerPositions, enemyPositions;

    [SerializeField] BattleCharacters playerPrefabs, enemiesPrefabs;
    
    // Start is called before the first frame update
    void Start()
    {
        instance = this; 
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
