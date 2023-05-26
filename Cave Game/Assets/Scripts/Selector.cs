using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour
{
    Usable usable = null;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Select();
        }
    }

    void Select()
    {
        usable.Use();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if(collision.gameObject.GetComponent<Usable>())
        {
            usable = collision.gameObject.GetComponent<Usable>();
        }
    }
}
