using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallonTrigger : MonoBehaviour
{
    private bool trig = false;

    [SerializeField] private GameObject textGameObject;

    private void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        textGameObject.SetActive(false);
    }
    private void Update()
    {
        if (trig)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            textGameObject.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            trig = true;
        }
    }
}
