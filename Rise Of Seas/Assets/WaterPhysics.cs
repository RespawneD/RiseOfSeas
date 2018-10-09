using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPhysics : MonoBehaviour {

    [SerializeField]
    Transform target;

    public float waveSize;
    public float waveSpeed;
    float t;



    void Start () {
        t = 0;
	}

    
	void Update () {

        if (t * waveSpeed > 2 * Mathf.PI)
            t = 0;
        else
            t += Time.deltaTime;

        transform.rotation *= Quaternion.Euler(waveSize * 0.05f * Mathf.Cos(waveSpeed * t), 0, waveSize * Mathf.Sin(waveSpeed * t));

	}
}
