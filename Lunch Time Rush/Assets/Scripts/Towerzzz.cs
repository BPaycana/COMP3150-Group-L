using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Towerzzz : MonoBehaviour
{


    public Transform target; // transform of the customers

    public float range = 15f; //tuneable parameter for the range of the tower

    public string enemyTag = "Enemy";

    public float fireRate = 1f;
    private float fireCountDown = 0f;

    public GameObject bulletPrefab;
    
    
    public Transform firePoint;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
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

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        if(bullet != null)
        {
            bullet.Seek(target);

        }

        Debug.Log("Shot at the customer");
    }
}
