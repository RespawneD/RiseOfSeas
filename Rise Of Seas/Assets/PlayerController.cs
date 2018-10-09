using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;
    public float runModifier;
    Animator am;

	// Use this for initialization
	void Start () {
        am = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        am.SetFloat("h", h);
        am.SetFloat("v", v);
        float mouseRotate = Input.GetAxis("Mouse X");
        transform.Rotate(0, mouseRotate, 0);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Vector3.Angle(collision.contacts[0].normal, transform.forward) < 20)
        {
            Physics.IgnoreCollision(GetComponent<Collider>(), collision.collider);
        }
    }

}
