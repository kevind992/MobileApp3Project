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

        HandleMovement(horizontal);

        Flip(horizontal);

        HandleWave();
    }

    private void HandleMovement(float horizontal)
    {
        myRigidbody.velocity = new Vector2(horizontal * movementSpeed, myRigidbody.velocity.y);

        myAnimator.SetFloat("speed", Mathf.Abs(horizontal));
    }

    private void HandleWave()
    {
        if (wave)
        {
            myAnimator.SetTrigger("hi");
        }
    }

    private void HandleInput()
    {
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
}
