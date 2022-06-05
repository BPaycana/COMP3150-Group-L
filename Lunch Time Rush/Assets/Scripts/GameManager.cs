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
    
    public int l1TimeMin;
    public int l2TimeMin;
    public int l3TimeMin;
    public int l1TimeSec;
    public int l2TimeSec;
    public int l3TimeSec;
    public int l1TimeMs;
    public int l2TimeMs;
    public int l3TimeMs;

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
        l1TimeMin = data.bestTime1Min;
        l2TimeMin = data.bestTime2Min;
        l3TimeMin = data.bestTime3Min;
        l1TimeSec = data.bestTime1Sec;
        l2TimeSec = data.bestTime2Sec;
        l3TimeSec = data.bestTime3Sec;
        l1TimeMs = data.bestTime1Ms;
        l2TimeMs = data.bestTime2Ms;
        l3TimeMs = data.bestTime3Ms;



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
        SaveSystem.SaveInfo(mode, moveControls, interactControls, l1TimeMin, l1TimeSec, l1TimeMs, l2TimeMin, l2TimeSec, l2TimeMs, l3TimeMin, l3TimeSec, l3TimeMs);
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
        SaveSystem.SaveInfo(mode, moveControls, interactControls, l1TimeMin, l1TimeSec, l1TimeMs, l2TimeMin, l2TimeSec, l2TimeMs, l3TimeMin, l3TimeSec, l3TimeMs);
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
        SaveSystem.SaveInfo(mode, moveControls, interactControls, l1TimeMin, l1TimeSec, l1TimeMs, l2TimeMin, l2TimeSec, l2TimeMs, l3TimeMin, l3TimeSec, l3TimeMs);
    }

    public bool getInteractControls()
    {
        // true == Joystick
        // false == Move Towards Finger
        return interactControls;
    }

    public void setTimes(string levelTime, int levelNum)
    {

        if (levelNum == 1)
        {
            
            string[] level1TimeSplit = levelTime.Split(':');
            l1TimeMin = int.Parse(level1TimeSplit[0]);
            l1TimeSec = int.Parse(level1TimeSplit[1]);
            l1TimeMs = int.Parse(level1TimeSplit[2]);
        }
        if (levelNum == 2)
        {

            string[] level2TimeSplit = levelTime.Split(':');
            l2TimeMin = int.Parse(level2TimeSplit[0]);
            l2TimeSec = int.Parse(level2TimeSplit[1]);
            l2TimeMs = int.Parse(level2TimeSplit[2]);
        }
        if (levelNum == 3)
        {
            string[] level3TimeSplit = levelTime.Split(':');
            l3TimeMin = int.Parse(level3TimeSplit[0]);
            l3TimeSec = int.Parse(level3TimeSplit[1]);
            l3TimeMs = int.Parse(level3TimeSplit[2]);
        }
        SaveSystem.SaveInfo(mode, moveControls, interactControls, l1TimeMin, l1TimeSec, l1TimeMs, l2TimeMin, l2TimeSec, l2TimeMs, l3TimeMin, l3TimeSec, l3TimeMs);
        

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
            append += l1TimeMin.ToString() + ":";
            if(l1TimeSec < 10)
            {
                append += "0" + l1TimeSec.ToString() + ":";
            }
            else
            {
                append += l1TimeSec.ToString() + ":";
            }
            if (l1TimeMs < 10)
            {
                append += "0" + l1TimeMs.ToString();
            }
            else
            {
                append += l1TimeMs.ToString();
            }
        }
        if (levelNum == 2)
        {
            append += l2TimeMin.ToString() + ":";
            if (l2TimeSec < 10)
            {
                append += "0" + l2TimeSec.ToString() + ":";
            }
            else
            {
                append += l2TimeSec.ToString() + ":";
            }
            if (l2TimeMs < 10)
            {
                append += "0" + l2TimeMs.ToString();
            }
            else
            {
                append += l2TimeMs.ToString();
            }
        }
        if (levelNum == 3)
        {
            append += l3TimeMin.ToString() + ":";
            if (l3TimeSec < 10)
            {
                append += "0" + l3TimeSec.ToString() + ":";
            }
            else
            {
                append += l3TimeSec.ToString() + ":";
            }
            if (l3TimeMs < 10)
            {
                append += "0" + l3TimeMs.ToString();
            }
            else
            {
                append += l3TimeMs.ToString();
            }
        }
        return append;      
    }
}
