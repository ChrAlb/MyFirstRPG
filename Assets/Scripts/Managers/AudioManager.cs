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
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            PlayBackgroundMusic(1);
        }
    }

    public void PlaySFX ( int soundToPlay)
    {
        if(soundToPlay < SFX.Length)
        {
            SFX[soundToPlay].Play();
        }
        
    }

    public void PlayBackgroundMusic (int musicToPlay)
    {
       StopMusic();
        
        if (musicToPlay < backgroudMusic.Length)
        {
            backgroudMusic[musicToPlay].Play();
        }
    }

    private void StopMusic()
    {
        foreach(AudioSource song in backgroudMusic)
        {
            song.Stop();
        }
    }

}
