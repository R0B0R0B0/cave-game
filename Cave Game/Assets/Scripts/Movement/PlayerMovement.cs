using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public enum PlayerDirection
{
    Up,Right,Down,Left
}

public class PlayerMovement : MonoBehaviour
{
    //Rotation
    PlayerDirection faceDirection;

    //BY GurbluciDevlogs :)

    //I recommend 7 for the move speed, and 1.2 for the force damping
    public Rigidbody2D rb;
    public float moveSpeed;
    public Vector2 forceToApply;
    public Vector2 PlayerInput;
    public float forceDamping;

    public Animator animator;
    void Update()
    {
        PlayerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;  
    }
    void FixedUpdate()
    {
        Vector2 moveForce = PlayerInput * moveSpeed;
        moveForce += forceToApply;
        forceToApply /= forceDamping;
        if (Mathf.Abs(forceToApply.x) <= 0.01f && Mathf.Abs(forceToApply.y) <= 0.01f)
        {
            forceToApply = Vector2.zero;
            animator.SetBool("IsMoving", false);
        }
        else
        {
            animator.SetBool("IsMoving", true);
        }
        rb.velocity = moveForce;

        if(moveForce != Vector2.zero)
        {
            faceDirection = (moveForce.x, moveForce.y)
switch
            {
                (0, 1) => PlayerDirection.Up,
                (0, -1) => PlayerDirection.Down,
                (1, 0) => PlayerDirection.Right,
                (-1, 0) => PlayerDirection.Left,
                _ => PlayerDirection.Right,
            };
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            forceToApply += collision.relativeVelocity.normalized * 10;
            Destroy(collision.gameObject);
        }
    }
}
