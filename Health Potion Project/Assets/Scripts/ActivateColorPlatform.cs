using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ActivateColorPlatform : Activate
{
    [SerializeField]
    public GameObject[] platforms;

    public override void Active()
    {
        for (int i = 0; i < platforms.Length; i++)
        {
            platforms[i].GetComponent<PlatformColor>().activate = !platforms[i].GetComponent<PlatformColor>().activate;
        }

        GetComponent<Animator>().SetTrigger("trig");
    }

    private void Update()
    {
        for (int i = 0; i < platforms.Length; i++)
        {
            platforms[i].GetComponent<SpriteRenderer>().sprite = platforms[i].GetComponent<PlatformColor>().activate ? 
                platforms[i].GetComponent<PlatformColor>().platformActivated : platforms[i].GetComponent<PlatformColor>().platformDesactivated;

            platforms[i].layer = platforms[i].GetComponent<PlatformColor>().activate ? platforms[i].GetComponent<PlatformColor>().activatedLayer : 11;
        }
    }
}