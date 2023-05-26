using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightStarter : MonoBehaviour
{
    public Enemy enemy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Triggered");
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Fight start");
            GameManager.Instance.FightManager.StartFight(enemy);
        }
    }
}
