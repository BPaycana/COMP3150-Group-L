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
    public Animator animator;

    public GameObject pizzaBubble;
    public GameObject burgerBubble;
    public GameObject pizzaDrinkBubble;
    public GameObject burgerDrinkBubble;
    public GameObject smileBubble;

    private string bubbleType;

    public bool isLastEnemy = false;


    void Start()
    {
        // start at waypoint 0
        transform.position = path.Waypoint(0);
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (enemyType == "pizza" && enemySpecType != "drink")
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = pizzaGirl;
            animator.SetBool("isPizza", true);
            GameObject bubble = Instantiate(pizzaBubble, this.gameObject.transform);
            bubble.transform.Translate(new Vector3(-.22f, .6f, 0));
            bubbleType = "pizza";
            //bubble.transform.Translate(new Vector3(25, -4));
        }
        if (enemyType == "burger" && enemySpecType != "drink")
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = burgerMan;
            animator.SetBool("isBurger", true);
            GameObject bubble = Instantiate(burgerBubble, this.gameObject.transform);
            //bubble.transform.position = new Vector3(-2, 6, 0);
            bubble.transform.Translate(new Vector3(-.22f, .6f, 0));
            bubbleType = "burger";
            //bubble.transform.Translate(new Vector3(25, -4));
        }
        if (enemySpecType == "drink" && enemyType == "burger")
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = drinkMan;
            animator.SetBool("isBurger", false);
            animator.SetBool("isPizza", false);
            animator.SetBool("isDrink", true);
            GameObject bubble = Instantiate(burgerDrinkBubble, this.gameObject.transform);
            bubble.transform.Translate(new Vector3(-.22f, .6f, 0));
            bubbleType = "burgerdrink";
            //bubble.transform.Translate(new Vector3(25, -4));
        }
        if (enemySpecType == "drink" && enemyType == "pizza")
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = drinkMan;
            GameObject bubble = Instantiate(pizzaDrinkBubble, this.gameObject.transform);
            bubble.transform.Translate(new Vector3(-.22f, .6f, 0));
            bubbleType = "pizzadrink";
            //bubble.transform.Translate(new Vector3(25, -4));
        }
        GameObject bubble2 = Instantiate(smileBubble, this.gameObject.transform);
        bubble2.transform.Translate(new Vector3(-.22f, .6f, 0));
        bubble2.SetActive(false);
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

            animator.SetFloat("Horizontal", direction.x);
            animator.SetFloat("Vertical", direction.y);
            animator.SetFloat("Speed", direction.sqrMagnitude);

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
