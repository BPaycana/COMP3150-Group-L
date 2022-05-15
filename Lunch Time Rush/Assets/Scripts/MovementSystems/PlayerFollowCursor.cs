using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFollowCursor : MonoBehaviour
{

    private GameManager gameManager;
    private PlayerInput input;
    public float moveSpeed = 5;
    private Vector3 direction;
    private bool touching;
    private Rigidbody2D rb;
    //private float direction;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        input = gameManager.gameObject.GetComponent<PlayerInput>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        touching = false;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (input.actions["PrimaryContact"].ReadValue<float>() == 1)
        {
            Vector3 pos = input.actions["PrimaryPosition"].ReadValue<Vector2>();
            pos = Camera.main.ScreenToWorldPoint(pos);
            pos.z = 0;
            direction = pos - transform.position;
            transform.Translate(direction.normalized * moveSpeed * Time.deltaTime);
        }
        */

        if (input.actions["PrimaryContact"].ReadValue<float>() == 1)
        {
            if (input.actions["Hold"].triggered)
            {
                touching = true;
            }
            if (touching)
            {
                Vector3 pos = input.actions["PrimaryPosition"].ReadValue<Vector2>();
                pos = Camera.main.ScreenToWorldPoint(pos);
                pos.z = 0;
                
                //only move if we are some distance from mouse to prevent jitter at mouse pos
                direction = pos - transform.position;
                    
                //transform.Translate(direction.normalized * moveSpeed * Time.deltaTime);
            }           
        }
        else
        {
            touching = false;
            direction = new Vector3(0,0,0);
        }
    }

    private void FixedUpdate()
    {
        if (direction.x > .05f || direction.y > .05f || direction.x < -.05f || direction.y < -.05f)
        {
            rb.velocity = new Vector2(direction.normalized.x, direction.normalized.y) * moveSpeed * Time.deltaTime; //this makes player not clip through walls/jitter around
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
