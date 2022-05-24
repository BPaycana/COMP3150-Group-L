using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private GameManager gameManager;
    private PlayerInput input;
    public float moveSpeed = 5;
    private Vector3 direction;
    public float changeReq = 5;
    private bool touching;
    public Image joystick;
    public Image joystickRing;
    public Animator animator;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        input = gameManager.gameObject.GetComponent<PlayerInput>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        
        direction = new Vector3(0, 0, 0);
        touching = false;
    }

    // Update is called once per frame
    void Update()
    {

        //Vector2 movement = input.actions["Move"].ReadValue<Vector2>();
        //Debug.Log(movement);
        //if (input.actions["Held"].ReadValue<float>() > 0)
        if (input.actions["PrimaryContact"].ReadValue<float>() == 1)
        {
            /*
            Vector2 pos = input.actions["PrimaryPosition"].ReadValue<Vector2>();
            //Vector2 del = input.actions["Delta"].ReadValue<Vector2>();
            Vector2 startPos = input.actions["StartPosition"].ReadValue<Vector2>();
            Vector3 worldStartPos = Camera.main.ScreenToWorldPoint(startPos);
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(pos);
            worldPos.z = 0;
            //Debug.Log(worldPos + " | " + transform.position + " | "  +
            //    worldStartPos);
            //transform.position = worldPos;
            direction = (worldPos - worldStartPos);
            
            //direction = Vector3.ClampMagnitude(direction, 1);
            //Debug.Log(direction.magnitude);
            transform.Translate(direction.normalized * moveSpeed * Time.deltaTime);
            */
            if (input.actions["Hold"].triggered)
            {
                touching = true;
            }
            if (touching)
            {

                Vector2 pos = input.actions["PrimaryPosition"].ReadValue<Vector2>();

                Vector2 startPos = input.actions["StartPosition"].ReadValue<Vector2>();
                Vector3 worldStartPos = Camera.main.ScreenToWorldPoint(startPos);
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(pos);
                worldPos.z = 0;
                worldStartPos.z = 0;

                direction = (worldPos - worldStartPos);
                direction = Vector3.ClampMagnitude(direction, 1);
                //transform.Translate(direction * moveSpeed * Time.deltaTime);
                //Debug.Log(direction.normalized.magnitude);

                joystickRing.transform.position = worldStartPos;
                joystick.transform.position = worldStartPos + (direction / 2);
                joystickRing.enabled = true;
                joystick.enabled = true;

            }

        }
        else
        {
            direction = Vector3.zero;
            touching = false;
            joystick.enabled = false;
            joystickRing.enabled = false;
        }

        if (gameManager.canRestock == true)
        {
            animator.SetBool("HoldingBox", true);
        }

        if (gameManager.canRestock == false)
        {
            animator.SetBool("HoldingBox", false);
        }


        //transform.Translate(movement * moveSpeed * Time.deltaTime);


        //if (input.actions["PrimaryContact"].ReadValue<float>() > 0)
        //{
        /*
        if (input.actions["Delta"].ReadValue<Vector2>().magnitude 
            > changeReq)
        {
            direction = input.actions["Delta"].ReadValue<Vector2>();

            direction = Vector2.ClampMagnitude(direction, 1);

        }
        transform.Translate(direction * moveSpeed * Time.deltaTime);
        Debug.Log(direction.magnitude);
        */
        /*
        if (input.actions["CurrentPosition"].ReadValue<Vector2>().magnitude !=
            input.actions["StartPosition"].ReadValue<Vector2>().magnitude)
        {
            float dir = Vector2.Distance(input.actions["CurrentPosition"].ReadValue<Vector2>(),
                input.actions["StartPosition"].ReadValue<Vector2>());
        }
        */
        //}

        /*
        if (input.actions["PrimaryContact"].triggered)
        {
            Debug.Log("hello");
        }
        */
    }

    private void FixedUpdate()
    {
        if (direction.x > .05f || direction.y > .05f || direction.x < -.05f || direction.y < -.05f)
        {
            rb.velocity = new Vector2(direction.x, direction.y) * moveSpeed * Time.deltaTime; //this makes player not clip through walls/jitter around
        }
        else
        {
            rb.velocity = Vector3.zero;
        }

        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
        animator.SetFloat("Speed", direction.sqrMagnitude);

    }
}