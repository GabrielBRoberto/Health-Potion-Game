using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Detection : MonoBehaviour
{
    private Player player;

    [SerializeField]
    private DetectionType type;

    private void Start()
    {
        player = GetComponentInParent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground" && type == DetectionType.Ground)
        {
            player.isGrounded = true;
            player.canJump = true;
        }

        if (collision.tag == "Wall" && type == DetectionType.Wall)
        {
            player.canInteract = true;
        }

        if (collision.tag == "WineWall" && type == DetectionType.WineWall)
        {
            player.canInteract = true;
        }

        if (collision.tag == "DestructableWall" && player.isDashing)
        {
            collision.GetComponent<DestructableObstacule>().Active();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ground" && type == DetectionType.Ground)
        {
            player.isGrounded = false;
        }

        if (collision.tag == "Wall" && type == DetectionType.Wall)
        {
            player.canInteract = false;
        }

        if (collision.tag == "WineWall" && type == DetectionType.WineWall)
        {
            player.canInteract = false;
        }
    }
}

public enum DetectionType
{
    Wall,
    WineWall,
    Ground
}