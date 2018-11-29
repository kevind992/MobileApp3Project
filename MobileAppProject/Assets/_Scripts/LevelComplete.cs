using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelComplete : MonoBehaviour {

    [SerializeField]
    private GameObject popup;
    [SerializeField]
    private GameObject pauseButton;
    [SerializeField]
    private GameObject finishPopup;


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
            ShowPopUp();
        }
	}
    void ShowPopUp()
    {
        
        if(GameManager.Instance.CurrLevel == 1)
        {
            popup.SetActive(true);
            pauseButton.SetActive(false);
            Time.timeScale = 0f;

            ShowScore();
        }
        else
        {
            finishPopup.SetActive(true);
            pauseButton.SetActive(false);
            Time.timeScale = 0f;

            CheckScore();

            ShowScore2();
        }
    }
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
    private void CheckScore()
    {
        int checkScore = PlayerPrefs.GetInt("score2");

        if(checkScore < GameManager.Instance.Collected)
        {
            PlayerPrefs.SetInt("score2", GameManager.Instance.Collected);
        }
    }
    public void Exit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
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
