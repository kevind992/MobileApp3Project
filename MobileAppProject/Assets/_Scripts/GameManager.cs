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
        ResetValues();
        currLevel = 1;
        currLoad = PlayerPrefs.GetInt("level");
    }

    // Update is called once per frame
    void Update()
    {
        HandleHealthBar();
        HandleLives();
    }

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

    private void HandleHealthBar()
    {
        content.fillAmount = HealthValue;
    }
    public void Respawn()
    {
        StartCoroutine("RespawnCoroutine");

    }
    private void HandleLives()
    {
        if (lives[2] == null)
        {
            Debug.Log("Lives is NULL");
            GameOverScript.gameOver = true;

        }
    }
    private IEnumerator RespawnCoroutine()
    {
        player.gameObject.SetActive(false);
        yield return new WaitForSeconds(RespawnDelay);
        player.transform.position = GameManager.instance.RespawnPoint;
        player.gameObject.SetActive(true);
        healthValue = 1f;
    }
    public void ResetValues()
    {
        GameOverScript.gameOver = false;
        Time.timeScale = 1f;
        healthValue = 1f;
        remainingLives = 0;

    }
}
