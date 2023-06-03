using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ActivateColorPlatform))]
public class PlatformColorEditor : Editor
{
    private SerializedProperty m_PlatformColor;

    private void OnEnable()
    {
        m_PlatformColor = serializedObject.FindProperty("platforms");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(m_PlatformColor, true);

        ActivateColorPlatform script = (ActivateColorPlatform)target;

        if (GUILayout.Button("Toggle"))
        {
            script.Active();
        }

        serializedObject.ApplyModifiedProperties();
    }
}
