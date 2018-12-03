using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;

public class LoadLevelController : MonoBehaviour {

    // function to change levels
    public void ChangeLevel()
    {
        // if current level is 1
        if (GameManager.Instance.CurrLevel == 1)
        {
            // load level 1
            SceneManager.LoadScene(SceneNames.LEVEL_1);
        }
        // if current level is 2
        else if (GameManager.Instance.CurrLevel == 2)
        {
            // load level 2
            SceneManager.LoadScene(SceneNames.LEVEL_2);
        }
        else
        {
            Debug.Log("You have completed all the levels..");
        }
    }
}
