using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateMovePlatform : Activate
{
    [SerializeField]
    private GameObject platform, A, B;

    public override void Active()
    {
        GetComponent<AudioSource>().Play();

        if (platform.transform.position == A.transform.position)
        {
            StartCoroutine(Vector3LerpCoroutine(platform, B.transform.position, 5f));
        }
        if (platform.transform.position == B.transform.position)
        {
            StartCoroutine(Vector3LerpCoroutine(platform, A.transform.position, 5f));
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