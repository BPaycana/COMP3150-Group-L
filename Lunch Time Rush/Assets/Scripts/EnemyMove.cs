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
    private SpriteRenderer spriteRenderer;

    public bool isLastEnemy = false;

    void Start()
    {
        // start at waypoint 0
        transform.position = path.Waypoint(0);
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (enemyType == "pizza")
        {
            spriteRenderer.color = Color.yellow;
        }
        if (enemyType == "burger")
        {
            spriteRenderer.color = Color.blue;
        }
        if (enemyType == "drink")
        {
            spriteRenderer.color = Color.black;
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

            if (enemyHealth.Health < enemyHealth.TargetHealth)
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
}
