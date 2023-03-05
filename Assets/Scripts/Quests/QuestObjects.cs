using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObjects : MonoBehaviour
{

    [SerializeField] GameObject objectToActiate;
    [SerializeField] string questToCheck;
    [SerializeField] bool activateIfComplete;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            CheckForCompletion();

        }
    }

    public void CheckForCompletion()
    {
        if (QuestManager.Instance.CheckIfComplete(questToCheck))
        {
            objectToActiate.SetActive(activateIfComplete);
        }
    }


    
}
