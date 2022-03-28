using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    private PlayerInput input;
    public float moveSpeed = 5;
    private Vector2 direction;
    public float changeReq = 5;

    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<PlayerInput>();
        direction = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        Vector2 movement = input.actions["Move"].ReadValue<Vector2>();
        //Debug.Log(movement);
        if (input.actions["Held"].ReadValue<float>() > 0)
        {
            Vector2 pos = input.actions["Position"].ReadValue<Vector2>();
            Vector2 del = input.actions["Delta"].ReadValue<Vector2>();
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(pos);
            worldPos.z = 0;
            Debug.Log(worldPos + " | " + transform.position + " | "  + 
                del);
            //transform.position = worldPos;
            transform.Translate(del * moveSpeed * Time.deltaTime);
        }
        */

        //transform.Translate(movement * moveSpeed * Time.deltaTime);

        if(input.actions["PrimaryContact"].ReadValue<float>() > 0)
        {
            
            if (input.actions["Delta"].ReadValue<Vector2>().magnitude 
                > changeReq)
            {
                direction = input.actions["Delta"].ReadValue<Vector2>();
                
                direction = Vector2.ClampMagnitude(direction, 1);
                              
            }

            transform.Translate(direction * moveSpeed * Time.deltaTime);
            Debug.Log(direction.magnitude);
        }
        else
        {
            direction = Vector2.zero;
        }
    }


}
