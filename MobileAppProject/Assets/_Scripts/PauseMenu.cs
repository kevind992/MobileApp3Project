using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;

public class PauseMenu : MonoBehaviour {

    public static bool gameIsPaused = false;

    // Getting an instace of the panels
    [SerializeField]
    private GameObject pauseMenuUI;
    [SerializeField]
    private GameObject pauseButton;

	// Update is called once per frame
	void Update () {
        // if the user selects the escape key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // if the game is already paused resume
            if (gameIsPaused)
            {
                Resume();
            }
            // else pause
            else
            {
                Pause();
            }
        }
	}
    // function which handles resume
    public void Resume()
    {
        // deactivate menu and reactivate pause menu
        pauseMenuUI.SetActive(false);
        pauseButton.SetActive(true);
        // restart game time
        Time.timeScale = 1f;
        gameIsPaused = false;
    }
    // function which handles pause
    public void Pause()
    {
        // activate menu
        pauseMenuUI.SetActive(true);
        // deactiveate pause button
        pauseButton.SetActive(false);
        // stop game time
        Time.timeScale = 0f;
        gameIsPaused = true;
    }
    // saving current level
    public void Save()
    {
        PlayerPrefs.SetInt("level",GameManager.Instance.CurrLevel);
    }
    // exiting back to the main menu
    public void Exit()
    {
        // restart game time
        Time.timeScale = 1f;
        // load main menu
        SceneManager.LoadScene(SceneNames.MAIN_MENU);
    }
}
