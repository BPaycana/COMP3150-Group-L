using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tower : MonoBehaviour
{

    public float interactRange = 1;
    public Transform player;
    private SpriteRenderer spriteRenderer;
    private PlayerInput playerInput;

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
        playerInput = GetComponent<PlayerInput>();

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
                if (playerInput.actions["Interact"].triggered)
                {
                    spriteRenderer.color = Color.blue;
                    towerState = TowerState.Held;
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

                if (playerInput.actions["Drop"].triggered)
                {
                    spriteRenderer.color = Color.green;
                    towerState = TowerState.Close;
                }

                break;
        }
    }
}
