using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaEnter : MonoBehaviour
{
    
    public string transitionAreaName;
    private string SceneName;

    // Start is called before the first frame update
    void Start()
    {
        if(transitionAreaName == Player.instance.transitionName)
        {
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
            

        }
       
    }
}

/*
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
            print(ActiveItemsinScene[i].name);
            print(ActiveItemsinScene[i].transform.position.x);
            print(ActiveItemsinScene[i].transform.position.y);
            print(ActiveItemsinScene[i].transform.position.z);
        }
    }

}*/