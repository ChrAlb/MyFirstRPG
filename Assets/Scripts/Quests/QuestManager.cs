using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] string[] questNames;
    [SerializeField] bool[] questMarkersCompleted;

    public static QuestManager Instance;
    
    // Start is called before the first frame update
    void Start()
    {
        
        Instance = this;
        questMarkersCompleted = new bool[questNames.Length];
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            print(CheckIfComplete("Defeat Dragon"));
            MarkQuestComplete("Steal the Gem");
            MarkQuestIncomplete("Take Monster Soul");
        }
    }

    public int GetQuestNumber(string questToFind)
    {
        for (int i = 0; i < questNames.Length; i++)
        {
            if (questNames[i] == questToFind)
            {
                return i;
            }
        }
        
        Debug.LogWarning("Quest: " + questToFind + " does not exist");
        return 0;
    }

    public bool CheckIfComplete(string questToCheck)
    {
        int questNumberToCheck = GetQuestNumber(questToCheck);

        if (questNumberToCheck != 0 )
        {
            return questMarkersCompleted[questNumberToCheck];
        }
        return false;
    }

    public void UpdateQuestObjects()
    {
        QuestObjects[] questObjects= FindObjectsOfType<QuestObjects>();

        if(questObjects.Length > 0)
        {
            foreach (QuestObjects questObject in questObjects)
            {
                questObject.CheckForCompletion();
            }
        }

    }

    public void MarkQuestComplete(string questToMark)
    {
        int questNumberToCheck = GetQuestNumber(questToMark);
        questMarkersCompleted[questNumberToCheck] = true;

        UpdateQuestObjects();
    }

    public void MarkQuestIncomplete(string questToMark)
    {
        int questNumberToCheck = GetQuestNumber(questToMark);
        questMarkersCompleted[questNumberToCheck] = false;

        UpdateQuestObjects();
    }

}
