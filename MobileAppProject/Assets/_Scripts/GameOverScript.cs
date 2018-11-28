using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour {

    public static bool gameOver = false;

    [SerializeField]
    private GameObject popup;
    [SerializeField]
    private GameObject pauseButton;
	
	// Update is called once per frame
	void Update () {
        if (gameOver)
        {
            GameOverPopUp();
        }
	}

    private void GameOverPopUp()
    {
        popup.SetActive(true);
        pauseButton.SetActive(false);
        Time.timeScale = 0f;
        gameOver = true;
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        popup.SetActive(false);
        pauseButton.SetActive(true);
        SceneManager.LoadScene("LevelOneScene");
    }
    public void Exit()
    {
        Time.timeScale = 1f;
        popup.SetActive(false);
        SceneManager.LoadScene("MainMenu");
    }
    public void NextLevel()
    {
        if (GameManager.Instance.Level == 1)
        {
            SceneManager.LoadScene("LevelTwoScene");
        }
        else
        {
            Debug.Log("You have completed all the levels..");
        }
    }
}
