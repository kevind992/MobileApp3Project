using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_script : MonoBehaviour {

    private Rigidbody2D myRigidbody;

    private Animator animator;

    [SerializeField]
    private float speed;

    private bool direction;

    [SerializeField]
    private Transform[] groundPoints;

    [SerializeField]
    private float groundRadius;

    [SerializeField]
    private LayerMask whatIsGround;

    private bool isGrounded;

    private bool jump;

    [SerializeField]
    private bool airControl;

    [SerializeField]
    private float jumpForce;

    private void Start()
    {
        direction = true;
        myRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        HandleInput();
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");

        isGrounded = IsGrounded();

        HandleMovement(horizontal);

        ChangeDirection(horizontal);

        ResetValues();
    }

    private void HandleMovement(float horizontal)
    {
        if(isGrounded || airControl)
        {
            myRigidbody.velocity = new Vector2(horizontal * speed, myRigidbody.velocity.y);
        }

        if (isGrounded && jump)
        {
            isGrounded = false;
            myRigidbody.AddForce(new Vector2(0, jumpForce));
        }

        animator.SetFloat("speed", Mathf.Abs(horizontal));
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true; 
        }
    }

    private void ChangeDirection(float horizontal)
    {
        if(horizontal > 0 && !direction || horizontal < 0 && direction)
        {
            direction = !direction;

            Vector3 theScale = transform.localScale;
            theScale.x *= -1;

            transform.localScale = theScale;
        }
    }
    private bool IsGrounded()
    {
        if( myRigidbody.velocity.y <= 0)
        {
            foreach(Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);

                for(int i = 0; i < colliders.Length; i++)
                {
                    if(colliders[i].gameObject != gameObject)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private void ResetValues()
    {
        jump = false;
    }

    private void OnCollisionEnter2D(Collision2D collectable)
    {
        if(collectable.gameObject.tag == "Good-Food")
        {
            GameManager.Instance.Collected += 100;
            Destroy(collectable.gameObject);
        }
        else if(collectable.gameObject.tag == "Bad-Food")
        {
            GameManager.Instance.Collected -= 100;

            if(GameManager.Instance.HealthValue == 1F)
            {
                GameManager.Instance.HealthValue = 0.75F;
            }
            else if(GameManager.Instance.HealthValue == 0.75F)
            {
                GameManager.Instance.HealthValue = 0.50F;
            }
            else if(GameManager.Instance.HealthValue == 0.50F)
            {
                GameManager.Instance.HealthValue = 0.25F;
            }
            else
            {
                GameManager.Instance.HealthValue = 0F;
            }

            Destroy(collectable.gameObject);
        }
    }
}