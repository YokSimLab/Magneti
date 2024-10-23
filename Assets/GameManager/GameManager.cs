using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
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

    [Range(1, 133420)] [SerializeField] public int seed = 0;

    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] GasManager gasManager;
    [SerializeField] private PathTracker pathTracker;
    [SerializeField] private CinemachineVirtualCamera cineMachineCamera;
    public GameObject chunk;
    public GameObject chunkList;

    [SerializeField] TutorialMenu tutorialMenu;
    [SerializeField] GameObject[] hideableObjects;
    [HideInInspector] public bool isGameFail = false;
    [HideInInspector] public int distanceFromCenter = 0;
    [HideInInspector] private int maxDistanceFromCenter = 0;
    [HideInInspector] private Vector3 initialLocation;

    public delegate void GameContinueDelegate();

    public static GameContinueDelegate onGameContinue;

    public Dictionary<int, int> chunkUniqueValueToRandomState = new();

    private void Awake()
    {
        _instance = this;
        cineMachineCamera.m_Lens.OrthographicSize = 7.5f;

        onGameContinue += GameContinue;

        seed = Random.Range(1, 133420);

        GameObject InitialChunk = Instantiate(chunk, new Vector3(0, 0, 0), new Quaternion(), chunkList.transform);
        InitialChunk.GetComponent<ChunkManager>().OnLoadChunk(InitialChunk.transform.position);
        endScreenScoreDisplay = endScreen.GetComponent<EndScreenScoreDisplay>();

        initialLocation = playerMovement.transform.position;
    }

    private void GameContinue()
    {
        Time.timeScale = 1;
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
        UpdateScore();
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

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void FailGame()
    {
        if (isGameFail) return;
        
        if (pathTracker)
        {
            pathTracker.ShowDeathReplay();
        }

        isGameFail = true;
        int highestScore = Mathf.Max(PlayerPrefs.GetInt("highScore", 0), maxDistanceFromCenter);
        PlayerPrefs.SetInt("highScore", highestScore);
        endScreenScoreDisplay.DisplayScores(maxDistanceFromCenter, highestScore);

        endScreen.SetActive(true);
        playerMovement.enabled = false;

        foreach (GameObject gameObject in hideableObjects)
        {
            gameObject.SetActive(false);
        }
    }
}