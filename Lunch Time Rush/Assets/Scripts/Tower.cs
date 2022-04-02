using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{


    public Transform target; // transform of the customers

    public float range = 15f; //tuneable parameter for the range of the tower
    public int ammoCapacity = 15;

    public string enemyTag = "Enemy";

    public float fireRate = 1f;
    private float fireCountDown = 0f;

    public GameObject bulletPrefab;
    
    
    public Transform firePoint;

    private int ammo;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        ammo = ammoCapacity;
    }


    void UpdateTarget()
    {
        
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        //store shortest distance to an enemy found so far
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance (transform.position, enemy.transform.position);
            
            if(distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;

            }        
        }

        if (nearestEnemy != null && shortestDistance <= range) // check if nearest enemy is found and if the shorest distance is within tower range
        {
            // found enemy and within tower range
            target = nearestEnemy.transform; // set target to that enemy
        }  
        else
        {
            target = null;
        }



    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            return;
        }

        if (fireCountDown <= 0f)
        {
            Shoot();
            fireCountDown = 1f/fireRate;
        }

        fireCountDown -=Time.deltaTime;
    }

    public void refillAmmo(int refillAmount)
    {
        ammo = refillAmount;
        Debug.Log("refilled tower with ammo amount: " + refillAmount);
    }

    void Shoot()
    {
        if(ammo > 0)
        {
            GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Bullet bullet = bulletGO.GetComponent<Bullet>();
            if(bullet != null)
            {
                bullet.Seek(target);
                ammo--;

            }

            Debug.Log("Shot at the customer, ammo left: " + ammo);
        } 
        else
        {
            Debug.Log("Out Of Ammo");
        }


    }
}
