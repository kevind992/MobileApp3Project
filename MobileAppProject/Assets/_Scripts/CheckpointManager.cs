using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour {

    public Sprite cpOrange;
    public Sprite cpYellow;
    private SpriteRenderer cpSpriteRenderer;
    public bool cpReached;

	// Use this for initialization
	void Start () {
        cpSpriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            cpSpriteRenderer.sprite = cpYellow;
            cpReached = true;
        }
    }
}
