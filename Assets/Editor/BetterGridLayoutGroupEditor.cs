using System.Collections;
using System.Collections.Generic;
using KefirTest;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BetterGridLayoutGroup))]
public class BetterGridLayoutGroupEditor : Editor
{
    private BetterGridLayoutGroup _grid;

    private void OnEnable()
    {
        _grid = target as BetterGridLayoutGroup;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(serializedObject.FindProperty("_columnCount"), new GUIContent("Column count"));
        GUILayout.Space(10);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("m_Spacing"), new GUIContent("Relative rect spacing"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("m_ChildAlignment"), new GUIContent("Child alignment"));

        serializedObject.ApplyModifiedProperties();
    }
}
