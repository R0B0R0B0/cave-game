using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    Transform weapon;

    //Weapon movemnt
    float rotationOffset;

    new Rigidbody2D rigidbody;

    //BY GurbluciDevlogs

    //I recommend 7 for the move speed, and 1.2 for the force damping
    public Rigidbody2D rb;
    public float moveSpeed;
    public Vector2 forceToApply;
    public Vector2 PlayerInput;
    public float forceDamping;
    void Update()
    {
        PlayerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        
    }
    private void LateUpdate()
    {
        RotateHead();
    }
    void FixedUpdate()
    {
        Vector2 moveForce = PlayerInput * moveSpeed;
        moveForce += forceToApply;
        forceToApply /= forceDamping;
        if (Mathf.Abs(forceToApply.x) <= 0.01f && Mathf.Abs(forceToApply.y) <= 0.01f)
        {
            forceToApply = Vector2.zero;
        }
        rb.velocity = moveForce;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            forceToApply += new Vector2(-20, 0);
            Destroy(collision.gameObject);
        }
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
