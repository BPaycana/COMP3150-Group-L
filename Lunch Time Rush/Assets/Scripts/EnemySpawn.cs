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
    private float specHealth;
    public float enemiesLeft;
    public int specialEnemyChance = 25;  //between 1-100, percentage. default to 25%
    private int isSpecial;
    private bool specialState;
    private string curFoodType;

    public GameObject enemyContainer;

    void SpawnEnemy(GameObject setParent)
    {
        spawnTime -= Time.deltaTime;

        if (spawnTime <= 0 && enemiesLeft > 0)
        {
            health = (int) Random.Range(setMinHealth, setMaxHealth);
            specHealth = (int)Random.Range(setMinHealth, setMaxHealth);
            speed = Random.Range(setMinSpeed, setMaxSpeed);
            isSpecial = (int)Random.Range(1, 100);
            if (isSpecial < specialEnemyChance)
            {
                specialState = true;
            }
            if (enemiesLeft == 1)
            {
                int randPath = Random.Range(0, pathArray.Length);
                int randType = Random.Range(0, foodType.Length);
                EnemyMove enemy = Instantiate(Enemy.GetComponent<EnemyMove>(), enemyContainer.transform);
                enemy.tag = "Enemy";
                enemy.path = pathArray[randPath];
                enemy.speed = speed;
                enemy.GetComponent<EnemyHealth>().targetHealth = health;
                enemy.isLastEnemy = true;
                curFoodType = foodType[randType];
                enemy.setType(curFoodType);
                //enemy.GetComponent<EnemyHealth>().specHealthBarBack.enabled = false;
                enemy.GetComponent<EnemyHealth>().specHealthBar.enabled = false;
                enemy.GetComponent<EnemyHealth>().specHealthBarBackground.enabled = false;
                if (specialState)
                {
                    enemy.setSpecType("drink");
                    //enemy.isSpecialEnemy = true;
                    enemy.GetComponent<EnemyHealth>().EnemyIsSpecial();
                    //Debug.Log(enemy.getSpecType());

                    enemy.GetComponent<EnemyHealth>().targetSpecHealth = specHealth;
                    enemy.GetComponent<EnemyHealth>().specHealthBar.enabled = false;
                    enemy.GetComponent<EnemyHealth>().specHealthBarBackground.enabled = false;
                }
                Debug.Log("path: " + enemy.path + ", enemytype: " + enemy.getType() + ", health: " + health + ", speed: " + speed + ", isSpecial: " + specialState + ", isLastEnemy: " + enemy.isLastEnemy);
                spawnTime = setSpawnTime;

                // if gameMode == true
                enemiesLeft--;
            }
            else
            {
                int randPath = Random.Range(0, pathArray.Length);
                int randType = Random.Range(0, foodType.Length);
                EnemyMove enemy = Instantiate(Enemy.GetComponent<EnemyMove>(), enemyContainer.transform);
                enemy.tag = "Enemy";
                enemy.path = pathArray[randPath];
                enemy.speed = speed;
                enemy.GetComponent<EnemyHealth>().targetHealth = health;
                curFoodType = foodType[randType];
                enemy.setType(curFoodType);
                enemy.GetComponent<EnemyHealth>().specHealthBar.enabled = false;
                enemy.GetComponent<EnemyHealth>().specHealthBarBackground.enabled = false;
                if (specialState)
                {
                    enemy.setSpecType("drink");
                    //enemy.isSpecialEnemy = true;
                    enemy.GetComponent<EnemyHealth>().EnemyIsSpecial();
                    //Debug.Log(enemy.getSpecType());

                    enemy.GetComponent<EnemyHealth>().targetSpecHealth = specHealth;
                    enemy.GetComponent<EnemyHealth>().specHealthBar.enabled = false;
                    enemy.GetComponent<EnemyHealth>().specHealthBarBackground.enabled = false;
                }
                Debug.Log("path: " + enemy.path + ", enemytype: " + enemy.getType() + ", health: " + health + ", speed: " + speed + ", isSpecial: " + specialState + ", isLastEnemy: " + enemy.isLastEnemy + "|||| spechealth" + specHealth);
                spawnTime = setSpawnTime;
                // if gameMode == true
                enemiesLeft--;
            }
            specialState = false;
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
