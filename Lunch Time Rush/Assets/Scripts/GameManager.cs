using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

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

    public bool gameMode;
    public bool moveControls;
    public bool interactControls;

    public GameObject Player;

    private TextAsset GameModeText;

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

        //new
        GameData data = SaveSystem.LoadData();

        gameMode = data.gameMode;
        moveControls = data.movement;
        interactControls = data.interact;

        if (moveControls)
        {
            Player.GetComponent<PlayerController>().enabled = true;
            Player.GetComponent<PlayerFollowCursor>().enabled = false;
        }
        else
        {
            Player.GetComponent<PlayerController>().enabled = false;
            Player.GetComponent<PlayerFollowCursor>().enabled = true;
        }

    }

    void Update() 
    {
        UIManager.Instance.UpdateHealth();
        UIManager.Instance.UpdateEnemyCounterText();
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

    public void setGameMode(bool classicEndless)
    {
        // true == Classic
        // false == Endless
        gameMode = classicEndless;
        //GameModeText = classicEndless.ToString();
        //File.WriteAllText("Assets/Scenes/Settings/GameMode.txt", classicEndless.ToString());
        SaveSystem.SaveInfo(gameMode, moveControls, interactControls);
    }

    public bool getGameMode()
    {
        // true == Classic
        // false == Endless
        return gameMode;
    }

    public void setMoveControls(bool controls)
    {
        // true == Joystick
        // false == Move Towards Finger
        moveControls = controls;
        //File.WriteAllText("Assets/Scenes/Settings/MovementControls.txt", moveControls.ToString());
        SaveSystem.SaveInfo(gameMode, moveControls, interactControls);
    }

    public bool getMoveControls()
    {
        // true == Joystick
        // false == Move Towards Finger
        return moveControls;
    }

    public void setInteractControls(bool controls)
    {
        // true == Joystick
        // false == Move Towards Finger
        interactControls = controls;
        //File.WriteAllText("Assets/Scenes/Settings/InteractControls.txt", interactControls.ToString());
        SaveSystem.SaveInfo(gameMode, moveControls, interactControls);
    }

    public bool getInteractControls()
    {
        // true == Joystick
        // false == Move Towards Finger
        return interactControls;
    }
}
