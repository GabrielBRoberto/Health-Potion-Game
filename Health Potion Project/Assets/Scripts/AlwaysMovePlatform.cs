using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysMovePlatform : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private GameObject A, B;

    // Update is called once per frame
    void Update()
    {
        if (transform.position == A.transform.position)
        {
            StartCoroutine(Vector3LerpCoroutine(gameObject, B.transform.position, speed));
        }
        if (transform.position == B.transform.position)
        {
            StartCoroutine(Vector3LerpCoroutine(gameObject, A.transform.position, speed));
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
