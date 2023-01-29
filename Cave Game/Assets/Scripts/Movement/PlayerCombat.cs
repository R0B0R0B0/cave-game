using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Transform attackpoint;
    public float  attackRange;
    public LayerMask enemyLayers;
    public float health = 10;
    public float damage  = 1;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }


    public void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackpoint.position + new Vector3(attackRange,0,0f), 1.5f,enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().Damage(damage);
        }
    }

}
