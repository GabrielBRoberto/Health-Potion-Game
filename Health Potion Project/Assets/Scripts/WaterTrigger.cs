using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            GameObject[] allPlayers = GameObject.FindGameObjectsWithTag("Player");

            for (int i = 0; i < allPlayers.Length; i++)
            {
                allPlayers[i].transform.position = allPlayers[i].GetComponent<Player>().respawnPosition.position;
            }
        }
    }
}
