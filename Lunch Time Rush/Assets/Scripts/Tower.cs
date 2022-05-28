using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour
{


    public Transform target; // transform of the customers

    private string enemyType;
    private string enemySpecType;

    private float targetHealth;
    private float health;
    private float specHealth;

    private EnemyHealth enemyHealth;
    private EnemyMove enemyMove;

    public float range = 15f; //tuneable parameter for the range of the tower
    public float ammoCapacity = 15;
    private float ammo;

    public string enemyTag = "Enemy";

    public float bulletDamage = 5f;

    public float fireRate = 1f;
    private float fireCountDown = 0f;

    public bool held = false;

    public GameObject bulletPrefab;

    private GameManager gameManager;

    private Color towerColor;

    public Transform firePoint;

    public Image ammoBar;

    public AudioClip RestockTowerSound;

    public LayerMask IgnoreMe;
    
    public string towerType;
    
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        ammo = ammoCapacity;
        towerColor = gameObject.GetComponent<SpriteRenderer>().color;
        Debug.Log(towerColor);
    }

    public Color getColor()
    {
        return towerColor;
    }

    void UpdateTarget()
    {

        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);


        //store shortest distance to an enemy found so far
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;

            }
        }        

        // check if nearest enemy is found and if the shorest distance is within tower range
        if (nearestEnemy != null && shortestDistance <= range)
        {

            enemyHealth = nearestEnemy.GetComponent<EnemyHealth>();
            enemyMove = nearestEnemy.GetComponent<EnemyMove>();
            enemyType = enemyMove.getType();
            enemySpecType = enemyMove.getSpecType();


            // found enemy and within tower range
            target = nearestEnemy.transform; // set target to that enemy
        }
        else
        {
            target = null;
            enemyType = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            return;
        }
        else // theres a target, get the target's health
        {
            targetHealth = enemyHealth.TargetHealth;
            health = enemyHealth.Health;
            if (enemyHealth.IsSpecial == false)
            {
                specHealth = targetHealth;

            }
            else
            {
                specHealth = enemyHealth.SpecHealth;
            }
            // check if the enemy is special, if it's not, set spechealth = target health

        }

        if (fireCountDown <= 0f)
        {
            Shoot();
            
            fireCountDown = 1f / fireRate;
        }
        animator.SetBool("Shoot", false);
        
        fireCountDown -= Time.deltaTime;
    }

    public bool refillAmmo(int refillAmount)
    {
        /*
        if (ammo <= 0)
        {
            ammo = refillAmount;
            ammoBar.fillAmount = ammo / ammoCapacity;
            Debug.Log("refilled tower with ammo amount: " + refillAmount);
            return true;
        }
        Debug.Log("Tower still has ammo!");
        return false;
        */
        GetComponent<AudioSource>().clip = RestockTowerSound;
        GetComponent<AudioSource>().Play(0);
        ammo = refillAmount;
        ammoBar.fillAmount = ammo / ammoCapacity;
        Debug.Log("refilled tower with ammo amount: " + refillAmount);
        return true;

    }

    void Shoot()
    {

        Debug.Log("Custome target health is " + targetHealth);
        Debug.Log("Custome health is " + health);
        Debug.Log("Custome drink health is " + specHealth);
        //if ((health < targetHealth || specHealth < targetHealth) && string.Equals(towerType, enemyType) || string.Equals(towerType, "drink"))
        if (health > 0 && string.Equals(towerType, enemyType) || specHealth > 0 && string.Equals(towerType, enemySpecType))
        {
            if(Physics2D.Linecast(transform.position, target.position, ~IgnoreMe))
            {
                Debug.Log("BAZINGA");
            }
            if (ammo > 0 && held == false && !Physics2D.Linecast(transform.position, target.position, ~IgnoreMe))
            {
                animator.SetBool("Shoot", true);
                GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                Bullet bullet = bulletGO.GetComponent<Bullet>();
                bullet.bulletStrength = bulletDamage;
                if (bullet != null)
                {
                    bullet.Seek(target);
                    ammo--;
                    ammoBar.fillAmount = ammo / ammoCapacity;
                }
                Debug.Log("Shot at the customer, ammo left: " + ammo);
            }
            else if (held == true)
            {
                //Debug.Log("Holding tower, not shooting");
            }
            else
            {
                Debug.Log("Out Of Ammo");
            }

        }
        else
        {
            Debug.Log("Not correct customer type to the tower");
        }

    }

    public float getAmmo()
    {
        return ammo;
    }
}
