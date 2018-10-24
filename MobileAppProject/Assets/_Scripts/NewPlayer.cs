using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayer : MonoBehaviour {

    private Rigidbody2D myRigidbody;

    private Animator myAnimator;

    [SerializeField]
    private float movementSpeed;

    private bool faceDirection;

    private bool wave;

    [SerializeField]
    private Transform[] groundPoints;

    [SerializeField]
    private float groundRadius;

    [SerializeField]
    private LayerMask whatIsGround;

    private bool isGrounded;

    private bool jump;

    [SerializeField]
    private float jumpForce;

    [SerializeField]
    private bool airControl;

    // Use this for initialization
    void Start()
    {
        faceDirection = true;
        myAnimator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleInput();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        float horizontal = Input.GetAxis("Horizontal");
        Debug.Log(horizontal);

        isGrounded = IsGrounded();

        HandleMovement(horizontal);

        Flip(horizontal);

        HandleWave();

        HandleLayers();

        ResetValues();
    }

    private void HandleMovement(float horizontal)
    {
        if (IsFalling)
        {
            gameObject.layer = 11;
        }
        if (!this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Wave") && (isGrounded || airControl))
        {
            myRigidbody.velocity = new Vector2(horizontal * movementSpeed, myRigidbody.velocity.y);
        }
        if (isGrounded && jump)
        {
            isGrounded = false;
            myRigidbody.AddForce(new Vector2(0, jumpForce));
            myAnimator.SetTrigger("Jump");
        }

        

        myAnimator.SetFloat("speed", Mathf.Abs(horizontal));
    }

    private void HandleWave()
    {
        if (wave && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Wave"))
        {
            myAnimator.SetTrigger("Wave");
            myRigidbody.velocity = Vector2.zero;
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !IsFalling)
        {
            jump = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            wave = true;
        }
    }

    private void Flip(float horizontal)
    {
        if(horizontal > 0 && !faceDirection || horizontal < 0 && faceDirection)
        {
            faceDirection =  !faceDirection;

            Vector3 theScale = transform.localScale;

            theScale.x *= -1;

            transform.localScale = theScale;
        }
    }
    private void ResetValues()
    {
        wave = false;
        jump = false;
    }

    private bool IsGrounded()
    {
        if (myRigidbody.velocity.y <= 0)
        {
            foreach (Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);
                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject)
                    {
                        myAnimator.ResetTrigger("Jump");
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private void HandleLayers()
    {
        if (!isGrounded)
        {
            myAnimator.SetLayerWeight(1, 1);
        }
        else
        {
            myAnimator.SetLayerWeight(1, 0);
        }
    }

    public bool IsFalling
    {
        get
        {
            return myRigidbody.velocity.y < 0;
        }
    }
}
