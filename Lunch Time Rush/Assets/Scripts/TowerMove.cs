using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TowerMove : MonoBehaviour
{
    public float interactRange = 1;
    public Transform player;
    public int maxTowerAmmo = 5;
    public SpriteRenderer outline;
    public SpriteRenderer cantPlaceCross;
    public SpriteRenderer areaOfEffect;
    private SpriteRenderer spriteRenderer;
    private GameManager gameManager;
    private PlayerInput input;
    private Tower tower;
    private Canvas towerAmmoCanvas;
    private int towerAmmo;
    private bool tooClose;
    private bool canPickUp;
    private Vector3 lastPos;
    private Vector3 deltaPos;


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
        towerAmmoCanvas = this.gameObject.GetComponentInChildren<Canvas>();
        //spriteRenderer.color = Color.yellow;
        tooClose = false;
        canPickUp = false;
        outline.enabled = false;
        areaOfEffect.enabled = false;
        cantPlaceCross.enabled = false;
        towerAmmo = maxTowerAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        areaOfEffect.enabled = false;
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
                    outline.enabled = false;
                    towerState = TowerState.Far;
                }

                if (input.actions["Interact"].triggered)
                {
                    if (gameManager.GetComponent<GameManager>().restockState()
                        && tower.getAmmo() < maxTowerAmmo)
                    {
                        Debug.Log("player restocking tower");
                        if (tower.refillAmmo(maxTowerAmmo))
                        {
                            gameManager.restock();
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
                        //spriteRenderer.color = Color.blue;
                        outline.enabled = false;
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
                    //spriteRenderer.color = Color.green;
                    outline.enabled = true;
                    towerState = TowerState.Close;
                }

                break;

            case TowerState.Held:

                //Physics.IgnoreCollision(player.GetComponent<Collider>(), GetComponent<Collider>());

                transform.position = player.position + new Vector3(.25f, .25f, 0f);
                deltaPos = player.position - lastPos;
                areaOfEffect.enabled = true;
                //towerPos = player.position + new Vector3(.5f, 0, 0);
                //moving left to right
                if (deltaPos.x > 0.001)
                {
                    spriteRenderer.sortingOrder = 2;
                    towerAmmoCanvas.sortingOrder = 2;
                    transform.position = player.position + new Vector3(.5f, .25f, 0);
                    //transform.position = player.position + new Vector3(.1f, .25f, 0);
                    //player.gameObject.GetComponent<SpriteRenderer>().sortingOrder = -5;
                    //maybe offset tower position here
                }

                //moving right to left
                if (deltaPos.x < -0.001)
                {
                    spriteRenderer.sortingOrder = 2;
                    towerAmmoCanvas.sortingOrder = 2;
                    transform.position = player.position + new Vector3(-.2f, .25f, 0);
                    //transform.position = player.position - new Vector3(.1f, -.25f, 0);
                    //player.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 5;
                    //maybe offset tower position here
                }

                //moving bottom to top
                if (deltaPos.y > 0.001)
                {
                    //transform.position = player.position + new Vector3(.5f, .25f, 0);
                    spriteRenderer.sortingOrder = 1;    //draw player over tower
                    towerAmmoCanvas.sortingOrder = 0;
                }

                //moving top to bottom
                if (deltaPos.y < -0.001)
                {
                    spriteRenderer.sortingOrder = 2;    //draw tower over player
                    towerAmmoCanvas.sortingOrder = 4;
                }

                if (deltaPos == new Vector3(0, 0, 0))
                {
                    spriteRenderer.sortingOrder = 1;    //default
                    towerAmmoCanvas.sortingOrder = 2;
                }

                //Debug.Log(deltaPos);

                //transform.position = player.position;

                if (tooClose == true)
                {
                    //cantPlaceCross.enabled = true;
                    spriteRenderer.color = Color.grey;
                }
                else if(tooClose == false)
                {
                    spriteRenderer.color = Color.white;
                    //cantPlaceCross.enabled = false;
                }

                if (input.actions["interact"].triggered)
                {
                    
                    if(tooClose == false)
                    {
                        gameManager.towerHoldBool();
                        //spriteRenderer.color = Color.green;
                        outline.enabled = true;
                        //transform.position = player.position;
                        //gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
                        spriteRenderer.sortingOrder = 1;    //make layerorder default
                        towerAmmoCanvas.sortingOrder = 2;
                        towerState = TowerState.Close;
                        tower.held = false;
                    }
                    
                }
                //Debug.Log(gameManager.getTowerHeld());
                break;
        }

        //spriteRenderer.color = towerColor;

        if (towerAmmo == 0)
        {
            //spriteRenderer.color = Color.white;
        }

        lastPos = player.position;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == 6 || other.gameObject.layer == 10)
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
        if (other.gameObject.layer == 6 || other.gameObject.layer == 10)
        {
            tooClose = false;
        }
        if (other.gameObject.layer == 7)
        {
            canPickUp = false;
        }
    }

}
