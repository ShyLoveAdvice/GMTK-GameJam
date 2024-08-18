using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;

[CustomEditor(typeof(DraggableObjects))]
[CanEditMultipleObjects]
public class DraggableObjectEditor : Editor
{
    DraggableObjects m_target;
    private void OnEnable()
    {
        m_target = target as DraggableObjects;
    }
    private void OnSceneGUI()
    {
        Vector3 temp;
        EditorGUI.BeginChangeCheck();
        temp = Handles.FreeMoveHandle(m_target.leftTop + (Vector2)m_target.transform.position, .2f, Vector2.zero, Handles.CylinderHandleCap) - m_target.transform.position;
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Bounds");
            m_target.leftTop = temp;
        }
        EditorGUI.BeginChangeCheck();
        temp = Handles.FreeMoveHandle(m_target.rightBottom + (Vector2)m_target.transform.position, .2f, Vector2.zero, Handles.CylinderHandleCap) - m_target.transform.position;
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Bounds");
            m_target.rightBottom = temp;
        }
    }
}
