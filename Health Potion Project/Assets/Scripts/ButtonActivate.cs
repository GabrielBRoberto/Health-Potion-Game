using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonActivate : Activate
{
    [SerializeField]
    private GameObject obstacleGameObject;

    [SerializeField]
    private Vector3 closedVector;
    [SerializeField]
    private Vector3 openVector;

    public override void Active()
    {
        obstacleGameObject.transform.position = Vector3.Lerp(openVector, closedVector, Time.deltaTime);
    }
    public void Desactivate()
    {
        obstacleGameObject.transform.position = Vector3.Lerp(closedVector, openVector, Time.deltaTime);
    }
}