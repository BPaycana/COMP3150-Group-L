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
    private Transform towerTrans;
    private int towerAmmo;
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
        //spriteRenderer.color = Color.yellow;

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

                if (Mathf.Abs(dist) > interactRange)
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
                    else
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

                if (Mathf.Abs(dist) <= interactRange)
                {
                    spriteRenderer.color = Color.green;
                    towerState = TowerState.Close;
                }


                break;

            case TowerState.Held:

                //Physics.IgnoreCollision(player.GetComponent<Collider>(), GetComponent<Collider>());
                transform.position = player.position + new Vector3(.25f, .25f, 0f);
                deltaPos = player.position - lastPos;
                //towerPos = player.position + new Vector3(.5f, 0, 0);
                //moving left to right
                if (deltaPos.x > 0.001)
                {
                    spriteRenderer.sortingOrder = 2;
                    transform.position = player.position + new Vector3(.5f, .25f, 0);
                    //transform.position = player.position + new Vector3(.1f, .25f, 0);
                    //player.gameObject.GetComponent<SpriteRenderer>().sortingOrder = -5;
                    //maybe offset tower position here
                }

                //moving right to left
                if (deltaPos.x < -0.001)
                {
                    spriteRenderer.sortingOrder = 2;
                    transform.position = player.position + new Vector3(-.2f, .25f, 0);
                    //transform.position = player.position - new Vector3(.1f, -.25f, 0);
                    //player.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 5;
                    //maybe offset tower position here
                }

                //moving bottom to top
                if(deltaPos.y > 0.001)
                {
                    //transform.position = player.position + new Vector3(.5f, .25f, 0);
                    spriteRenderer.sortingOrder = 1;    //draw player over tower
                }

                //moving top to bottom
                if (deltaPos.y < -0.001)
                {
                    spriteRenderer.sortingOrder = 2;    //draw tower over player
                }

                if(deltaPos == new Vector3(0, 0, 0))
                {
                    spriteRenderer.sortingOrder = 1;    //default
                }

                //Debug.Log(deltaPos);

                if (input.actions["drop"].triggered)
                {
                    gameManager.towerHoldBool();
                    spriteRenderer.color = Color.green;
                    //transform.position = player.position;
                    towerState = TowerState.Close;
                    tower.held = false;
                }
                //Debug.Log(gameManager.getTowerHeld());
                break;
        }

        //spriteRenderer.color = towerColor;

        if (towerAmmo == 0)
        {
            spriteRenderer.color = Color.white;
        }

        lastPos = player.position;
        
    }

}
