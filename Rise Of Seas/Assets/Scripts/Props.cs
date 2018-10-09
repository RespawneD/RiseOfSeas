﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Props : Entity {

    public List<GameObject> drops;


	new void Start () {
        base.Start();
	}
	
	new void Update () {
        base.Update();

	}

    public override void OnDead()
    {

        GetComponent<Collider>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = false;

        Destroy(gameObject, 3f);

    }



    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.collider.transform.name);

        Tool t;
        if (t = collision.collider.GetComponent<Axe>())
        {
            TakeDamage(GetComponent<Entity>(), t.damage);
            collision.collider.GetComponent<Collider>().enabled = false;
        }
            
            
    }

}