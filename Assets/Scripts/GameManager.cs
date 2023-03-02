using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] PlayerStats[] playerStats;

    public bool gameMenuOpened, dialogBoxOpended, shopOpened;

    public int currentBitcoins = 40;
    
    
    // Start is called before the first frame update
    void Start()
    {
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

}
