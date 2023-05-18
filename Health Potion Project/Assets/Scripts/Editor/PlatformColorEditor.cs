using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ActivateColorPlatform))]
public class PlatformColorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ActivateColorPlatform script = (ActivateColorPlatform)target;

        script.platform = (GameObject)EditorGUILayout.ObjectField(script.platform, typeof(GameObject), true);
        script.platformActivated = (Sprite)EditorGUILayout.ObjectField(script.platformActivated, typeof(Sprite), false);
        script.platformDesactivated = (Sprite)EditorGUILayout.ObjectField(script.platformDesactivated, typeof(Sprite), false);

        if (GUILayout.Button("Toggle"))
        {
            script.activated = !script.activated;
        }
    }
}
