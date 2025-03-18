using JetBrains.Annotations;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;

    [Header("Move")]
    public float move_speed;
    public float horizontalMove;
    public bool isFacingRight;


    [Header("Jump")]
    public float JumpForce;
    public LayerMask groundLayer;
    public Transform groundCheck;
    [SerializeField] private float fallMultiplier;
    Vector2 vecGravity;

    EntityStats entityStats;

    void Awake()
    {
        isFacingRight = true;
    }

    void Start()
    {
        vecGravity = new Vector2(0, -Physics2D.gravity.y);

        rb = GetComponent<Rigidbody2D>();
        entityStats = gameObject.GetComponent<EntityStats>();

        move_speed = entityStats.base_speed;
    }

    
    void Update()
    {
        Move();
        Flip();
        Jump();
    }

    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.transform.position, 0.2f, groundLayer);
    }

    void Move()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");

        rb.linearVelocity = new Vector3(horizontalMove * move_speed, rb.linearVelocity.y);
    }

    void Jump()
    {
        if(Input.GetButtonDown("Jump") &&  isGrounded())
        {
            rb.AddForce(new Vector2(rb.linearVelocity.x, JumpForce), ForceMode2D.Impulse);
            
        }

        if(rb.linearVelocity.y < 0 )
        {
            rb.linearVelocity -= vecGravity * fallMultiplier * Time.deltaTime;
        }
    }

    void Flip()
    {
        if(isFacingRight && horizontalMove < 0 || !isFacingRight && horizontalMove > 0)
        {
            isFacingRight = !isFacingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1;
            transform.localScale = ls;
        }
        
    }
}
