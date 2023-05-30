using UnityEngine;


public class PlayerMovement : MonoBehaviour
{

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
        }
        rb.velocity = moveForce;

        animator.SetFloat("Horizontal", moveForce.x);
        animator.SetFloat("Vertical", moveForce.y);
        animator.SetFloat("Speed",moveForce.sqrMagnitude);

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
