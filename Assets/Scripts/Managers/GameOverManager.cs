using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        
        DestroyGameSession();
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadLastSave()
    {
        DestroyGameSession();
        SceneManager.LoadScene("LoadingScene");
    }

    private static void DestroyGameSession()
    {

        Destroy(GameManager.instance.gameObject);
        Destroy(Player.instance.gameObject);
        Destroy(MenuManager.Instance.gameObject);
        Destroy(BattleManager.instance.gameObject);

    }

    public void QuitGame()
    {
        Debug.Log("WE'v quit the game");
        Application.Quit();
    }
}
