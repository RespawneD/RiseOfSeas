using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Props : Entity {

    public List<GameObject> drops;


	void Start () {
        EntityStart();
	}
	
	void Update () {
        CheckIfDead();

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
        if (t = collision.collider.GetComponent<Tool>())
        {
            ScriptableWeapon w = (ScriptableWeapon)t.data;

            TakeDamage(GetComponent<Entity>(), w.damage);
            collision.collider.GetComponent<Collider>().enabled = false;
        }
            
            
    }

}
