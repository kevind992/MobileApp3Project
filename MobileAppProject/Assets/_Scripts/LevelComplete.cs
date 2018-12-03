using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utilities;

public class LevelComplete : MonoBehaviour {

    // Getting an instance of the popup panel, pause button panel and finish popup
    [SerializeField]
    private GameObject popup;
    [SerializeField]
    private GameObject pauseButton;
    [SerializeField]
    private GameObject finishPopup;

    // Score Text boxes
    [SerializeField]
    private Text score1;

    [SerializeField]
    private Text score2;

    LoadLevelController loadLevel;

    void Start()
    {
        loadLevel = new LoadLevelController();   
    }

    // Update is called once per frame
    void Update () {
        if (GameManager.Instance.Complete)
        {
            // if level is complete show popup
            ShowPopUp();
        }
	}
    // function to manage pop up
    void ShowPopUp()
    {   
        // if first level
        if(GameManager.Instance.CurrLevel == 1)
        {
            popup.SetActive(true);
            pauseButton.SetActive(false);
            Time.timeScale = 0f;

            ShowScore();
        }
        // if last level
        else
        {
            finishPopup.SetActive(true);
            pauseButton.SetActive(false);
            Time.timeScale = 0f;

            CheckScore();

            ShowScore2();
        }
    }
    // function to load up next level
    public void NextLevel()
    {
   
        GameManager.Instance.CurrLevel += 1;

        int score = PlayerPrefs.GetInt("score1");

        if(GameManager.Instance.Collected > score)
        {
            PlayerPrefs.SetInt("score1", GameManager.Instance.Collected);
        }
        
        PlayerPrefs.SetInt("level", GameManager.Instance.CurrLevel);

        loadLevel.ChangeLevel();
    }
    // function to manage score on popup
    private void CheckScore()
    {
        // getting score which was saved on file
        int checkScore = PlayerPrefs.GetInt("score2");

        if(checkScore < GameManager.Instance.Collected)
        {
            PlayerPrefs.SetInt("score2", GameManager.Instance.Collected);
        }
    }

    // if the user selects Exit
    public void Exit()
    {
        // restart time
        Time.timeScale = 1f;
        // load main menu
        SceneManager.LoadScene(SceneNames.MAIN_MENU);
    }
    public void Retry()
    {
        loadLevel.ChangeLevel();
    }
    private void ShowScore()
    {
        score1.text = "Score: " + GameManager.Instance.Collected.ToString();
    }
    private void ShowScore2()
    {
        score2.text = "Score: " + GameManager.Instance.Collected.ToString();
    }
}
