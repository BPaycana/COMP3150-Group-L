using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TowerRestock : MonoBehaviour
{
    public int restockAmt;
    public Transform player;
    private GameManager gameManager;
    private PlayerInput input;
    private SpriteRenderer spriteRenderer;
    private int _restockAmt;

    public SpriteRenderer outline;

    // Start is called before the first frame update
    void Start()
    {
        _restockAmt = restockAmt;
        gameManager = FindObjectOfType<GameManager>();
        input = gameManager.gameObject.GetComponent<PlayerInput>();
        Debug.Log(input.gameObject.name);

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(player.position, transform.position);
        
        if(Mathf.Abs(dist) < 1)
        {
            outline.enabled = true;
        }
        else
        {
            outline.enabled = false;
        }

        if (input.actions["Interact"].triggered &&
                Mathf.Abs(dist) < 1 && 
                !gameManager.GetComponent<GameManager>().restockState())
        {
            Debug.Log("towerRestock restocking player");
            gameManager.GetComponent<GameManager>().restock();
        }
        
    }
}
