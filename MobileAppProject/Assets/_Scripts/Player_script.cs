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

    [SerializeField]
    private AudioClip eatClip;

    private SoundController soundController;

    private void Start()
    {
        soundController = SoundController.FindSoundController();

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
            PlayClip(eatClip);

            if (GameManager.Instance.HealthValue < 1F)
            {
                GameManager.Instance.Collected += 100;
                GameManager.Instance.HealthValue += 0.25F;
            }
            else
            {
                GameManager.Instance.Collected += 100;
            }
            Destroy(collectable.gameObject);
        }
        else if(collectable.gameObject.tag == "Bad-Food")
        {
            PlayClip(eatClip);

            if (GameManager.Instance.Collected <= 0)
            {
                Debug.Log("Not Collecting");
            }
            else
            {
                GameManager.Instance.Collected -= 100;
            }
            
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
                GameManager.Instance.Respawn();
                GameManager.Instance.HealthValue = 1f;
            }
            Destroy(collectable.gameObject);
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Fall_Detector")
        {
            GameManager.Instance.Respawn();
            Destroy(GameManager.Instance.Lives[GameManager.Instance.RemainingLives]);
            GameManager.Instance.RemainingLives++;
            
        }
        if(collision.tag == "Checkpoint")
        {
            GameManager.Instance.RespawnPoint = collision.transform.position;
        }
        if (collision.tag == "Finish")
        {
            Debug.Log("Level Finished..");
            GameManager.Instance.Level += 1;
            GameManager.Instance.Complete = true;
        }
        if (collision.tag == "level1")
        {
            GameManager.Instance.CurrLevel = 1;
            Debug.Log(GameManager.Instance.CurrLevel);
        }
        if (collision.tag == "level2")
        {
            GameManager.Instance.CurrLevel = 2;
            Debug.Log(GameManager.Instance.CurrLevel);
        }
    }
    private void PlayClip(AudioClip clip)
    {
        if (soundController)
        {
            soundController.PlayOneShot(clip);
        }
    }
}