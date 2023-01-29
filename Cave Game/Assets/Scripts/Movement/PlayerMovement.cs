using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    Transform weapon;

    new Rigidbody2D rigidbody;

    [SerializeField]
    float speed = 1;


    //Weapon movemnt
    float rotationOffset;




    // Start is called before the first frame update
    void Start()
    {
        rigidbody= GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(x,y).normalized;

        rigidbody.MovePosition(rigidbody.position + move * speed * Time.fixedDeltaTime);

        RotateHead();
    }

    //or something
    void RotateHead()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z= 0;
        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);

        mousePos.x -= objectPos.x;
        mousePos.y -= objectPos.y;

        float angle = Mathf.Atan2(mousePos.y,mousePos.x) * Mathf.Rad2Deg;
        weapon.rotation = Quaternion.Euler(new Vector3(0, 0, angle + rotationOffset));
    }
}
