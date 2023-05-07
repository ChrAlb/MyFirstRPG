using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{
    [SerializeField] string sceneToLoad;
    [SerializeField] string transitionAreaName;
    [SerializeField] AreaEnter theAreaEnter;

    private GameObject[] ActiveItemsinScene;
    private string SzeneName;

    // Start is called before the first frame update
    void Start()
    {
       theAreaEnter.transitionAreaName = transitionAreaName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SaveActiveItemsinScene();
            Player.instance.transitionName  = transitionAreaName;
            //MenuManager.Instance.FadeImage();
            StartCoroutine(LoadSceneCoroutine());
        }
        
    }

    private void SaveActiveItemsinScene()
    {
        SzeneName = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("Szene_", SzeneName);

        ActiveItemsinScene = GameObject.FindGameObjectsWithTag("Item");

        PlayerPrefs.SetInt("AnzahlItems_" + SzeneName, ActiveItemsinScene.Length);
        
        for (int i = 0; i < ActiveItemsinScene.Length; i++)
        {                
            PlayerPrefs.SetString("Item_" + SzeneName + i, ActiveItemsinScene[i].name);
            //print(ActiveItemsinScene[i].name); 
        }
        
    }

    IEnumerator LoadSceneCoroutine()
    {
        
        yield return new WaitForSeconds(0.7f);
        SceneManager.LoadScene(sceneToLoad);
    }
}
