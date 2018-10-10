using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;
    public float runModifier;
    public float turnSmoothTime = .2f;
    public float speedSmooth = 5f;

    private Transform cameraT;
    private TransformsShortcut ts;
    private Animator am;
    private float turnSmoothVelocity;

    void Start ()
    {
        am = GetComponent<Animator>();
        cameraT = Camera.main.transform;
        ts = GetComponent<TransformsShortcut>();
	}

    void Update()
    {

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


        am.SetBool("drawSword", Input.GetMouseButtonDown(0) && (ts.GetItem("Weapon_R").childCount == 0));
        am.SetBool("isAttacking", Input.GetMouseButtonDown(0) && (ts.GetItem("Weapon_R").childCount > 0));

        am.SetBool("sheatSword", Input.GetKeyDown(KeyCode.C) && (ts.GetItem("Sabre").childCount == 0));





        if (Input.GetKey(KeyCode.Alpha1))
            am.SetInteger("dance", 1);
        else
        
        if(Input.GetKey(KeyCode.Alpha2))
            am.SetInteger("dance", 2);
        else
            am.SetInteger("dance", -1);




    }

}
