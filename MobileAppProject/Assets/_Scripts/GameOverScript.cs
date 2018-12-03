using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;

// A script for managing the game over functionality
public class GameOverScript : MonoBehaviour {

    public static bool gameOver = false;

    // Getting an instance of the popup panel and pause button panel
    [SerializeField]
    private GameObject popup;
    [SerializeField]
    private GameObject pauseButton;

    LoadLevelController loadLevel;

    void Start()
    {
        loadLevel = new LoadLevelController();
    }
    // Update is called once per frame
    void Update () {
        // checking to see whether the game is over
        if (gameOver)
        {
            // if over run GameOverPopUp function
            GameOverPopUp();
        }
	}
    // function that activates pop up
    private void GameOverPopUp()
    {
        popup.SetActive(true);
        pauseButton.SetActive(false);
        // turning off time
        Time.timeScale = 0f;
        gameOver = true;
    }
    // if the user selects the retry button
    public void Retry()
    {
        // Resetting values
        Time.timeScale = 1f;
        popup.SetActive(false);
        pauseButton.SetActive(true);
        // loading level
        loadLevel.ChangeLevel();
        GameManager.Instance.ResetValues();
    }
    // if the user selects Exit for the pop up menu
    public void Exit()
    {
        Time.timeScale = 1f;
        popup.SetActive(false);
        SceneManager.LoadScene(SceneNames.MAIN_MENU);
    }
    // if the user selects next level
    public void NextLevel()
    {
        // change level
        loadLevel.ChangeLevel();
    }
}
