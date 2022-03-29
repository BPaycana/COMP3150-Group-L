using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class FollowMouse : MonoBehaviour
{

    public GameObject joystick;
    private PlayerInput input;
    private Image image;
    private Vector3 lastPos;

    // Start is called before the first frame update
    void Start()
    {
        image = joystick.GetComponentInChildren<Image>();
        //image.enabled = false;
        input = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(input.actions["PrimaryContact"].ReadValue<float>() > 0)
        {

            //image.enabled = false;
            //joystick.transform.position = lastPos;
            Debug.Log("Test");
        }
        else
        {
            Vector3 pos = Input.mousePosition;
            pos = Camera.main.ScreenToWorldPoint(pos);
            pos.z = 0;
            joystick.transform.position = pos;
            lastPos = pos;
            //Debug.Log("hello");
            //image.enabled = true;
        }
    }
}
