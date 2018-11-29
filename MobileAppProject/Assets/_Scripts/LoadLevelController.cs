using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelController : MonoBehaviour {

    public void ChangeLevel()
    {
        if (GameManager.Instance.CurrLevel == 1)
        {
            SceneManager.LoadScene("LevelOneScene");
        }
        else if (GameManager.Instance.CurrLevel == 2)
        {
            SceneManager.LoadScene("LevelTwoScene");
        }
        else
        {
            Debug.Log("You have completed all the levels..");
        }
    }
}
