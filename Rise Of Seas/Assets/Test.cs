using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Debug.Log(GetComponent<MeshFilter>().mesh.bounds.extents);
       

        BoxCollider b = gameObject.AddComponent<BoxCollider>();
        b.size = 2 * GetComponent<MeshFilter>().mesh.bounds.extents;

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
