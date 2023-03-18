using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public float health;

    public void Damage(float damage)
    {
        health -= damage;
        Debug.Log(name + " " + health);

        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
