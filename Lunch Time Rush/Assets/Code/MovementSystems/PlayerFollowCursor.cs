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
    //private float direction;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        input = gameManager.gameObject.GetComponent<PlayerInput>();
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
                direction = pos - transform.position;
                transform.Translate(direction.normalized * moveSpeed * Time.deltaTime);
            }           
        }
        else
        {
            touching = false;
        }
    }
}
