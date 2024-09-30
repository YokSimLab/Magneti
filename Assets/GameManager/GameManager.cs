using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    #region Singleton

    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance is null)
            {
                Debug.LogError("Game manager is null");
            }

            return _instance;
        }
    }

    #endregion

    [SerializeField] GameObject endScreen;
    private EndScreenScoreDisplay endScreenScoreDisplay;
    
    [Range(1, 133420)]
    [SerializeField] public int seed = 0;
    
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] GasManager gasManager;
    [SerializeField] GameObject Chunk;
    public GameObject chunkList;

    [SerializeField] Button resetButton;
    [SerializeField] TutorialMenu tutorialMenu;
    [SerializeField] GameObject[] hideableObjects;
    [HideInInspector] public bool isGameFail = false;
    [HideInInspector] public int distanceFromCenter = 0;
    [HideInInspector] private int maxDistanceFromCenter = 0;
    
    private Vector3 initialLocation;
    
    public delegate void GameContinueDelegate();

    public static GameContinueDelegate onGameContinue;

    private void Awake()
    {
        _instance = this;

        onGameContinue += GameContinue;

        seed = Random.Range(1, 133420);

        GameObject InitialChunk = Instantiate(Chunk, new Vector3(0, 0, 0), new Quaternion(), chunkList.transform);
        InitialChunk.GetComponent<ChunkManager>().OnLoadChunk(InitialChunk.transform.position);
        endScreenScoreDisplay = endScreen.GetComponent<EndScreenScoreDisplay>();
        
        initialLocation = playerMovement.transform.position;
    }

    private void GameContinue()
    {
        Time.timeScale = 1;
        resetButton.interactable = true;
        if (playerMovement)
        {
            playerMovement.enabled = true;
        }
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt("SeenTutorial", 1) == 1)
        {
            Time.timeScale = 0;
            playerMovement.enabled = false;
            resetButton.interactable = false;
            tutorialMenu.OnStartTutorial();
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        if (level == 0)
        {
            Application.targetFrameRate = 144;
        }
        else
        {
            Application.targetFrameRate = 60;
        }
    }

    private void Update()
    {
        TryResetGame();
        UpdateScore();
    }

    private void TryResetGame()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetGame();
        }
    }

    private void UpdateScore()
    {
        if (isGameFail) return;
        
        Vector3 currPos = playerMovement.transform.position;
        float currDistance = (currPos - initialLocation).magnitude;
        distanceFromCenter = (int)currDistance;
        
        if (distanceFromCenter > maxDistanceFromCenter)
        {
            maxDistanceFromCenter = distanceFromCenter;
        }
    }
    
    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void FailGame()
    {
        int highestScore = math.max(PlayerPrefs.GetInt("highScore", 0), maxDistanceFromCenter);
        PlayerPrefs.SetInt("highScore", highestScore);
        endScreenScoreDisplay.DisplayScores(maxDistanceFromCenter, highestScore);
        
        isGameFail = true;
        endScreen.SetActive(true);
        playerMovement.enabled = false;
        
        foreach (GameObject gameObject in hideableObjects)
        {
            gameObject.SetActive(false);
        }
    }
}