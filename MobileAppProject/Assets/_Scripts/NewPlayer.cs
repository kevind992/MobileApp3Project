using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayer : MonoBehaviour {

    private Rigidbody2D myRigidbody;

    [SerializeField]
    private float movementSpeed;

    private bool faceDirection;

    // Use this for initialization
    void Start()
    {
        faceDirection = true;
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        float horizontal = Input.GetAxis("Horizontal");
        Debug.Log(horizontal);

        HandleMovement(horizontal);

        Flip(horizontal);
    }

    private void HandleMovement(float horizontal)
    {
        myRigidbody.velocity = new Vector2(horizontal * movementSpeed, myRigidbody.velocity.y);
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
