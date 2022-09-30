using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovement : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D rb;
    Animator animateplayer;
    CapsuleCollider2D mycapsule;
    bool isAlive=true;
    
    
    BoxCollider2D myFeet;
    [SerializeField] float runSpeed  = 10f;
    [SerializeField]float jumpSpeed = 5f;
    [SerializeField]float climbSpeed = 5f;
    [SerializeField] Vector2 deathkick = new Vector2 (10f, 10f);
    float gravityAtStart;
    [SerializeField] GameObject Bullet;
    [SerializeField] Transform gun;

    void Start()
    {
    
        rb = GetComponent<Rigidbody2D>();
        animateplayer = GetComponent<Animator>();
        mycapsule = GetComponent<CapsuleCollider2D>();
        gravityAtStart = rb.gravityScale;
        myFeet = GetComponent<BoxCollider2D>();
    }

   
    void Update()
    {
        if(!isAlive){return;}
        Run();
        flipSprite();
        climb();
        Die();
    }

    void OnMove(InputValue value)
    {
         if(!isAlive){return;}
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    void Run()
    {

        Vector2 playerVelocity = new Vector2(moveInput.x*runSpeed, rb.velocity.y);
        rb.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        animateplayer.SetBool("isRunning", playerHasHorizontalSpeed);
    }

    void flipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2 (Mathf.Sign(rb.velocity.x), 1f);
        }
    }

    void OnJump(InputValue value )
    {
        if(!isAlive){return;}
        if(!myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"))){return;}
        
        if(value.isPressed)
        {
            rb.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    void climb()
    {
        if(!myFeet.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            rb.gravityScale = gravityAtStart;
            animateplayer.SetBool("isClimbing", false);
            return;
        }
        Vector2 climbVelocity = new Vector2(rb.velocity.x, moveInput.y*climbSpeed);
        rb.velocity = climbVelocity;
        rb.gravityScale = 0f;

        bool playerVerticalSpeed = Mathf.Abs(rb.velocity.y) > Mathf.Epsilon;
        animateplayer.SetBool("isClimbing", playerVerticalSpeed);
    }

    void Die()
    {
        if(mycapsule.IsTouchingLayers(LayerMask.GetMask("enemies", "Hazards")))
        {
            isAlive = false;
            animateplayer.SetTrigger("dying");
            rb.velocity =  deathkick;
            FindObjectOfType<gameSession>().ProcessPlayerDeath();
        }
    }
    
    void OnFire(InputValue value)
    {
        if(!isAlive){return;}
        Instantiate(Bullet, gun.position, transform.rotation);
        
        
    }
}
