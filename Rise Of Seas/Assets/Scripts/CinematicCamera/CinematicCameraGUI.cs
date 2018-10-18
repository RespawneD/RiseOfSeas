using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CinematicCamera))]
[CanEditMultipleObjects]
public class CinematicCameraGUI : Editor
{

    CinematicCamera ccam;

    private void OnEnable()
    {

        ccam = (CinematicCamera)target;

        if (ccam.keys == null)
            ccam.keys = new List<CinematicCamera.Key>();

        
            

    }
    

    public override void OnInspectorGUI()
    {
        

        DrawDefaultInspector();
       
        if (GUILayout.Button("Save KeyFrame"))
        { 
            CinematicCamera.Key key = new CinematicCamera.Key(ccam.transform.position, ccam.transform.rotation, .5f, .5f);
            ccam.keys.Add(key);
            EditorUtility.SetDirty(target);
            Debug.Log("Key Added [" + ccam.keys.Count.ToString() + "] : " + key.ToString());
        }

        EditorGUILayout.LabelField("Keys Count : ", ccam.keys.Count.ToString());

        if (GUILayout.Button("Clear"))
        {
            ccam.keys.Clear();
            EditorUtility.SetDirty(target);

        }

    }
}