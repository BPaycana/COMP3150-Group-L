using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyHealth : MonoBehaviour
{

    public float targetHealth;  //current health
    public float targetSpecHealth;
    private float health = 0f;
    private float specHealth = 0f;

    public AudioClip Eating;
    public AudioClip Full;

    private bool isSpecial = false;

    public bool IsSpecial
    {
        get
        {
            return isSpecial;
        }
    }

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

    public TextMeshPro healthNum;
    public TextMeshPro specHealthNum;


    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().clip = Eating;
        health = targetHealth;
        specHealth = targetSpecHealth;
        foreach (Transform child in transform)
        {
            Debug.Log("tryna find some shit");
            if (child.name == "BurgerBubble(Clone)")
            {
                foreach (Transform c in child.transform)
                {
                    if (c.name == "BurgerText")
                    {
                        Debug.Log("Got BurgerText");
                        healthNum = c.GetComponentInChildren<TextMeshPro>();
                        healthNum.SetText(health.ToString());
                        Debug.Log("Got BurgerText: " + healthNum.name);
                    }
                }
            }
            if (child.name == "PizzaBubble(Clone)")
            {
                foreach (Transform c in child.transform)
                {
                    if (c.name == "PizzaText")
                    {
                        Debug.Log("Got PizzaText");
                        healthNum = c.GetComponentInChildren<TextMeshPro>();
                        healthNum.SetText(health.ToString());
                        Debug.Log("Got PizzaText: " + healthNum.name);
                    }
                }
            }
            if (child.name == "BurgerDrinkBubble(Clone)")
            {
                foreach (Transform c in child.transform)
                {
                    if (c.name == "BurgerText")
                    {
                        Debug.Log("Got BurgerText");
                        healthNum = c.GetComponentInChildren<TextMeshPro>();
                        healthNum.SetText(health.ToString());
                        Debug.Log("Got BurgerText: " + healthNum.name);
                    }
                    if (c.name == "DrinkText")
                    {
                        Debug.Log("Got DrinkText");
                        specHealthNum = c.GetComponentInChildren<TextMeshPro>();
                        specHealthNum.SetText(specHealth.ToString());
                        Debug.Log("Got DrinkText: " + specHealthNum.name);
                    }
                }
            }
            if (child.name == "PizzaDrinkBubble(Clone)")
            {
                foreach (Transform c in child.transform)
                {
                    if (c.name == "PizzaText")
                    {
                        Debug.Log("Got PizzaText");
                        healthNum = c.GetComponentInChildren<TextMeshPro>();
                        healthNum.SetText(health.ToString());
                        Debug.Log("Got PizzaText: " + healthNum.name);
                    }
                    if (c.name == "DrinkText")
                    {
                        Debug.Log("Got DrinkText");
                        specHealthNum = c.GetComponentInChildren<TextMeshPro>();
                        specHealthNum.SetText(specHealth.ToString());
                        Debug.Log("Got DrinkText: " + specHealthNum.name);
                    }
                }
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0 && targetSpecHealth <= 0)
        {

            foreach (Transform child in transform)
            {
                Debug.Log("tryna find some shit");
                if (child.name == "BurgerBubble(Clone)")
                {
                    child.gameObject.SetActive(false);
                }
                if (child.name == "PizzaBubble(Clone)")
                {
                    child.gameObject.SetActive(false);
                }
                if (child.name == "BurgerDrinkBubble(Clone)")
                {
                    child.gameObject.SetActive(false);
                }
                if (child.name == "PizzaDrinkBubble(Clone)")
                {
                    child.gameObject.SetActive(false);
                }
                if (child.name == "SmileBubble(Clone)")
                {
                    child.gameObject.SetActive(true);
                }
            }
        }
        if (health <= 0 && specHealth <= 0)
        {

            foreach (Transform child in transform)
            {
                Debug.Log("tryna find some shit");
                if (child.name == "BurgerBubble(Clone)")
                {
                    child.gameObject.SetActive(false);
                }
                if (child.name == "PizzaBubble(Clone)")
                {
                    child.gameObject.SetActive(false);
                }
                if (child.name == "BurgerDrinkBubble(Clone)")
                {
                    child.gameObject.SetActive(false);
                }
                if (child.name == "PizzaDrinkBubble(Clone)")
                {
                    child.gameObject.SetActive(false);
                }
                if (child.name == "SmileBubble(Clone)")
                {
                    child.gameObject.SetActive(true);
                }
            }
        }
    }

    public void TakeDamage(float amount, string type)
    {
        //whne take damage, bullet damage "amount" makes 'health' go up to damage the player.
        //eg health start 0, targethealth 3,  bullet damages health to +3 satisfying the player
        health -= amount;
        Debug.Log(health);
        healthNum.SetText((health).ToString());
        //Debug.Log(health);
        //healthBar.fillAmount = health / targetHealth;
        if (health <= 0 && specHealth <= 0)
        {
            GetComponent<AudioSource>().clip = Full;
        }
        GetComponent<AudioSource>().Play(0);


    }

    public void SpecTakeDamage(float amount, string type)
    {

        specHealth -= amount;

        specHealthNum.SetText((specHealth).ToString());
        if (health <= 0 && specHealth <= 0)
        {
            GetComponent<AudioSource>().clip = Full;
        }
        GetComponent<AudioSource>().Play(0);
        //specHealthBar.fillAmount = specHealth / targetHealth;

    }

    public void EnemyIsSpecial()
    {
        isSpecial = true;
    }
}
