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

    LoadLevelController loadLevel;

    void Start()
    {
        loadLevel = new LoadLevelController();
    }
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
        loadLevel.ChangeLevel();
        GameManager.Instance.ResetValues();
    }
    public void Exit()
    {
        Time.timeScale = 1f;
        popup.SetActive(false);
        SceneManager.LoadScene("MainMenu");
    }
    public void NextLevel()
    {
        loadLevel.ChangeLevel();
    }
}
