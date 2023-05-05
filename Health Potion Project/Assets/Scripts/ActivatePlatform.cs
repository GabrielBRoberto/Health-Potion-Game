using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ActivatePlatform : MonoBehaviour
{
    [SerializeField]
    private GameObject A, B;

    [SerializeField]
    private GameObject platform;

    [HideInInspector]
    public bool isMoving = false;


    private void Update()
    {
        if (!isMoving)
        {
            return;
        }

        if (platform.transform.position == A.transform.position)
        {
            StartCoroutine(Vector3LerpCoroutine(platform, B.transform.position, 5f));
        }
        if (platform.transform.position == B.transform.position)
        {
            StartCoroutine(Vector3LerpCoroutine(platform, A.transform.position, 5f));
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (collision.GetComponent<Player>().type == PlayerType.Player1)
            {
                if (collision.GetComponent<Player>().inputActions.Player1.Interact.triggered)
                {
                    isMoving = true;
                }
            }
            else
            {
                if (collision.GetComponent<Player>().inputActions.Player2.Interact.triggered)
                {
                    isMoving = true;
                }
            }
        }
    }

    IEnumerator Vector3LerpCoroutine(GameObject obj, Vector3 target, float speed)
    {
        Vector3 startPosition = obj.transform.position;
        float time = 0f;

        while (obj.transform.position != target)
        {
            obj.transform.position = Vector3.Lerp(startPosition, target, (time / Vector3.Distance(startPosition, target)) * speed);
            time += Time.deltaTime;
            yield return null;
        }
    }
}