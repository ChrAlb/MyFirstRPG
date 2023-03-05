using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dialogText, nameText;
    [SerializeField] GameObject dialogBox, nameBox;

    [SerializeField] string[] dialogSentences;
    [SerializeField] int currentSentence;

    public static DialogController instance;

    private bool dialogJustStarted;

    private string questToMark;
    private bool markTheQuestComplete;
    private bool shouldMarkQuest;
    
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        dialogText.text = dialogSentences[currentSentence];
    }

    // Update is called once per frame
    void Update()
    {
        if(dialogBox.activeInHierarchy)
        {
            if(Input.GetButtonUp("Fire1"))
            {
                if(!dialogJustStarted)
                {
                currentSentence++;
                if(currentSentence >= dialogSentences.Length)
                {
                    dialogBox.SetActive(false);
                    GameManager.instance.dialogBoxOpended = false;

                        if(shouldMarkQuest)
                        {
                            shouldMarkQuest = false;
                            if(markTheQuestComplete)
                            {
                                QuestManager.Instance.MarkQuestComplete(questToMark);
                            }
                            else
                            {
                                QuestManager.Instance.MarkQuestIncomplete(questToMark);
                            }
                        }
                }
                else
                {
                        CheckForName();
                        dialogText.text = dialogSentences[currentSentence]; 
                }
                }
                else 
                {
                   dialogJustStarted = false;
                }
                
                

            }
        }
    }

    public void ActiveQuestAtEnd(string questName, bool markComplete)
    {
        questToMark = questName;
        markTheQuestComplete = markComplete;
        shouldMarkQuest = true;
    }

    public void ActivateDialog(string[] newSentencetoUse)
    {
        dialogSentences = newSentencetoUse;
        currentSentence = 0;

        CheckForName();
        dialogText.text = dialogSentences[currentSentence];
        dialogBox.SetActive(true);

        dialogJustStarted = true;
        GameManager.instance.dialogBoxOpended = true;
    }

    private void CheckForName()
    {
        if(dialogSentences[currentSentence].StartsWith("#"))
        {
            nameText.text = dialogSentences[currentSentence].Replace("#","");
            currentSentence++;
        }
    }

    public bool IsDialogBoxActive()
    {
        return dialogBox.activeInHierarchy;
    }
}
