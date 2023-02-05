using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;

    public void Damage(float damage)
    {
        health -= damage;
        Debug.Log(name);
    }
}
