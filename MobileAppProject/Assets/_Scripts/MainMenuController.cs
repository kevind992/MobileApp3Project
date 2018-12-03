using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utilities;

public class MainMenuController : MonoBehaviour
{
    // Getting a screen panels , textboxs and toggle switch from unity editor
    [SerializeField]
    private GameObject welcomeMessage;

    [SerializeField]
    private Text hsLevel1;

    [SerializeField]
    private Text hsLevel2;

    [SerializeField]
    Toggle tog;

    // if first play
    private string firstPlay = "true";

    void Start()
    {
        Debug.Log("Hello..");
        firstPlay = PlayerPrefs.GetString("played");
        if (firstPlay.Equals("true"))
        {
            PlayerPrefs.SetString("played", "false");
            Debug.Log(firstPlay);
            OpenFirstTimePlay();
        }
    }
    // function to load previously saved game
    public void Load()
    {
        // Getting level which has been saved to file
        int level = PlayerPrefs.GetInt("level");
        
   
        if (level == 1)
        {
            SceneManager.LoadScene(SceneNames.LEVEL_1);
        }
        else if (level == 2)
        {
            SceneManager.LoadScene(SceneNames.LEVEL_2);
        }
        else
        {
            Debug.Log("No Level Saved..");
        }
    }
    public void OpenFirstTimePlay()
    {
        // showing welcome message
        welcomeMessage.SetActive(true);
    }
    public void CloseFirstTimePlay()
    {
        // closing welcome message
        welcomeMessage.SetActive(false);
    }
    // function for handling if the player clicks new game
    public void NewGame()
    {
        // load level 1
        SceneManager.LoadScene(SceneNames.LEVEL_1);
    }
    // fucntion for handing high score
    public void HighScores()
    {
        // Getting both level 1 and 2 score which have been saved on file
        int level1 = PlayerPrefs.GetInt("score1");
        int level2 = PlayerPrefs.GetInt("score2");

        // if score saved is not 0 display score otherwise display 0
        if (level1 != 0)
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
    // function for handling sound options
    public void CheckToggle()
    {
        // check if sound has been previously changed
        string check = PlayerPrefs.GetString("sound");
        

        if (check.Equals("soundon"))
        {
            // music will be on
            tog.isOn = true;
        }
        else
        {
            // music will be off
            tog.isOn = false;
        }
    }


    public void Toggle_Changed(bool isChanged)
    {
        if (isChanged)
        {
            // changing saved string
            PlayerPrefs.SetString("sound", "soundon");
            Debug.Log("Sound is on..");
        }
        else
        {
            // changing saved string
            PlayerPrefs.SetString("sound", "soundoff");
            Debug.Log("Sound is off..");
        }
    }
}

    
