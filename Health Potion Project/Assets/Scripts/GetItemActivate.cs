using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItemActivate : Activate
{
    public override void Active()
    {
        Debug.Log("Item Get");

        Destroy(gameObject);
    }
}
