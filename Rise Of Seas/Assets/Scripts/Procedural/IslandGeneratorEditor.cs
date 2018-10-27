using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof (IslandGenerator))]
public class IslandGeneratorEditor : Editor {

	public override void OnInspectorGUI() {
        IslandGenerator islandGen = (IslandGenerator)target;

		if (DrawDefaultInspector ()) {
			if (islandGen.autoUpdate) {
                islandGen.GenerateMap ();
                
			}
		}

		if (GUILayout.Button ("Generate")) {
            islandGen.GenerateMap ();
		}
        if (GUILayout.Button("StartAutoGen"))
        {
            if(!islandGen.autoGenerate)
            {
                Debug.Log("hello");
                islandGen.autoGenerate = true;
                islandGen.StartAutoGeneration();

            }
        }
        if (GUILayout.Button("StopAutoGen"))
        {
            islandGen.autoGenerate = false;
            islandGen.StopAutoGeneration();
        }


    }
}
