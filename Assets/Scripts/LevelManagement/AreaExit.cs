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
            Player.instance.transitionName  = transitionAreaName;
            //MenuManager.Instance.FadeImage();
            StartCoroutine(LoadSceneCoroutine());
        }
        
    }

    private void SaveActiveItemsinScene()
    {
        PlayerPrefs.SetString("Szene_" + SceneManager.GetActiveScene().buildIndex, SceneManager.GetActiveScene().name);

        ActiveItemsinScene = GameObject.FindGameObjectsWithTag("Item");
        for (int i = 0; i < ActiveItemsinScene.Length; i++)
        {
            if (ActiveItemsinScene[i].activeInHierarchy)
            {
                PlayerPrefs.SetString("Item_" + ActiveItemsinScene[i], ActiveItemsinScene[i].name);

                PlayerPrefs.SetFloat("Item_Pos_X" + ActiveItemsinScene[i], ActiveItemsinScene[i].transform.position.x);
                PlayerPrefs.SetFloat("Item_Pos_Y" + ActiveItemsinScene[i], ActiveItemsinScene[i].transform.position.y);
                PlayerPrefs.SetFloat("Item_Pos_Z" + ActiveItemsinScene[i], ActiveItemsinScene[i].transform.position.z);
                print (ActiveItemsinScene[i].name);
                print (ActiveItemsinScene[i].transform.position.x);
                print (ActiveItemsinScene[i].transform.position.y);
                print (ActiveItemsinScene[i].transform.position.z);
            }
        }
        
    }

    IEnumerator LoadSceneCoroutine()
    {
        SaveActiveItemsinScene();
        yield return new WaitForSeconds(0.7f);
        SceneManager.LoadScene(sceneToLoad);
    }
}
