using UnityEngine;
using System.Collections;
using UnityEditor;



[CustomEditor(typeof(GhostSprite))]
public class GhostStateEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //GhostSprite myTarget = (GhostSprite)target;
        //myTarget.State =  (GhostState)EditorGUILayout.EnumPopup("State", myTarget.State);
        //EditorGUILayout.EnumPopup("State", myTarget.State);

        base.OnInspectorGUI();
        
    }
}

