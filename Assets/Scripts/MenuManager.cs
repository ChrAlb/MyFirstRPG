using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MenuManager : MonoBehaviour
{
    [SerializeField] Image imageToFade;

    public static MenuManager Instance;

    public void FadeImage()
    {
        imageToFade.GetComponent<Animator>().SetTrigger("StartFading");
    }


}
