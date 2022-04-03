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
    public float setSpeed;
    public string[] foodType = {"burger", "pizza", "drink" };

    private float spawnTime;
    private float speed;
    private State state;

    public float enemiesLeft;

    void SpawnEnemy(GameObject setParent)
    {
        spawnTime -= Time.deltaTime;

        if (spawnTime <= 0 && enemiesLeft > 0)
        {
            int randPath = Random.Range(0, pathArray.Length);
            int randType = Random.Range(0, foodType.Length);
            EnemyMove enemy = Instantiate(Enemy.GetComponent<EnemyMove>());
            enemy.tag = "Enemy";
            enemy.path = pathArray[randPath];
            enemy.speed = speed;
            enemy.setType(foodType[randType]);
            Debug.Log("path: " + enemy.path + ", enemytype: " + enemy.getType());
            spawnTime = setSpawnTime;
            enemiesLeft--;
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
        speed = setSpeed;
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
