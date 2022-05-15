using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TowerMove : MonoBehaviour
{
    public float interactRange = 1;
    public Transform player;
    public int maxTowerAmmo = 5;
    private SpriteRenderer spriteRenderer;
    private GameManager gameManager;
    private PlayerInput input;
    private Tower tower;
    private int towerAmmo;
    private bool tooClose;
    private bool canPickUp;



    enum TowerState
    {
        Close,
        Far,
        Held,

    };

    private TowerState towerState;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        input = gameManager.gameObject.GetComponent<PlayerInput>();
        tower = this.gameObject.GetComponent<Tower>();
        towerState = TowerState.Far;
        spriteRenderer = GetComponent<SpriteRenderer>();
        //spriteRenderer.color = Color.yellow;
        tooClose = false;
        canPickUp = false;

        towerAmmo = maxTowerAmmo;
    }

    // Update is called once per frame
    void Update()
    {

        float dist = Vector3.Distance(player.position, transform.position);
        //Debug.Log(dist);
        switch (towerState)
        {
            case TowerState.Close:

                /*
                if (Mathf.Abs(dist) > interactRange)
                {
                    spriteRenderer.color = tower.getColor();
                    towerState = TowerState.Far;
                }
                */
                if (canPickUp == false)
                {
                    spriteRenderer.color = tower.getColor();
                    towerState = TowerState.Far;
                }

                if (input.actions["Interact"].triggered)
                {
                    if (tower.getAmmo() == 0)
                    {
                        if (gameManager.GetComponent<GameManager>().restockState())
                        {
                            Debug.Log("player restocking tower");
                            if (tower.refillAmmo(maxTowerAmmo))
                            {
                                gameManager.restock();
                            }

                            spriteRenderer.color = Color.red;
                        }
                    }
                    else if(!gameManager.GetComponent<GameManager>().getTowerHeld())
                    {
                        /*
                        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                        RaycastHit2D hit2D = Physics2D.GetRayIntersection(ray);
                        if (hit2D.collider != null)
                        {
                            if (hit2D.transform.gameObject == gameObject &&
                                !gameManager.GetComponent<GameManager>().getTowerHeld())
                            {
                                gameManager.towerHoldBool();
                                spriteRenderer.color = Color.blue;
                                //Debug.Log(gameManager.getTowerHeld());
                                towerState = TowerState.Held;
                                tower.held = true;
                            }
                        }
                        */
                        gameManager.towerHoldBool();
                        spriteRenderer.color = Color.blue;
                        //Debug.Log(gameManager.getTowerHeld());
                        //gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
                        towerState = TowerState.Held;
                        tower.held = true;
                    }
                }

                /*
                if (input.actions["Interact"].triggered && tower.getAmmo() == 0)
                {
                        if (gameManager.GetComponent<GameManager>().restockState())
                        {
                            Debug.Log("player restocking tower");
                        if (tower.refillAmmo(maxTowerAmmo))
                        {
                            gameManager.restock();
                        }
                            
                            spriteRenderer.color = Color.red;
                        }
                }
                */

                break;

            case TowerState.Far:
                /*
                if (Mathf.Abs(dist) <= interactRange)
                {
                    spriteRenderer.color = Color.green;
                    towerState = TowerState.Close;
                }
                */
                if (canPickUp == true)
                {
                    spriteRenderer.color = Color.green;
                    towerState = TowerState.Close;
                }

                break;

            case TowerState.Held:

                //Physics.IgnoreCollision(player.GetComponent<Collider>(), GetComponent<Collider>());
                transform.position = new Vector3(player.position.x - 0.1f, player.position.y);
                if (input.actions["drop"].triggered)
                {
                    
                    if(tooClose == false)
                    {
                        gameManager.towerHoldBool();
                        spriteRenderer.color = Color.green;
                        //transform.position = player.position;
                        //gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
                        towerState = TowerState.Close;
                        tower.held = false;
                    }
                    
                }
                Debug.Log(gameManager.getTowerHeld());
                break;
        }

        //spriteRenderer.color = towerColor;

        if (towerAmmo == 0)
        {
            spriteRenderer.color = Color.white;
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == 6)
        {
            tooClose = true;
        }
        if(other.gameObject.layer == 7)
        {
            canPickUp = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == 6)
        {
            tooClose = false;
        }
        if (other.gameObject.layer == 7)
        {
            canPickUp = false;
        }
    }

}
