using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {

    [SerializeField]
    private Text hsLevel1;

    [SerializeField]
    private Text hsLevel2;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Load()
    {
        int level = PlayerPrefs.GetInt("level");

        if(level == 1)
        {
            SceneManager.LoadScene("LevelOneScene");
        }
        else if(level == 2)
        {
            SceneManager.LoadScene("LevelTwoScene");
        }
        else
        {
            Debug.Log("No Level Saved..");
        }
    }
    public void NewGame()
    {
        SceneManager.LoadScene("LevelOneScene");
    }
    public void HighScores()
    {
        int level1 = PlayerPrefs.GetInt("score1");
        int level2 = PlayerPrefs.GetInt("score2");

        if(level1 != 0)
        {
            hsLevel1.text = "Level 1 Highscore: " + level1.ToString();
        }
        else
        {
            hsLevel1.text = "Level 1 Highscore: 0";
        }
        if (level2 != 0)
        {
            hsLevel2.text = "Level 2 Highscore: " + level2.ToString();
        }
        else
        {
            hsLevel2.text = "Level 2 Highscore: 0";
        }
    }
}
