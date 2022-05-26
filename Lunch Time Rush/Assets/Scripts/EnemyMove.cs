using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public Path path;
    public float speed = 2; // metres per second

    private float reviewDamage = 1f;
    private int nextWaypoint = 1;
    private string enemyType;
    private string enemySpecType;
    private SpriteRenderer spriteRenderer;
    public Sprite burgerMan;
    public Sprite pizzaGirl;
    public Sprite drinkMan;

    public GameObject pizzaBubble;
    public GameObject burgerBubble;
    public GameObject pizzaDrinkBubble;
    public GameObject burgerDrinkBubble;

    public bool isLastEnemy = false;


    void Start()
    {
        // start at waypoint 0
        transform.position = path.Waypoint(0);
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (enemyType == "pizza" && enemySpecType != "drink")
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = pizzaGirl;
            GameObject bubble = Instantiate(pizzaBubble, this.gameObject.transform);
            bubble.transform.Translate(new Vector3(-.22f, .6f, 0));
            //bubble.transform.Translate(new Vector3(25, -4));
        }
        if (enemyType == "burger" && enemySpecType != "drink")
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = burgerMan;
            GameObject bubble = Instantiate(burgerBubble, this.gameObject.transform);
            //bubble.transform.position = new Vector3(-2, 6, 0);
            bubble.transform.Translate(new Vector3(-.22f, .6f, 0));
            //bubble.transform.Translate(new Vector3(25, -4));
        }
        if (enemySpecType == "drink" && enemyType == "burger")
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = drinkMan;
            GameObject bubble = Instantiate(burgerDrinkBubble, this.gameObject.transform);
            bubble.transform.Translate(new Vector3(-.22f, .6f, 0));
            //bubble.transform.Translate(new Vector3(25, -4));
        }
        if (enemySpecType == "drink" && enemyType == "pizza")
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = drinkMan;
            GameObject bubble = Instantiate(pizzaDrinkBubble, this.gameObject.transform);
            bubble.transform.Translate(new Vector3(-.22f, .6f, 0));
            //bubble.transform.Translate(new Vector3(25, -4));
        }
        // rotate to face the next waypoint
        Vector3 waypoint = path.Waypoint(1);
        Vector3 direction = waypoint - transform.position;
        //transform.rotation = Quaternion.FromToRotation(Vector3.forward, direction);
    }

    void Update()
    {
        Vector3 waypoint = path.Waypoint(nextWaypoint);

        float distanceTravelled = speed * Time.deltaTime;
        float distanceToWaypoint = Vector3.Distance(waypoint, transform.position);

        if (distanceToWaypoint <= distanceTravelled)
        {
            // reached the waypoint, start heading to the next one
            transform.position = waypoint;
            NextWaypoint();
        }
        else
        {
            // move towards waypoint
            Vector3 direction = waypoint - transform.position;
            direction = direction.normalized;
            transform.Translate(direction * distanceTravelled, Space.World);

            // rotate to face waypoint
            //transform.rotation = Quaternion.FromToRotation(Vector3.forward, direction);
        }
    }

    private void NextWaypoint()
    {
        nextWaypoint++;

        // destroy self if we have reached the end of the path.
        if (nextWaypoint == path.Length)
        {
            EnemyHealth enemyHealth = gameObject.GetComponent<EnemyHealth>();

            if (enemyHealth.Health > 0)
            {
                GameManager.Instance.DamageRestaurant(reviewDamage);
            }

            Destroy(gameObject);

            if (isLastEnemy && GameManager.Instance.CurrentRestaurantHealth > 0)
            {
                // win
                GameManager.Instance.GameOver(true);
            }
        }
    }

    //
    // Summary:
    //     set the type of food this enemy wants.
    public void setType(string s)
    {
        this.enemyType = s;
    }

    public string getType()
    {
        return this.enemyType;
    }
    public void setSpecType(string s)
    {
        this.enemySpecType = s;
    }

    public string getSpecType()
    {
        return this.enemySpecType;
    }
}
