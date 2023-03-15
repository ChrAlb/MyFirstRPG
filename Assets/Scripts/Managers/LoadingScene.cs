using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    [SerializeField] float waitToLoad;
    
    // Start is called before the first frame update
    void Start()
    {
        if(waitToLoad > 0)
        {
            StartCoroutine(LoadScene());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }public IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(waitToLoad);
        SceneManager.LoadScene(PlayerPrefs.GetString("Current_Scene"));
        GameManager.instance.LoadData();
        QuestManager.Instance.LoadQuestData();
        
    }
}
