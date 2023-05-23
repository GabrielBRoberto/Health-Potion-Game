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

    [SerializeField]
    private Sprite defaultSprite;
    [SerializeField]
    private Sprite activatedSprite;

    public override void Active()
    {
        gameObjectToMove.transform.position = Vector3.Lerp(openVector.position, closedVector.position, Time.deltaTime);
    }
    public void Desactivate()
    {
        gameObjectToMove.transform.position = Vector3.Lerp(closedVector.position, openVector.position, Time.deltaTime);
    }
}