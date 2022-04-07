using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeltaMovement : MonoBehaviour
{

    public float moveSpeed = 5;
    public float deltaMag = 5;
    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.deltaPosition.magnitude > deltaMag)
            {
                direction = touch.deltaPosition;
                direction = Vector3.ClampMagnitude(direction, 1);
            }

            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }
        else
        {
            direction = Vector3.zero;
        }
    }
}
