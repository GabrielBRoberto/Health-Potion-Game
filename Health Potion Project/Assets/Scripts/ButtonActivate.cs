using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonActivate : Activate
{
    [SerializeField]
    private GameObject gameObjectToMove;

    [SerializeField]
    private Transform closedVector;
    [SerializeField]
    private Transform openVector;

    public override void Active()
    {
        //gameObjectToMove.transform.position = Vector3.Lerp(openVector.position, closedVector.position, 50f * Time.deltaTime);

        StartCoroutine(Vector3LerpCoroutine(gameObjectToMove, openVector.position, 10f));
    }
    public void Desactivate()
    {
        //gameObjectToMove.transform.position = Vector3.Lerp(closedVector.position, openVector.position, Time.deltaTime);

        StartCoroutine(Vector3LerpCoroutine(gameObjectToMove, closedVector.position, 10f));
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