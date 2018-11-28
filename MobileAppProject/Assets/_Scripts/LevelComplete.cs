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
    private Text score;

    // Update is called once per frame
    void Update () {
        if (GameManager.Instance.Complete)
        {
            ShowPopUp();
        }
	}
    void ShowPopUp()
    {
        popup.SetActive(true);
        pauseButton.SetActive(false);
        Time.timeScale = 0f;

        ShowScore();
    }
    public void NextLevel()
    {
        SceneManager.LoadScene("LevelTwoScene");
    }
    public void Exit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
    public void Retry()
    {
        SceneManager.LoadScene("LevelOneScene");
    }
    private void ShowScore()
    {
        score.text = "Score: " + GameManager.Instance.Collected.ToString();
    }
}
