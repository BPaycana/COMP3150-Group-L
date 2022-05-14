using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class FollowMouse : MonoBehaviour
{

    public GameObject joystick;
    private GameManager gameManager;
    private PlayerInput input;
    public Image image;
    private Vector3 lastPos;

    // Start is called before the first frame update
    void Start()
    {
        //image.enabled = false;
        gameManager = FindObjectOfType<GameManager>();
        input = gameManager.gameObject.GetComponent<PlayerInput>();
        lastPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        Debug.Log(input.actions["PrimaryContact"].ReadValue<float>());
        if(input.actions["PrimaryContact"].ReadValue<float>() > 0)
        {
            if(image.enabled == false)
            {
                image.enabled = true;
                Vector3 pos = input.actions["PrimaryPosition"].ReadValue<Vector2>();
                pos = Camera.main.ScreenToWorldPoint(pos);
                pos.z = 0;
                joystick.transform.position = pos;
                lastPos = pos;
                Debug.Log("ah");
            }
            //image.enabled = false;
            joystick.transform.position = lastPos;
            Debug.Log("Test");
        }
        else
        {
            image.enabled = false;
            
            Vector3 pos = input.actions["PrimaryPosition"].ReadValue<Vector2>();
            pos = Camera.main.ScreenToWorldPoint(pos);
            pos.z = 0;
            joystick.transform.position = pos;
            lastPos = pos;
            
            Debug.Log("hello");
            //image.enabled = true;
        }
    */
        //MousePosition
        //if (input.actions["PrimaryContact"].ReadValue<float>() < 1) // OH IT'S BECAUSE IT'S UNDER ZERO SO TOUCH ONLY ON RELEASE
        if (input.actions["PrimaryContact"].ReadValue<float>() > 0)
        {
            Vector3 pos = Input.mousePosition;
            pos = Camera.main.ScreenToWorldPoint(pos);
            pos.z = 0;
            joystick.transform.position = pos;
        }
        if (input.actions["Hold"].triggered)
        {
            Debug.Log("Mwhaha");
        }
        

        /*
        if (input.actions["PrimaryContact"].ReadValue<float>() < 1)
        {
            Vector3 pos = input.actions["PrimaryPosition"].ReadValue<Vector2>();
            pos = Camera.main.ScreenToWorldPoint(pos);
            pos.z = 0;
            joystick.transform.position = pos;
        }
        
        if (input.actions["Hold"].triggered)
        {
            Debug.Log("Mwhaha");
            Vector3 pos = input.actions["PrimaryPosition"].ReadValue<Vector2>();
            pos = Camera.main.ScreenToWorldPoint(pos);
            pos.z = 0;
            joystick.transform.position = pos;
        }
        */
    }
        
}
