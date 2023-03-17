using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueActivate : Activate
{
    [SerializeField]
    private GameObject dialogueObject;

    public override void Active()
    {
        dialogueObject.SetActive(true);

        dialogueObject.GetComponent<NPCDialogue>().CallDialogue();

        Destroy(gameObject);
    }
}
