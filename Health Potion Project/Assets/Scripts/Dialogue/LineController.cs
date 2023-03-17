using System.Collections.Generic;
using UnityEngine.InputSystem;
using System.Collections;
using Dlog.Runtime;
using UnityEngine;
using System;

public class LineController : MonoBehaviour {
    public LineEntry Prefab;
    public NPCDialogue Owner;
    private List<LineEntry> entries;
    private int selectedIndex;
    private bool isActive;

    PlayerControls inputs;
    InputAction spaceAction;
    InputAction setaUpAction;
    InputAction setaDownAction;

    private void Awake()
    {
        inputs = new PlayerControls();
        //spaceAction = inputs.Player.InstanciasSpace;
        //setaUpAction = inputs.Player.SetaUp;
        //setaDownAction = inputs.Player.SetaDown;
    }

    private void Start()
    {
        spaceAction.Enable();
        setaUpAction.Enable();
        setaDownAction.Enable();
    }

    public void Clear() 
    {
        entries.ForEach(entry => Destroy(entry.gameObject));
        entries.Clear();
    }
    
    public void Initialize(List<ConversationLine> lines)
    {
        if (lines.Count == 1)
        {
            SelectLine(0);
        }

        isActive = true;
        entries = new List<LineEntry>();
        for (var i = 0; i < lines.Count; i++)
        {
            var line = lines[i];
            var entry = Instantiate(Prefab, transform);
            entry.Initialize(line.Message);
            entries.Add(entry);
        }

        selectedIndex = 0;
        entries[0].Select(true);
    }

    public void SelectLine(int index)
    {
        Clear();
        isActive = false;
        selectedIndex = -1;
        Owner.PlayerSelect(index);
    }

    private void Update()
    {
        if (!isActive) 
        {
            return;
        }
        if (setaDownAction.WasPressedThisFrame())
        {
            var nextIndex = Mathf.Min(selectedIndex + 1, entries.Count - 1);
            entries[selectedIndex].Select(false);
            entries[nextIndex].Select(true);
            selectedIndex = nextIndex;
        }
        else if (setaUpAction.WasPressedThisFrame())
        {
            var nextIndex = Mathf.Max(selectedIndex - 1, 0);
            entries[selectedIndex].Select(false);
            entries[nextIndex].Select(true);
            selectedIndex = nextIndex;
        }

        if (spaceAction.WasPressedThisFrame())
        {
            SelectLine(selectedIndex);
        }

        
    }
}
