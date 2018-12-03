using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_script : MonoBehaviour {

    private Rigidbody2D myRigidbody;
    private Animator animator;
    private bool direction;
    private bool isGrounded;
    private bool jump;
    private SoundController soundController;

    // === Serializable Variables ===
    [SerializeField]
    private float speed;
    [SerializeField]
    private Transform[] groundPoints;
    [SerializeField]
    private float groundRadius;
    [SerializeField]
    private LayerMask whatIsGround;
    [SerializeField]
    private bool airControl;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private AudioClip eatClip;

    private void Start()
    {
        soundController = SoundController.FindSoundController();

        direction = true;
        myRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
    }

    private void Update()
    {
        // handling movement input
        HandleInput();
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");

        // checking if player is grounded
        isGrounded = IsGrounded();

        // movement function
        HandleMovement(horizontal);

        // change direction function
        ChangeDirection(horizontal);

        // reseting values
        ResetValues();
    }
    // function to handle movement
    private void HandleMovement(float horizontal)
    {
        // optional air control
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
    // Jump function
    private void HandleInput()
    {
        // if spacebar is pressed JUMP
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true; 
        }
    }
    // function to handle character change of direction
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
    // resetting jump back to false
    private void ResetValues()
    {
        jump = false;
    }
    // function for detecting collisions
    private void OnCollisionEnter2D(Collision2D collectable)
    {
        // if the player collides with a good item
        if(collectable.gameObject.tag == "Good-Food")
        {
            // play audio clip
            PlayClip(eatClip);

            // if the player does not have full health
            if (GameManager.Instance.HealthValue < 1F)
            {
                // increment score
                GameManager.Instance.Collected += 100;
                // increase health
                GameManager.Instance.HealthValue += 0.25F;
            }
            else
            {
                // if the player already has full heath just increment score
                GameManager.Instance.Collected += 100;
            }
            // distroy object after colliding into it
            Destroy(collectable.gameObject);
        }
        // if the player collides with a bad item
        else if(collectable.gameObject.tag == "Bad-Food")
        {
            // play audio clip
            PlayClip(eatClip);

            // if the score is 0 do not decrement
            if (GameManager.Instance.Collected <= 0)
            {
                Debug.Log("Not Collecting");
            }
            else // decrement by 100 
            {
                GameManager.Instance.Collected -= 100;
            }
            
            // if health is at 100% reduce to 75%
            if(GameManager.Instance.HealthValue == 1F)
            {
                GameManager.Instance.HealthValue = 0.75F;
            }
            // if health is at 75% reduce to 50%
            else if(GameManager.Instance.HealthValue == 0.75F)
            {
                GameManager.Instance.HealthValue = 0.50F;
            }
            // if health is at 50% reduce to 25%
            else if(GameManager.Instance.HealthValue == 0.50F)
            {
                GameManager.Instance.HealthValue = 0.25F;
            }
            // if health is at 25% player dies and player is respawned
            else
            {
                GameManager.Instance.Respawn();
                GameManager.Instance.HealthValue = 1f;
            }
            // game object is distroyed
            Destroy(collectable.gameObject);
        }

    }

    // function for detecting triggers
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // trigger to detect a fall
        if(collision.tag == "Fall_Detector")
        {
            // respawn
            GameManager.Instance.Respawn();
            // reduce lives
            Destroy(GameManager.Instance.Lives[GameManager.Instance.RemainingLives]);
            GameManager.Instance.RemainingLives++;
            
        }
        // trigger to detect a checkpoint
        if(collision.tag == "Checkpoint")
        {
            // update respawn point
            GameManager.Instance.RespawnPoint = collision.transform.position;
        }
        // tripper to detect if the player finishes a level
        if (collision.tag == "Finish")
        {
            Debug.Log("Level Finished..");
            GameManager.Instance.Level += 1;
            GameManager.Instance.Complete = true;
        }
        // Level one trigger
        if (collision.tag == "level1")
        {
            GameManager.Instance.CurrLevel = 1;
            Debug.Log(GameManager.Instance.CurrLevel);
        }
        // Level two trigger
        if (collision.tag == "level2")
        {
            GameManager.Instance.CurrLevel = 2;
            Debug.Log(GameManager.Instance.CurrLevel);
        }
    }
    // Getting a instance of soundController
    private void PlayClip(AudioClip clip)
    {
        if (soundController)
        {
            // Play on shot of audio clip
            soundController.PlayOneShot(clip);
        }
    }
}