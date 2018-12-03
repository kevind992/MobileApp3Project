using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingScript : MonoBehaviour {

    // When the player comes in contact with a moving platform
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision Detected..");
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("FLoating...");
            // Making the player a child of the platform
            collision.collider.transform.SetParent(transform);
        }
    }
    // When the player jumps off the platform
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Not FLoating...");
            // Removing the player as a child of the platform
            collision.collider.transform.SetParent(null);
        }
    }
}
