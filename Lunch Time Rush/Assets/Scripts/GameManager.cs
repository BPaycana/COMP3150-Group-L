using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
     // Singleton
    static private GameManager instance;
    static public GameManager Instance 
    {
        get 
        {
            if (instance == null)
            {
                Debug.LogError("There is no GameManager instance in the scene.");
            }
            return instance;
        }
    }


    private bool towerHeld;
    private bool canRestock;
    private bool getRestock;


    private float maxRestaurantHealth;
    public float MaxRestaurantHealth 
    {
        get
        {
            return maxRestaurantHealth;
        }
    }

    private float currentRestaurantHealth;
    public float CurrentRestaurantHealth
    {
        get
        {
            return currentRestaurantHealth;
        }
    }
    
    private bool restaurantIsAlive;

    public bool RestaurantIsAlive
    {
        get
        {
            return RestaurantIsAlive;
        }
    }

    void Awake()
    {
        if (instance != null) 
        {
            // destroy duplicates
            //Destroy(gameObject);            
        }
        else 
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }        
    }


    // Start is called before the first frame update
    public void Start()
    {

        restaurantIsAlive = true;
        maxRestaurantHealth = 5f; // set restaurant health at the start

        currentRestaurantHealth = maxRestaurantHealth;
        UIManager.Instance.SetMaxHealth(maxRestaurantHealth);

        towerHeld = false;
        canRestock = false;
        getRestock = true;
    }

    void Update() 
    {
        UIManager.Instance.UpdateHealth();
    }


    public void DamageRestaurant(float damage)
    {
        if(currentRestaurantHealth <= 1) // weird bug, doesnt work at <=0
        {
            // game over
            Die(); 
        }
        currentRestaurantHealth -= damage;
        Debug.Log("restHealth: " + currentRestaurantHealth);
        UIManager.Instance.SetHealth(currentRestaurantHealth);
    }

    public void Die()
    {
        restaurantIsAlive = false;
        //Debug.Log("car died");
        GameOver(false);

    }

    public void GameOver(bool win)
    {
        UIManager.Instance.ShowGameOver(win);
    }

    public void towerHoldBool()
    {
        if (towerHeld == true)
        {
            towerHeld = false;
        }
        else if (towerHeld == false)
        {
            towerHeld = true;
        }
    }

    public bool getTowerHeld()
    {
        //Debug.Log("isTowerHeld: " + towerHeld);
        return towerHeld;
    }

    public void restock()
    {
        if (canRestock == true)
        {
            canRestock = false;
        }
        else if (canRestock == false)
        {
            canRestock = true;
        }
        //Debug.Log(canRestock);
    }

    public bool restockState()
    {
        //Debug.Log(canRestock);
        return canRestock;
    }

}
