using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ActivateColorPlatform : Activate
{
    public Sprite platformActivated, platformDesactivated;

    public GameObject platform;


    public bool activated;

    public override void Active()
    {
        activated = !activated;
    }

    private void Update()
    {
        platform.GetComponent<SpriteRenderer>().sprite = activated ? platformActivated : platformDesactivated;
        platform.layer = activated ? 9 : 10;
    }
}