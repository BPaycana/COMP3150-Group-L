using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{

    public float targetHealth;
    private float health = 0f;
    public float Health
    {
        get
        {
            return health;
        }
    }

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
