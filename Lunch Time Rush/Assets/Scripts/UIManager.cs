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

    
    public int health;
    public int numOfStars;

    public Image[] stars;
    public Sprite emptyStar;
    public Sprite  halfEmptyStar;
    public Sprite fullStar;

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

    }

    void Update()
    {

        if ( health> numOfStars)
        {
            health = numOfStars;
        }
        
      for (int i = 0; i< stars.Length; i++)
      {
          if (i< health){
              stars[i].sprite= fullStar;
          } else{
              stars[i].sprite = emptyStar;
          }

          if(i < numOfStars){
              stars[i].enabled=true;
          } else {
              stars[i].enabled=false;
          }
      }
    }

    public void SetMaxHealth(float health)
    {
    }

    public void SetHealth (float health)
    {
    
    }
}
