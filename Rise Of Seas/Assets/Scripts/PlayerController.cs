using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField] private GameObject skeleton;

    private Player player;

    public float speed;
    public float runModifier;
    public float turnSmoothTime = .2f;
    public float speedSmooth = 5f;

    private Transform cameraT;
    private TransformsShortcut ts;
    private Animator am;
    private float turnSmoothVelocity;

    private bool inMenu;

    void Start ()
    {
        am = GetComponent<Animator>();
        cameraT = Camera.main.transform;
        ts = GetComponent<TransformsShortcut>();
        player = GetComponent<Player>();
	}

    void Update()
    {


        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 inputDir = input.normalized;
        

        // Inventory
        if (Input.GetButtonDown("Inventory"))
        {
            inMenu = !inMenu;
            GetComponentInChildren<UIManager>().inventory.SetActive(inMenu);
        }

        Camera.main.GetComponent<ThirdPersonCamera>().enabled = !inMenu;
        Cursor.visible = inMenu;

        if (inMenu)
        {
            Cursor.lockState = CursorLockMode.None;
            am.SetFloat("v", 0);
            return;
        }

        Cursor.lockState = CursorLockMode.Confined;

        // CHEAT KEYS

        if (Input.GetKeyDown(KeyCode.P))
            Instantiate(skeleton, transform.position + Vector3.forward * 2, Quaternion.identity);


        // Move Around
        if (inputDir != Vector2.zero)
        {
            float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);

        }

        // Animations

        am.SetFloat("v", inputDir.magnitude, speedSmooth, Time.deltaTime);
        am.SetBool("isJumping", Input.GetButtonDown("Jump"));

        am.SetBool("drawSword", Input.GetMouseButtonDown(0) && (ts.GetItem("Weapon_R").childCount == 0));
        am.SetBool("isAttacking", Input.GetMouseButtonDown(0) && (ts.GetItem("Weapon_R").childCount > 0));
        am.SetBool("sheatSword", Input.GetKeyDown(KeyCode.C) && (ts.GetItem("Sabre").childCount == 0));



        // Dances

        if (Input.GetKey(KeyCode.Alpha1))
            am.SetInteger("dance", 1);
        else
        
        if(Input.GetKey(KeyCode.Alpha2))
            am.SetInteger("dance", 2);
        else
            am.SetInteger("dance", -1);

        
            

        // GrabItem

        if (player.selectedItem != null && Input.GetButtonDown("Grab"))
        {
            if (GetComponent<Container>().AddInInventory(player.selectedItem.GetComponent<Item>()))
            {
                Destroy(player.selectedItem);
                player.selectedItem = null;
            }
            else
                Debug.Log("Inventory Full !");
        }


    }

}
