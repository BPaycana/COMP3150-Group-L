using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tower : MonoBehaviour
{
    public float interactRange = 1;
    public Transform player;
    private SpriteRenderer spriteRenderer;
    private PlayerInput input;

    enum TowerState
    {
        Close,
        Far,
        Held
    };

    private TowerState towerState;

    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<PlayerInput>();

        towerState = TowerState.Far;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.yellow;
    }

    // Update is called once per frame
    void Update()
    {

        float dist = Vector3.Distance(player.position, transform.position);

        switch (towerState)
        {
            case TowerState.Close:

                if(Mathf.Abs(dist) > interactRange)
                {
                    spriteRenderer.color = Color.yellow;
                    towerState = TowerState.Far;
                }

                //if (Input.GetMouseButtonDown(0))
                if (input.actions["PrimaryContact"].ReadValue<float>() == 1)
                {
                    Debug.Log("hello");
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit2D hit2D = Physics2D.GetRayIntersection(ray);
                    if(hit2D.transform.gameObject == gameObject)
                    {
                        Debug.Log(hit2D.transform.gameObject);
                        spriteRenderer.color = Color.blue;
                        towerState = TowerState.Held;
                    }
                }                

                break;

            case TowerState.Far:

                if (Mathf.Abs(dist) <= interactRange)
                {
                    spriteRenderer.color = Color.green;
                    towerState = TowerState.Close;
                }

                break;

            case TowerState.Held:

                transform.position = player.position;
                
                if (Input.GetMouseButtonDown(0))
                {
                    spriteRenderer.color = Color.green;
                    towerState = TowerState.Close;
                }
                
                break;
        }
    }
}
