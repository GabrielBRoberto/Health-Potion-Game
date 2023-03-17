using Dlog.Runtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class NPCDialogue : MonoBehaviour
{
    public DialogueGraph DialogueSystem;
    public LineController LineController;

    [Header("UI References")]
    public GameObject SecondaryScreen;  //This is not required. Will be used only if the NPC has a shop, or something related.
    public GameObject PlayerContainer;
    public GameObject NpcContainer;
    public TMP_Text PlayerText;
    public TMP_Text NpcText;
    public TMP_Text NpcName;    //PlayerName won't be setted here, only in the PlayerContainer;
    public Image NpcImage;      //Icon recived from ActorScriptableObject;

    [SerializeField]
    private bool willStopTime = false;
    private bool isInConversation = false;
    private bool showingSecondaryScreen;
    private bool showPlayer;
    private bool isPlayerChoosing;
    private bool shouldShowText;
    private bool showingText;
    private string textToShow;

    /// <summary>
    /// This is only if you are using the new inputSystem.
    /// If not you can just delete a change for the old inputSystem.
    /// </summary>
    private PlayerControls inputs;
    private InputActionMap dialogueInputs;

    private void Awake()
    {
        inputs = new PlayerControls();
        dialogueInputs = inputs.Dialogue;
    }

    public void CallDialogue()
    {
        if (!isInConversation)
        {
            if (willStopTime)
            {
                Time.timeScale = 0;
            }

            inputs.Dialogue.Enable();

            DialogueSystem.ResetConversation();
            isInConversation = true;
            (showPlayer ? PlayerContainer : NpcContainer).SetActive(true);
        }
    }

    /// <summary>
    /// Don't change anything in the Update for safety.
    /// If you gonna change, maybe will brake the code.
    /// </summary>
    private void Update()
    {
        if (inputs.Dialogue.Interact.triggered)
        {
            CallDialogue();
        }

        if (!isInConversation || isPlayerChoosing)
        {
            return;
        }
        if (shouldShowText)
        {
            (showPlayer ? PlayerContainer : NpcContainer).SetActive(true);
            (showPlayer ? PlayerText : NpcText).gameObject.SetActive(true);
            (showPlayer ? PlayerText : NpcText).text = textToShow;
            showingText = true;
            shouldShowText = false;
        }

        if (showingText)
        {
            if (inputs.Dialogue.Enter.WasPressedThisFrame())
            {
                showingText = false;
                (showPlayer ? PlayerContainer : NpcContainer).SetActive(false);
                (showPlayer ? PlayerText : NpcText).gameObject.SetActive(false);
            }
        }
        else
        {
            if (DialogueSystem.IsConversationDone())
            {
                //Reset state
                isInConversation = false;
                showingSecondaryScreen = false;
                showPlayer = false;
                isPlayerChoosing = false;
                shouldShowText = false;
                showingText = false;

                PlayerContainer.SetActive(false);
                NpcContainer.SetActive(false);

                Time.timeScale = 1;

                inputs.Dialogue.Disable();

                gameObject.SetActive(false);

                return;
            }

            var isNpc = DialogueSystem.IsCurrentNpc();
            if (isNpc)
            {
                var currentActor = DialogueSystem.GetCurrentActor();
                showPlayer = false;
                shouldShowText = true;
                textToShow = DialogueSystem.ProgressNpc();
                NpcName.text = currentActor.Name;
                NpcImage.sprite = currentActor.CustomData.icone;
            }
            else
            {
                var currentLines = DialogueSystem.GetCurrentLines();
                isPlayerChoosing = true;
                PlayerContainer.SetActive(true);
                LineController.gameObject.SetActive(true);
                LineController.Initialize(currentLines);
            }
        }
    }

    /// <summary>
    /// Function that choose the dialogue that the player want.
    /// DON'T CHANGE ANYTHING.
    /// </summary>
    /// <param name="index"></param>
    public void PlayerSelect(int index)
    {
        LineController.gameObject.SetActive(false);
        textToShow = DialogueSystem.ProgressSelf(index);
        isPlayerChoosing = false;
        shouldShowText = true;
        showPlayer = true;
    }

    /// <summary>
    /// Everything below now is only for example in case you use triggers.
    /// </summary>

    /// <summary>
    /// For custom checks and triggers
    /// Example so see if you already visited the NPC.
    /// The function MetBefore is the check.
    /// The function Meet is the trigger.
    /// </summary>
    /// <param name="node"></param>
    /// <param name="lineIndex"></param>
    /// <returns></returns>
    private bool metBefore = false;
    public bool MetBefore(string node, int lineIndex)
    {
        return metBefore;
    }
    public void Meet(string node, int lineIndex)
    {
        metBefore = true;
    }
}
