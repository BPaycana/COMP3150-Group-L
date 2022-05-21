using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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


    private string winText = "You Survived the Lunch Time Rush!";
    private string loseText = "Nice job running your restaurant to 0 star review!";

    public float playerHealth;

    [SerializeField] private Image[] stars;
    private int levelCount = 0;

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
        gameOverPanel.SetActive(false);
        gameWonPanel.SetActive(false);

        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Menu")
        {
            menuPanel.SetActive(true);
        }

        UpdateHealth();
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
            gameWonText.text = winText;
            gameWonPanel.SetActive(true);
        }
        else
        {
            gameOverText.text = loseText;
            gameOverPanel.SetActive(true);
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
        //gameWonPanel.SetActive(false);
        SceneManager.LoadScene(0);

    }

    public void LevelIntermediate()
    {
        gameWonPanel.SetActive(false);
        SceneManager.LoadScene(1);
        
    }

    public void LevelAdvanced()
    {
        gameWonPanel.SetActive(false);
        SceneManager.LoadScene(2);

    }

    public void Won()
    {
        gameWonPanel.SetActive(false);
        SceneManager.LoadScene("GameWon");
    }


}
