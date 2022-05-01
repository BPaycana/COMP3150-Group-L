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
    public Text gameOverText;


    private string winText = "You Survived the Lunch Time Rush!";
    private string loseText = "Nice job running your restaurant to 0 star review!";

    public float playerHealth;
    
    [SerializeField] private Image[] stars;


    void Awake() 
    {
        if (instance != null)
        {
            // if there is already a UIManager in the scene, destroy this one
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        gameOverPanel.SetActive(false);
        UpdateHealth();
    }
    
    public void UpdateHealth()
    {
        for(int i = 0; i < stars.Length; i++)
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

    public void SetHealth (float health)
    {
        playerHealth = health;
    }

     public void ShowGameOver(bool win)
    {


        if (win)
        {
            gameOverText.text = winText;
        }
        else 
        {
            gameOverText.text = loseText;
        }
        gameOverPanel.SetActive(true);
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



}
