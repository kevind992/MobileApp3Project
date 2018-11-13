using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    private static GameManager instance;

    [SerializeField]
    private Text score;

    private int collected;

    public static GameManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }

            return instance;
        }
    }

    public int Collected
    {
        get
        {
            return collected;
        }

        set
        {
            score.text = value.ToString();
            collected = value;
        }
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
