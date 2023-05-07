using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaEnter : MonoBehaviour
{
    
    public string transitionAreaName;
    private string SceneName;
    private int AnzahlItems;
    private string ItemName;

    private GameObject[] ItemsInScene;
    

    // Start is called before the first frame update
    void Start()
    {
       
        if(transitionAreaName == Player.instance.transitionName)
        {
            LoadActiveItemsinScene();
            Player.instance.transform.position = transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LoadActiveItemsinScene()
    {
        if (PlayerPrefs.HasKey("Szene_" + SceneManager.GetActiveScene().buildIndex))
        {
            SceneName = PlayerPrefs.GetString("Szene_" + SceneManager.GetActiveScene().buildIndex);
            ItemsInScene = GameObject.FindGameObjectsWithTag("Item");
            AnzahlItems = PlayerPrefs.GetInt("AnzahlItems_" + SceneName);

            /*
            for (int j = 0; j < AnzahlItems; j++)
            {
                ItemName = PlayerPrefs.GetString("Item_" + SceneName + j);
                SavedItems[count] = ItemName;
                count++;
                
            }*/

            for (int i = 0; i < ItemsInScene.Length; i++)
            {
                if (!InSafedItems(ItemsInScene[i].name))
                {
                    ItemsInScene[i].SetActive(false);
                }  
            }
        }       
    }

    private bool InSafedItems(string item)
    {
        for (int i = 0; i < AnzahlItems; i++)
        {
            if (PlayerPrefs.GetString("Item_" + SceneName + i) == item)
            {
                return true;
            }
              
        }
        return false;
    }

   
}

