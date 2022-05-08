using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Transform target;

    public float bulletStrength = 50f;
    
    public float speed = 50f;
    public void Seek(Transform _target){
        target = _target;

    }
    public string bulletType;


    // Update is called once per frame
    void Update()
    {
        if(target == null){
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if(dir.magnitude <= distanceThisFrame)
        {
            HitTarget(target);
            return;
        }

        transform.Translate (dir.normalized * distanceThisFrame, Space.World);
 
    }

    void HitTarget(Transform target){

        EnemyHealth enemy = target.GetComponent<EnemyHealth>();

        // get the enemy type, if it matches the bullet type then deal damage, if not dont deal damage
        EnemyMove enemyMove = target.GetComponent<EnemyMove>();

        string enemyType = enemyMove.getType();
        string enemySpecType = enemyMove.getSpecType();

        if (string.Equals(bulletType, enemyType)){
            enemy.TakeDamage(bulletStrength, bulletType);
        }

        if(string.Equals(bulletType, enemySpecType))
        {
            enemy.SpecTakeDamage(bulletStrength, bulletType);
        }

        Destroy(gameObject);
        Debug.Log("Hit something for damage: " + bulletStrength);
    }
}
