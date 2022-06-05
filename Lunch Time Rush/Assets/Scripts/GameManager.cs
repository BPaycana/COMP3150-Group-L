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

    public bool mode;
    public bool moveControls;
    public bool interactControls;
    
    public int[] l1Time;
    public int[] l2Time;
    public int[] l3Time; 

    //string l1Time;
    //string l2Time;
    //string l3Time;

    public GameObject Player;

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

        //new
        GameData data = SaveSystem.LoadData();

        mode = data.gameMode;
        moveControls = data.movement;
        interactControls = data.interact;
        l1Time = data.bestTime1;
        l2Time = data.bestTime2;
        l3Time = data.bestTime3;


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

        //Debug.Log("GAME MANAGER HAS STARTED NOW");

        //mode = true;
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

    public void setMode(bool classicEndless)
    {
        // true == Classic
        // false == Endless
        mode = classicEndless;
        //GameModeText = classicEndless.ToString();
        //File.WriteAllText("Assets/Scenes/Settings/GameMode.txt", classicEndless.ToString());
        SaveSystem.SaveInfo(mode, moveControls, interactControls, l1Time, l2Time, l3Time);
    }

    public bool getMode()
    {
        // true == Classic
        // false == Endless
        Debug.Log("HI I'VE BEEN CALLED " + mode);
        return mode;
    }

    public void setMoveControls(bool controls)
    {
        // true == Joystick
        // false == Move Towards Finger
        moveControls = controls;
        //File.WriteAllText("Assets/Scenes/Settings/MovementControls.txt", moveControls.ToString());
        SaveSystem.SaveInfo(mode, moveControls, interactControls, l1Time, l2Time, l3Time);
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
        SaveSystem.SaveInfo(mode, moveControls, interactControls, l1Time, l2Time, l3Time);
    }

    public bool getInteractControls()
    {
        // true == Joystick
        // false == Move Towards Finger
        return interactControls;
    }

    public void setTimes(string levelTime, int levelNum)
    {
        
        int[] level1Text = new int[3];
        int[] level2Text = new int[3];
        int[] level3Text = new int[3];

        if (levelNum == 1)
        {
            
            string[] level1TimeSplit = levelTime.Split(':');
            for(int i = 0; i < 3; i++)
            {
                level1Text[i] = int.Parse(level1TimeSplit[i]);
            }          
            l1Time = level1Text;
            Debug.Log("hyah1 " + l1Time);
        }
        if (levelNum == 2)
        {
            
            string[] level2TimeSplit = levelTime.Split(':');
            for (int i = 0; i < 3; i++)
            {
                level2Text[i] = int.Parse(level2TimeSplit[i]);
            }
            l2Time = level2Text;
            Debug.Log("hyah2 " + l2Time);
        }
        if (levelNum == 3)
        {
            
            string[] level3TimeSplit = levelTime.Split(':');
            for (int i = 0; i < 3; i++)
            {
                level3Text[i] = int.Parse(level3TimeSplit[i]);
            }
            l3Time = level3Text;
            Debug.Log("hyah3 " + l3Time);
        }
        SaveSystem.SaveInfo(mode, moveControls, interactControls, l1Time, l2Time, l3Time);
        

        /*
        if (levelNum == 1)
        {
            l1Time = levelTime;
        }
        if (levelNum == 2)
        {
            l2Time = levelTime;
        }
        if (levelNum == 3)
        {
            l3Time = levelTime;
        }
        SaveSystem.SaveInfo(mode, moveControls, interactControls, l1Time, l2Time, l3Time);
        */
    }

    public string getTimes(int levelNum)
    {
        string append = "";
        if (levelNum == 1)
        {
            Debug.Log("hmmmmm1 " + l1Time.Length);
            append += l1Time[0].ToString() + ":";
            if(l1Time[1] < 10)
            {
                append += "0" + l1Time[1].ToString() + ":";
            }
            else
            {
                append += l1Time[1].ToString() + ":";
            }
            if (l1Time[2] < 10)
            {
                append += "0" + l1Time[2].ToString();
            }
            else
            {
                append += l1Time[2].ToString();
            }
        }
        if (levelNum == 2)
        {
            Debug.Log("hmmmmm2 " + l2Time.Length);
            append += l2Time[0].ToString() + ":";
            if (l2Time[1] < 10)
            {
                append += "0" + l2Time[1].ToString() + ":";
            }
            else
            {
                append += l2Time[1].ToString() + ":";
            }
            if (l2Time[2] < 10)
            {
                append += "0" + l2Time[2].ToString();
            }
            else
            {
                append += l2Time[2].ToString();
            }
        }
        if (levelNum == 3)
        {
            Debug.Log("hmmmmm3 " + l3Time.Length);
            append += l3Time[0].ToString() + ":";
            if (l3Time[1] < 10)
            {
                append += "0" + l3Time[1].ToString() + ":";
            }
            else
            {
                append += l3Time[1].ToString() + ":";
            }
            if (l3Time[2] < 10)
            {
                append += "0" + l3Time[2].ToString();
            }
            else
            {
                append += l3Time[2].ToString();
            }
        }
        return append;
    }
}
