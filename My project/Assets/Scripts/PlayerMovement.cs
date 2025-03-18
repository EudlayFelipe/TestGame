using JetBrains.Annotations;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;

    [Header("Move")]
    public float move_speed;
    public float horizontalMove;
    

    [Header("Jump")]
    public float JumpForce;
    public LayerMask groundLayer;
    public Transform groundCheck;
    [SerializeField] private float fallMultiplier;
    Vector2 vecGravity;
    public float jumpStartTime;
    float jumpTime;
    float coyoteTime = 0.15f;
    float coyoteTimeCounter;
    float jumpBufferTime = 0.2f;
    float jumpBufferCounter;


    [Header("SFX")]
    public AudioClip jumpClip;
    AudioSource jumpSound;

    [Header("Bools")]        
    public bool isFacingRight;
    bool isJumping_;


    EntityStats entityStats;

    void Awake()
    {
        isFacingRight = true;
    }

    void Start()
    {
        vecGravity = new Vector2(0, -Physics2D.gravity.y);

        jumpSound = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        entityStats = gameObject.GetComponent<EntityStats>();

        move_speed = entityStats.base_speed;
    }

    
    void Update()
    {
        Move();
        Flip();
        if(isGrounded()){
            coyoteTimeCounter = coyoteTime;
        }
        else{
            coyoteTimeCounter -= Time.deltaTime;
        }
        
        if(Input.GetButtonDown("Jump")){
            jumpBufferCounter = jumpBufferTime;
        }
        else{
            jumpBufferCounter -= Time.deltaTime;
        }

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
        if(jumpBufferCounter > 0f &&  coyoteTimeCounter > 0f)
        {
            jumpBufferCounter = 0f;
            isJumping_ = true;
            jumpTime = jumpStartTime;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, JumpForce);
            
            //rb.AddForce(new Vector2(rb.linearVelocity.x, JumpForce), ForceMode2D.Impulse);
            
        }

        if(Input.GetButton("Jump") && isJumping_)
        {
            if(jumpTime > 0)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, JumpForce);
                jumpTime -= Time.deltaTime;
            }
            else{
                isJumping_ = false;
                coyoteTimeCounter = 0f;
            }
        }

        if(Input.GetButtonUp("Jump")){
            isJumping_ = false;
            coyoteTimeCounter = 0f;
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
