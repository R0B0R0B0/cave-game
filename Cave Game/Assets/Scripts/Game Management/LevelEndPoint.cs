using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class LevelEndPoint : MonoBehaviour
{
    public int exitID = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.Instance.OnEndPositionTriggered(exitID);
    }
}
