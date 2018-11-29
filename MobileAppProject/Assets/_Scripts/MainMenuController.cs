using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{

    [SerializeField]
    private GameObject welcomeMessage;

    [SerializeField]
    private Text hsLevel1;

    [SerializeField]
    private Text hsLevel2;

    [SerializeField]
    Toggle tog;

    private string firstPlay = "true";

    void Start()
    {
        firstPlay = PlayerPrefs.GetString("played");
        if (firstPlay.Equals("true"))
        {
            PlayerPrefs.SetString("played", "false");
            StartCoroutine("Display");
        }
    }

    public void Load()
    {
        int level = PlayerPrefs.GetInt("level");

        if (level == 1)
        {
            SceneManager.LoadScene("LevelOneScene");
        }
        else if (level == 2)
        {
            SceneManager.LoadScene("LevelTwoScene");
        }
        else
        {
            Debug.Log("No Level Saved..");
        }
    }
    private IEnumerator Display()
    {
        yield return new WaitForSeconds(05);
        OpenFirstTimePlay();
    }
    public void OpenFirstTimePlay()
    {
        welcomeMessage.SetActive(true);
    }
    public void CloseFirstTimePlay()
    {
        welcomeMessage.SetActive(false);
    }

    public void NewGame()
    {
        SceneManager.LoadScene("LevelOneScene");
    }
    public void HighScores()
    {
        int level1 = PlayerPrefs.GetInt("score1");
        int level2 = PlayerPrefs.GetInt("score2");

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

    public void CheckToggle()
    {
        string check = PlayerPrefs.GetString("sound");

        if (check.Equals("soundon"))
        {
            tog.isOn = true;
        }
        else
        {
            tog.isOn = false;
        }
    }


    public void Toggle_Changed(bool isChanged)
    {
        if (isChanged)
        {
            PlayerPrefs.SetString("sound", "soundon");
            Debug.Log("Sound is on..");
        }
        else
        {
            PlayerPrefs.SetString("sound", "soundoff");
            Debug.Log("Sound is off..");
        }
    }
}

    
