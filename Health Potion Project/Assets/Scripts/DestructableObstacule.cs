using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableObstacule : Activate
{
    public override void Active()
    {
        Destroy(gameObject);
    }
}