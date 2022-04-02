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
            Destroy(gameObject);            
        }
        else 
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }        
    }


    // Start is called before the first frame update
    public void Start()
    {

        restaurantIsAlive = true;
        maxRestaurantHealth = 5f;
        currentRestaurantHealth = maxRestaurantHealth;
        UIManager.Instance.SetMaxHealth(maxRestaurantHealth);
    }

    void Update() 
    {
      
    }


    public void DamageRestaurant(float damage)
    {
        if(currentRestaurantHealth < 0)
        {
            
        }
        currentRestaurantHealth -= damage;
        UIManager.Instance.SetHealth(currentRestaurantHealth);
    }

}
