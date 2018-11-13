using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour {

    [SerializeField]
    private float healthValue;

    [SerializeField]
    private Image content;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        HandleHealthBar();
	}

    private void HandleHealthBar()
    {
        content.fillAmount = healthValue;
    }
}
