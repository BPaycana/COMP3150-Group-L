using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using System.IO;

public class UIManager : MonoBehaviour
{

    static private UIManager instance;
    static public UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("There is no UIManager instance in the scene.");
            }
            return instance;
        }
    }


    public GameObject gameOverPanel;
    public GameObject gameWonPanel;
    public Text gameOverText;
    public Text gameWonText;
    public GameObject menuPanel;
    public EnemySpawn spawner;
    public GameObject enemyHolder;
    public EndlessTimer endlessTimer;
    private String timer;
    private bool gameMode;

    public GameObject survivePanel;
    public TextMeshPro surviveTime;

    private string gameState;

    private string winText = "You Survived the Lunch Time Rush!";
    private string loseText = "Nice job running your restaurant to 0 star review!";

    public float playerHealth;

    [SerializeField] private Image[] stars;
    private int levelCount = 0;

    private float enemyCount;
    public TextMeshPro enemyCountText;

    public TextMeshPro Level1BestTimes;
    public TextMeshPro Level2BestTimes;
    public TextMeshPro Level3BestTimes;

    public GameObject pausePanel;

    void Awake()
    {
        if (instance != null)
        {
            // if there is already a UIManager in the scene, destroy this one
            //Destroy(gameObject);
        }
        else
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        
        gameMode = FindObjectOfType<GameManager>().getMode();
        Debug.Log("game mode = " + gameMode);
        enemyCount = spawner.maxEnemies;

        if(gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
        if(gameWonPanel != null)
        {
            gameWonPanel.SetActive(false);
        }
        if(pausePanel != null)
        {
            pausePanel.SetActive(false);
        }
        if(survivePanel != null)
        {
            survivePanel.SetActive(false);
        }
                
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Menu")
        {
            menuPanel.SetActive(true);
        }

        if(Level1BestTimes != null)
        {
            Level1BestTimes.SetText("Level 1 Best Time: " + File.ReadAllText("Assets/Scenes/Endless/BestTimes/Level1Times.txt"));
            Level2BestTimes.SetText("Level 2 Best Time: " + File.ReadAllText("Assets/Scenes/Endless/BestTimes/Level2Times.txt"));
            Level3BestTimes.SetText("Level 3 Best Time: " + File.ReadAllText("Assets/Scenes/Endless/BestTimes/Level3Times.txt"));
        }
        
        UpdateHealth();
    }

    void Update()
    {
        
        if(gameMode == false && gameState == null)
        {
            timer = endlessTimer.getTimer();
            enemyCountText.SetText(timer);
        }
        

    }

    public void UpdateHealth()
    {
        for (int i = 0; i < stars.Length; i++)
        {
            if (i < playerHealth)
            {
                stars[i].color = Color.yellow; // as long as i is less than player health, then set all stars to yellow as they are available
            }
            else
            {
                stars[i].color = Color.black; // if i is above player health, it means that health is lost
            }
        }

    }

    public void SetMaxHealth(float health)
    {
        playerHealth = health;
    }

    public void SetHealth(float health)
    {
        playerHealth = health;
    }

    public void ShowGameOver(bool win)
    {

        if (win)
        {
            //gameWonText.text = winText;
            enemyHolder.SetActive(false);
            spawner.enabled = false;
            gameState = "won";
            gameWonPanel.SetActive(true);
        }
        else if(gameMode == true)
        {
            //gameOverText.text = loseText;
            spawner.enabled = false;
            enemyHolder.SetActive(false);
            gameState = "lost";
            gameOverPanel.SetActive(true);
        }
        else
        {
            //spawner.enabled = false;
            enemyHolder.SetActive(false);
            gameState = "survived";
            survivePanel.SetActive(true);
            surviveTime.text = "You survived for: " + timer;
            string bestTimes;

            // if level 1
            if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                bestTimes = "Assets/Scenes/Endless/BestTimes/Level1Times.txt";  
            }
            // if level 2
            else if (SceneManager.GetActiveScene().buildIndex == 3)
            {
                bestTimes = "Assets/Scenes/Endless/BestTimes/Level2Times.txt";
            }
            // if level 3
            else
            {
                bestTimes = "Assets/Scenes/Endless/BestTimes/Level3Times.txt";
            }

            // splits the timers into numbers
            string textTimer = File.ReadAllText(bestTimes);
            string[] textTimerSplit = textTimer.Split(':');
            string[] timerSplit = timer.Split(':');

            // if minutes are equal
            if (textTimerSplit[0] == timerSplit[0])
            {
                // if seconds are equal
                if (textTimerSplit[1] == timerSplit[1])
                {
                    // if miliseconds are greater than previous best time
                    if (int.Parse(timerSplit[2]) > int.Parse(textTimerSplit[2]))
                    {
                        // overwrite best time with new time
                        File.WriteAllText(bestTimes, timer);
                    }
                }
                // if seconds are greater than previous best time
                else if (int.Parse(timerSplit[1]) > int.Parse(textTimerSplit[1]))
                {
                    // overwrite best time with new time
                    File.WriteAllText(bestTimes, timer);
                }
            }
            // if minutes are greater than previous best time
            else if (int.Parse(timerSplit[0]) > int.Parse(textTimerSplit[0]))
            {
                // overwrite best time with new time
                File.WriteAllText(bestTimes, timer);
            }
        }

    }

    public void Restart()
    {
        // reload this scene
        //SceneManager.LoadScene(0); // 
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        gameOverPanel.SetActive(false);

        GameManager.Instance.Start();
    }

    public void LevelBeginner()
    {
        gameWonPanel.SetActive(false);
        Debug.Log("Clicked beginner");
        SceneManager.LoadScene(2);

    }

    public void LevelIntermediate()
    {
        gameWonPanel.SetActive(false);
        SceneManager.LoadScene(3);
        
    }

    public void LevelAdvanced()
    {
        gameWonPanel.SetActive(false);
        SceneManager.LoadScene(4);

    }

    public void LoadLevelMenu()
    {
        gameWonPanel.SetActive(false);
        SceneManager.LoadScene(1);
    }

    public void LoadSettingsMenu()
    {
        gameWonPanel.SetActive(false);
        SceneManager.LoadScene(6);
    }

    public void LoadMovementSettings()
    {
        gameWonPanel.SetActive(false);
        SceneManager.LoadScene(7);
    }

    public void LoadInteractSettings()
    {
        gameWonPanel.SetActive(false);
        SceneManager.LoadScene(8);
    }

    public void LoadBestTimes()
    {
        gameWonPanel.SetActive(false);
        SceneManager.LoadScene(10);
    }

    public void EndlessMenu()
    {
        gameWonPanel.SetActive(false);
        SceneManager.LoadScene(9);
    }

    public void Won()
    {
        gameWonPanel.SetActive(false);
        SceneManager.LoadScene("GameWon");
    }

    public string GetGameState()
    {
        return gameState;
    }

    public void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        
    }
     
    public void Home(int sceneID)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneID);
    }

    public void UpdateEnemyCounterText()
    {
        if(gameMode == true)
        {
            enemyCountText.SetText("Customers left: " + enemyCount);
        }    
    }

    public void UpdateEnemyCounter()
    {
        enemyCount--;
    }
}
