using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestZone : MonoBehaviour
{

    [SerializeField] string questToMark;
    [SerializeField] bool markAsComplet;
    [SerializeField] bool markOnEnter;

    private bool canMark;

    public bool deactivateOnMarking;
    
    

    // Update is called once per frame
    void Update()
    {
        if(canMark && Input.GetButtonDown("Fire1"))
        {
            canMark = false;
            MarkTheQuest();

        }
    }

    public void MarkTheQuest()
    {
        if(markAsComplet)
        {
            QuestManager.Instance.MarkQuestComplete(questToMark);
        }
        else
        {
            QuestManager.Instance.MarkQuestIncomplete(questToMark);
        }
        gameObject.SetActive(!deactivateOnMarking);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            MarkTheQuest();
        }
        else
        {
            canMark = true;
        }
    }

}
