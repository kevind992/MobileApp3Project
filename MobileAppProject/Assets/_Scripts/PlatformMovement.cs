using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{

    private Vector3 posA;

    private Vector3 posB;

    private Vector3 nexPosition;

    [SerializeField]
    private float speed;

    [SerializeField]
    private Transform childTransform;

    [SerializeField]
    private Transform transformB;


	// Use this for initialization
	void Start () {
        posA = childTransform.localPosition;
        posB = transformB.localPosition;
        nexPosition = posB;
	}
	
	// Update is called once per frame
	void Update () {
        Move();
	}

    private void Move()
    {
        childTransform.localPosition = Vector3.MoveTowards(childTransform.localPosition, nexPosition, speed * Time.deltaTime);
        if (Vector3.Distance(childTransform.localPosition,nexPosition) <= 0.1)
        {
            ChangeDestination();
        }

    }
    private void ChangeDestination()
    {
        nexPosition = nexPosition != posA ? posA : posB;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.collider.transform.SetParent(null);
        }
    }

}
