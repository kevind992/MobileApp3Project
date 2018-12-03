using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour {

    // == Checkpoint sprites ==
    public Sprite cpOrange;
    public Sprite cpYellow;

    // == Sprite Renderer ==
    private SpriteRenderer cpSpriteRenderer;

    // == public variables == 
    public bool cpReached;

	// Use this for initialization
	void Start () {
        cpSpriteRenderer = GetComponent<SpriteRenderer>();
	}
    void OnTriggerEnter2D(Collider2D collision)
    {
        // detecting a collision between the player and the checkpoint
        if(collision.tag == "Player")
        {
            // Changing checkpoint colour
            cpSpriteRenderer.sprite = cpYellow;
            cpReached = true;
        }
    }
}
