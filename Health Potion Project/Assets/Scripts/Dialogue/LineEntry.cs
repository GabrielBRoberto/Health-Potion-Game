using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LineEntry : MonoBehaviour {
    public TMP_Text Text;
    public Image Selection;

    public void Initialize(string text) {
        Text.text = text;
        Select(false);
    }

    public void Select(bool selected) {
        if (!selected)
        {
            Selection.enabled = false;
        }
        else
        {
            Selection.enabled = true;
        }
    }
}
