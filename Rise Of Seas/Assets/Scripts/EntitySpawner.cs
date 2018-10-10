using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawner : MonoBehaviour {


    public GameObject entity;

    public float speed;
    public float time;

    private float t;

	void Update () {
        if (t < 0)
        {
            Instantiate(entity, transform.position, Quaternion.identity);
            t = time;
        }
           
        else
            t -= Time.deltaTime * speed;


	}
}
