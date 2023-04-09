using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlayBackgroundMusic(3);
        Player.instance.gameObject.SetActive(false);
        MenuManager.Instance.gameObject.SetActive(false);
        BattleManager.instance.gameObject.SetActive(false);

    }

    

    public void QuitToMainMenu()
    {

    }

    public void LoadLastSave()
    {

    }

    private static void DestroyGameSession()
    {

    }
}
