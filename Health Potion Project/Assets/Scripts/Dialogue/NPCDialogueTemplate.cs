using UnityEngine.InputSystem;
using UnityEngine.UI;
using Dlog.Runtime;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(DialogueGraph))]
public class NPCDialogueTemplate : MonoBehaviour
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
    InputAction spaceAction;
    InputAction interactionAction;

    private void Awake()
    {
        inputs = new PlayerControls();
        interactionAction = inputs.Player1.Interact;
        spaceAction = inputs.Player1.Interact;
    }
    private void Start()
    {
        interactionAction.Enable();
        spaceAction.Enable();
    }

    /// <summary>
    /// Don't change anything in the Update for safety.
    /// If you gonna change, maybe will brake the code.
    /// </summary>
    private void Update()
    {
        if (interactionAction.triggered && !isInConversation)
        {
            if (willStopTime)
            {
                Time.timeScale = 0;
            }

            DialogueSystem.ResetConversation();
            isInConversation = true;
            (showPlayer ? PlayerContainer : NpcContainer).SetActive(true);
        }

        // You can delete this IF if SecondaryScreen is null.
        if (showingSecondaryScreen)
        {
            if (spaceAction.WasPressedThisFrame())
            {
                showingSecondaryScreen = false;
                SecondaryScreen.SetActive(false);
            }
            return;
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
            if (spaceAction.WasPressedThisFrame())
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
                showingText=false;

                PlayerContainer.SetActive(false);
                NpcContainer.SetActive(false);

                Time.timeScale = 1;
                gameObject.SetActive(false);

                return;
            }

            var isNpc = DialogueSystem.IsCurrentNpc();
            if (isNpc)
            {
                var currentActor = DialogueSystem.GetCurrentActor();
                showPlayer = false;
                shouldShowText=true;
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
