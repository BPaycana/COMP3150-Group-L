using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JoystickControl : MonoBehaviour
{
    private PlayerInput input;
    public float moveSpeed = 5;

    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = input.actions["Move"].ReadValue<Vector2>();
        //movement = Camera.main.ScreenToWorldPoint(movement);
        //movement.z = 0;
        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }
}
