using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private GameManager gameManager;
    private PlayerInput input;
    public float moveSpeed = 5;
    private Vector2 direction;
    public float changeReq = 5;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        input = gameManager.gameObject.GetComponent<PlayerInput>();
        direction = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {

        //Vector2 movement = input.actions["Move"].ReadValue<Vector2>();
        //Debug.Log(movement);
        //if (input.actions["Held"].ReadValue<float>() > 0)
        if (input.actions["PrimaryContact"].ReadValue<float>() > 0)
        {
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
            
            direction = Vector3.ClampMagnitude(direction, 1);
            Debug.Log(direction.magnitude);
            transform.Translate(direction * moveSpeed * Time.deltaTime);
            
            
        }
        else
        {
            direction = Vector2.zero;
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


}
