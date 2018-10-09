using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;
    public float runModifier;
    Animator am;

    private float turnSmoothVelocity;
    public float turnSmoothTime = .2f;
    public float speedSmooth = 5f;

    Transform cameraT;

	// Use this for initialization
	void Start () {
        am = GetComponent<Animator>();
        cameraT = Camera.main.transform;
	}

    // Update is called once per frame
    void Update() {

        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 inputDir = input.normalized;

        if (inputDir != Vector2.zero)
        {
            float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);

        }
        am.SetFloat("v", inputDir.magnitude, speedSmooth, Time.deltaTime);

        am.SetBool("isJumping", Input.GetButtonDown("Jump"));

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;

        am.SetBool("isAttacking", Input.GetMouseButton(0));


        

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Vector3.Angle(collision.contacts[0].normal, transform.forward) < 20)
        {
            Physics.IgnoreCollision(GetComponent<Collider>(), collision.collider);
        }
    }

}
