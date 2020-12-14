using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour
{
    private AudioSource music;
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        music = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void PlayMusic()
    {
        if(music.isPlaying)
        {
            return;
        }
        music.Play();
    }

    void StopMusic()
    {
        music.Stop();
    }

    void Update()
    {
        
    }
}
