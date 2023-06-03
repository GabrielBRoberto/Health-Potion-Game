using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTriggerSound : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            if(collision.GetComponent<Player>().type == PlayerType.Player2)
            {
                GetComponent<AudioSource>().Play();
            }
        }
    }
}
