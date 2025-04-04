using JetBrains.Annotations;
using UnityEngine;
using FirstGearGames.SmoothCameraShaker;
using System;


public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator_;

    public ShakeData cameraShake;

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
    float jumpBufferTime = 0.1f;
    float jumpBufferCounter;


    [Header("SFX")]
    public AudioClip jumpClip;
    public AudioClip hitClip;
    AudioSource audioSource;

    [Header("Bools")]        
    public bool isFacingRight;
    bool isJumping_;

    public ParticleSystem dust_;


    EntityStats entityStats;

    void Awake()
    {
        Time.timeScale = 1f;
        isFacingRight = true;
    }

    void Start()
    {
        animator_ = gameObject.GetComponent<Animator>();
        vecGravity = new Vector2(0, -Physics2D.gravity.y);

        audioSource = GetComponent<AudioSource>();
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
        
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)){
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

        animator_.SetFloat("xVelocity", Math.Abs(rb.linearVelocity.x));
    }

    void Jump()
    {
        if(jumpBufferCounter > 0f &&  coyoteTimeCounter > 0f)
        {
            jumpBufferCounter = 0f;
            isJumping_ = true;
            jumpTime = jumpStartTime;
            dust_.Play();
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, JumpForce);
            
            //rb.AddForce(new Vector2(rb.linearVelocity.x, JumpForce), ForceMode2D.Impulse);
            
        }

        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) && isJumping_)
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
            if(isGrounded()){
                dust_.Play();
            }
        }        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "FlyingEnemy"){
            CameraShakerHandler.Shake(cameraShake);
            audioSource.PlayOneShot(hitClip);
        }
    }
}
