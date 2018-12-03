using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    // === Serializable Variables ===
    [SerializeField]
    private Player_script player;
    [SerializeField]
    private Text score;
    private int collected;
    [SerializeField]
    private float respawnDelay;
    [SerializeField]
    private Vector3 respawnPoint;
    [SerializeField]
    private float healthValue;
    [SerializeField]
    private Image content;
    [SerializeField]
    private Image[] lives;

    // === Private Variables ===
    private static GameManager instance;
    private int level;
    private int remainingLives = 0;
    private int currLevel;
    private int currLoad;
    private bool complete;


    // Use this for initialization
    void Start()
    {
        player = FindObjectOfType<Player_script>();
        // Resets the main game settings at the start of every instance being created.
        ResetValues();
        currLevel = 1;
        currLoad = PlayerPrefs.GetInt("level");
    }

    // Update is called once per frame
    void Update()
    {
        // Listening for updates for Health bar and lives
        HandleHealthBar();
        HandleLives();
    }
    // function to handle health bar
    private void HandleHealthBar()
    {
        content.fillAmount = HealthValue;
    }
    // Function to handel Respawning
    public void Respawn()
    {
        // Starting the Coroutine
        StartCoroutine("RespawnCoroutine");
    }
    // Function for checking player lives
    private void HandleLives()
    {
        if (lives[2] == null)
        {
            Debug.Log("Lives is NULL");
            // if player has no more lives end game
            GameOverScript.gameOver = true;

        }
    }
    // Coroutine for managing respawing
    // Put 1 second delay to give player time to gather themselves
    // delay is set in the unity properities
    private IEnumerator RespawnCoroutine()
    {
        // de-activing player
        player.gameObject.SetActive(false);
        // Starting delay
        yield return new WaitForSeconds(RespawnDelay);
        // Setting respawn point
        player.transform.position = GameManager.instance.RespawnPoint;
        // activing player
        player.gameObject.SetActive(true);
        // Setting health to full
        healthValue = 1f;
    }
    // Resetting values
    public void ResetValues()
    {
        GameOverScript.gameOver = false;
        Time.timeScale = 1f;
        healthValue = 1f;
        remainingLives = 0;

    }

    // === Getters and Setters ===
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }

            return instance;
        }
    }

    public int Collected
    {
        get
        {
            return collected;
        }

        set
        {
            Score.text = value.ToString();
            collected = value;
        }
    }

    public float HealthValue
    {
        get
        {
            return healthValue;
        }

        set
        {
            healthValue = value;
        }
    }

    public Vector3 RespawnPoint
    {
        get
        {
            return respawnPoint;
        }

        set
        {
            respawnPoint = value;
        }
    }

    public Image[] Lives
    {
        get
        {
            return lives;
        }

        set
        {
            lives = value;
        }
    }

    public int RemainingLives
    {
        get
        {
            return remainingLives;
        }

        set
        {
            remainingLives = value;
        }
    }

    public float RespawnDelay
    {
        get
        {
            return respawnDelay;
        }

        set
        {
            respawnDelay = value;
        }
    }

    public Text Score
    {
        get
        {
            return score;
        }

        set
        {
            score = value;
        }
    }

    public int Level
    {
        get
        {
            return level;
        }

        set
        {
            level = value;
        }
    }

    public bool Complete
    {
        get
        {
            return complete;
        }

        set
        {
            complete = value;
        }
    }

    public int CurrLevel
    {
        get
        {
            return currLevel;
        }

        set
        {
            currLevel = value;

        }
    }

    public int CurrLoad
    {
        get
        {
            return currLoad;
        }

        set
        {
            currLoad = value;
        }
    }

}
