using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateColorPlatform : Activate
{
    [SerializeField] private Sprite platformActivated, platformDesactivated;

    [SerializeField] private GameObject platform;

    private bool activated;

    public override void Active()
    {
        platform.GetComponent<SpriteRenderer>().sprite = activated ? platformActivated : platformDesactivated;
        platform.layer = activated ? 9 : 10;
    }
}
