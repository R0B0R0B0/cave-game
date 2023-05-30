using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pellet : MonoBehaviour
{
    public float lifeTime = 2f;
    bool useLifeTime;

    private IEnumerator Start()
    {
        if (!useLifeTime) { StopAllCoroutines(); }
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger");
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.GetComponent<FightManager>().PelletHit();
        }
    }
}
