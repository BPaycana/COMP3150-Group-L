using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{

    public float targetHealth;
    private float health = 0f;
    private float specHealth = 0f;

    public float Health
    {
        get
        {
            return health;
        }
    }
    public float SpecHealth
    {
        get
        {
            return specHealth;
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
    public Image specHealthBar;
    public Image specHealthBarBackground;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float amount, string type)
    {
        
        health += amount;

        healthBar.fillAmount = health / targetHealth;
    }

    public void SpecTakeDamage(float amount, string type)
    {

        specHealth += amount;

        specHealthBar.fillAmount = specHealth / targetHealth;

    }
}
