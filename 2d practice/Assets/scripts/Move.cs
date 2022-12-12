using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    //reference for the rigidbody and the player controller
    Rigidbody2D rb;
    PlayerController pc;

    //reference for the deathline gameobject
    [SerializeField]
    GameObject deathline;
  
    //vector to collect users current input
    Vector2 currentInput;
    [SerializeField]
    float jumpCounter;
    float horizontal;
    public float speed = 10.0f;
    float jumpForce = 10.0f;

    bool isGrounded = true;



    private void Awake()
    {
        gameObject.AddComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        pc = new PlayerController();
        onMove();
        pc.Movement.Jump.performed += onJump;

    }

    private void Update()
    {

        //update the movement of the deathline as the player moves
        deathLine();
    }

    private void FixedUpdate()
    {
        //user input in real time
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
       // Jumpcount();

    }


    //handles movement
    private void onMove()
    {
        pc.Movement.Move.started += context =>
        {
            horizontal = context.ReadValue<Vector2>().x; 
        };
        pc.Movement.Move.canceled += context =>
        {
            horizontal = context.ReadValue<Vector2>().x;

        };
         pc.Movement.Move.performed += context =>
        {
            horizontal = context.ReadValue<Vector2>().x;
            

        };
    }


    //handles jumping
    void onJump(InputAction.CallbackContext context) 
    {

         if (context.performed && isGrounded)
         {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCounter += 1;
            isGrounded = false;
         }
        
    }
    
    void Jumpcount()
    {
        if(jumpCounter == 2)
        {
            isGrounded = false;
        }
        isGrounded = true;
    }

    //allows for deathline to follow under player every frame
    void deathLine()
    {
        deathline.transform.position = new Vector2(transform.position.x, deathline.transform.position.y);
    }


    //Ground detection
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            isGrounded = true;
        }
        else
            isGrounded = false;
    }
    
    
    private void OnEnable()
    {
       pc.Movement.Enable();
    }

    private void OnDisable()
    {
        pc.Movement.Disable();
    }
    

}
