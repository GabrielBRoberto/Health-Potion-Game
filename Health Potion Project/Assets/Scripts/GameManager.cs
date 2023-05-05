using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public float musicVolume;

    // Start is called before the first frame update
    void Start()
    {
        musicVolume = PlayerPrefs.GetFloat("MusicVolume");
    }

    // Update is called once per frame
    void Update()
    {
        musicVolume = GameObject.FindAnyObjectByType<AudioSource>().volume;

        Debug.Log(musicVolume);

        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
    }
}