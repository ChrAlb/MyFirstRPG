using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogHandler : MonoBehaviour
{
    public string[] sentences;
    private bool canActivateBox;

    [SerializeField] bool shouldAcitvateQuest;
    [SerializeField] string questToMark;
    [SerializeField] bool markAsComplete;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canActivateBox && Input.GetButtonDown("Fire1") && !DialogController.instance.IsDialogBoxActive())
        {
            DialogController.instance.ActivateDialog(sentences);

            if (shouldAcitvateQuest)
            {
                DialogController.instance.ActiveQuestAtEnd(questToMark, markAsComplete);
            }
        }  
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            canActivateBox = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canActivateBox = false;
        }
    }
}
