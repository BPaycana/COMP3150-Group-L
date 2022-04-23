using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{

    public float startHealth = 0;
    private float health;
    public float Health
    {
        get
        {
            return health;
        }
    }


    public float targetHealth = 100f;

    public float TargetHealth
    {
        get
        {
            return targetHealth;
        }
    }
    public Image healthBar;
    // Start is called before the first frame update
    void Start()
    {
        health = startHealth;
        Debug.Log(health);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float amount)
    {
        health += amount;

        healthBar.fillAmount = health / targetHealth;

    }


    
}
