using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public float musicVolume;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        musicVolume = GameObject.FindAnyObjectByType<AudioSource>().volume;
        

        
    }

    public float SetMusicVolume(float volume)
    {
        return volume;
    }
}