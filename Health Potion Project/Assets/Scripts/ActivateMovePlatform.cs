using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateMovePlatform : Activate
{
    [SerializeField]
    private GameObject platform, A, B;
    [SerializeField]
    private GameObject platform2, A2, B2;
    [SerializeField]
    private GameObject platform3, A3, B3;

    [SerializeField]
    private bool multiplePlatforms;

    public override void Active()
    {
        GetComponent<AudioSource>().Play();

        if (!multiplePlatforms)
        {
            if (platform.transform.position == A.transform.position)
            {
                StartCoroutine(Vector3LerpCoroutine(platform, B.transform.position, 5f));
            }
            if (platform.transform.position == B.transform.position)
            {
                StartCoroutine(Vector3LerpCoroutine(platform, A.transform.position, 5f));
            }
        }

        if (multiplePlatforms)
        {
            if (platform.transform.position == A.transform.position)
            {
                StartCoroutine(Vector3LerpCoroutine(platform, B.transform.position, 5f));
            }
            if (platform.transform.position == B.transform.position)
            {
                StartCoroutine(Vector3LerpCoroutine(platform, A.transform.position, 5f));
            }
            if (platform2.transform.position == A2.transform.position)
            {
                StartCoroutine(Vector3LerpCoroutine(platform2, B2.transform.position, 5f));
            }
            if (platform2.transform.position == B2.transform.position)
            {
                StartCoroutine(Vector3LerpCoroutine(platform2, A2.transform.position, 5f));
            }

            if (platform3 != null && A3 != null && B3 != null)
            {
                if (platform3.transform.position == A3.transform.position)
                {
                    StartCoroutine(Vector3LerpCoroutine(platform3, B3.transform.position, 5f));
                }
                if (platform3.transform.position == B3.transform.position)
                {
                    StartCoroutine(Vector3LerpCoroutine(platform3, A3.transform.position, 5f));
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