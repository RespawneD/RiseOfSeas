using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralHelper : MonoBehaviour {

	public Mesh GenerateMesh()
    {
        Mesh m = new Mesh();
        m.vertices = new Vector3[3];
        m.triangles = new int[3];


        // make changes to the Mesh by creating arrays which contain the new values
        m.vertices = new Vector3[] {
            new Vector3(0, 0, 0),
            new Vector3(0, 1, 0),
            new Vector3(1, 0, 0),
            new Vector3(1, 1, 0),
            new Vector3(1, 0, 1),
            new Vector3(1, 1, 1) };
        //m.uv = new Vector2[] { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1) };

        m.triangles = new int[] {
            0, 1, 2,
            3, 2, 1,
            1, 3, 4,
            5, 4, 3 };

        m.RecalculateBounds();
        m.RecalculateNormals();

        return m;
    }

    public void Start()
    {
        MeshFilter m = gameObject.GetComponent<MeshFilter>();
        m.sharedMesh = GenerateMesh();
    }

}
