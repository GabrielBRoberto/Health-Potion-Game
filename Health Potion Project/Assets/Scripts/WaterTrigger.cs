using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTrigger : MonoBehaviour
{
    [SerializeField] private GameObject restorePoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            Player player= collision.GetComponent<Player>();

            if (player.type == PlayerType.Player1)
            {
                player.gameObject.transform.parent = restorePoint.transform;
            }
        }
    }
}
