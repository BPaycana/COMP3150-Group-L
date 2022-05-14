using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    enum State
    {
        Waiting,
        Spawning,
    }

    public GameObject Enemy;
    public GameObject Parent;
    //public Path path;
    public Path[] pathArray;
    public float maxEnemies;
    public float setSpawnTime;
    //public float setSpeed;
    public float setMinHealth;
    public float setMaxHealth;
    public float setMinSpeed;
    public float setMaxSpeed;

    public string[] foodType = { "burger", "pizza", "soda" };

    private float spawnTime;
    private float speed;
    private State state;
    private float health;
    public float enemiesLeft;
    public int specialEnemyChance = 25;  //between 1-100, percentage. default to 25%
    private int isSpecial;
    private bool specialState;

    void SpawnEnemy(GameObject setParent)
    {
        spawnTime -= Time.deltaTime;

        if (spawnTime <= 0 && enemiesLeft > 0)
        {
            health = Random.Range(setMinHealth, setMaxHealth);
            speed = Random.Range(setMinSpeed, setMaxSpeed);
            isSpecial = (int)Random.Range(1, 100);
            if (isSpecial < specialEnemyChance)
            {
                specialState = true;
            }
            else
            {
                specialState = false;
            }
            if (enemiesLeft == 1)
            {
                int randPath = Random.Range(0, pathArray.Length);
                int randType = Random.Range(0, foodType.Length);
                EnemyMove enemy = Instantiate(Enemy.GetComponent<EnemyMove>());
                enemy.tag = "Enemy";
                enemy.path = pathArray[randPath];
                enemy.speed = speed;
                enemy.GetComponent<EnemyHealth>().targetHealth = health;
                enemy.isLastEnemy = true;
                enemy.setType(foodType[randType]);
                //enemy.GetComponent<EnemyHealth>().specHealthBarBack.enabled = false;
                enemy.GetComponent<EnemyHealth>().specHealthBar.enabled = false;
                enemy.GetComponent<EnemyHealth>().specHealthBarBackground.enabled = false;
                if (specialState)
                {
                    enemy.setSpecType("drink");
                    //enemy.isSpecialEnemy = true;
                    enemy.GetComponent<EnemyHealth>().EnemyIsSpecial();

                    enemy.GetComponent<EnemyHealth>().specHealthBar.enabled = true;
                    enemy.GetComponent<EnemyHealth>().specHealthBarBackground.enabled = true;
                }
                Debug.Log("path: " + enemy.path + ", enemytype: " + enemy.getType() + ", health: " + health + ", speed: " + speed + ", isSpecial: " + specialState + ", isLastEnemy: " + enemy.isLastEnemy);
                spawnTime = setSpawnTime;
                enemiesLeft--;
            }
            else
            {
                int randPath = Random.Range(0, pathArray.Length);
                int randType = Random.Range(0, foodType.Length);
                EnemyMove enemy = Instantiate(Enemy.GetComponent<EnemyMove>());
                enemy.tag = "Enemy";
                enemy.path = pathArray[randPath];
                enemy.speed = speed;
                enemy.GetComponent<EnemyHealth>().targetHealth = health;
                enemy.setType(foodType[randType]);
                enemy.GetComponent<EnemyHealth>().specHealthBar.enabled = false;
                enemy.GetComponent<EnemyHealth>().specHealthBarBackground.enabled = false;
                if (specialState)
                {
                    enemy.setSpecType("drink");
                    //enemy.isSpecialEnemy = true;
                    enemy.GetComponent<EnemyHealth>().EnemyIsSpecial();

                    enemy.GetComponent<EnemyHealth>().specHealthBar.enabled = true;
                    enemy.GetComponent<EnemyHealth>().specHealthBarBackground.enabled = true;
                }
                Debug.Log("path: " + enemy.path + ", enemytype: " + enemy.getType() + ", health: " + health + ", speed: " + speed + ", isSpecial: " + specialState + ", isLastEnemy: " + enemy.isLastEnemy);
                spawnTime = setSpawnTime;
                enemiesLeft--;
            }

        }

    }

    /*    int RandPath(Path[] path)
        {
            int numPaths = path.GetLength();

            return n;
        }*/

    // Start is called before the first frame update
    void Start()
    {
        enemiesLeft = maxEnemies;
        spawnTime = setSpawnTime;
        //speed = setSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Waiting:
                //Debug.Log("Waiting");
                spawnTime -= Time.deltaTime;
                if (spawnTime <= 0)
                {
                    state = State.Spawning;
                }
                break;

            case State.Spawning:
                //Debug.Log("Spawning");
                SpawnEnemy(Parent);
                state = State.Waiting;
                break;
        }
    }
}
