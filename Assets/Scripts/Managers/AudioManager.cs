using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource[] SFX, backgroudMusic;

    public static AudioManager Instance;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            PlaySFX(3);
        }
    }

    public void PlaySFX ( int soundToPlay)
    {
        SFX[soundToPlay].Play();
    }

}
