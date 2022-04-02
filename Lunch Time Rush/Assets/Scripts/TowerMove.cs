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
        gameManager = FindObjectOfType<GameManager>();
        input = gameManager.gameObject.GetComponent<PlayerInput>();
        tower = this.gameObject.GetComponent<Tower>();
        towerState = TowerState.Far;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.yellow;

        towerAmmo = maxTowerAmmo;
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

                if (input.actions["Interact"].triggered)
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit2D hit2D = Physics2D.GetRayIntersection(ray);
                    if(hit2D.collider != null)
                    {
                        if (hit2D.transform.gameObject == gameObject &&
                            !gameManager.GetComponent<GameManager>().getTowerHeld())                       
                        {
                            gameManager.towerHoldBool();
                            spriteRenderer.color = Color.blue;
                            towerState = TowerState.Held;
                        }                                           
                    }
                }

                if (input.actions["Interact2"].triggered)
                {
                        if (gameManager.GetComponent<GameManager>().restockState())
                        {
                            Debug.Log("player restocking tower");
                            tower.refillAmmo(maxTowerAmmo);
                            gameManager.restock();
                            towerAmmo = maxTowerAmmo;
                            spriteRenderer.color = Color.red;
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

                
                //Physics.IgnoreCollision(player.GetComponent<Collider>(), GetComponent<Collider>());
                transform.position = player.position;
                if (input.actions["drop"].triggered)
                {
                    gameManager.towerHoldBool();
                    spriteRenderer.color = Color.green;
                    //transform.position = player.position;
                    towerState = TowerState.Close;
                }
                
                break;
        }

        if(towerAmmo == 0)
        {
            spriteRenderer.color = Color.white;
        }
    }
}
