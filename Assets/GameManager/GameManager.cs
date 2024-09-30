using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    [SerializeField] GameObject gasCan1;
    [SerializeField] GameObject gasCan2;
    [SerializeField] GameObject gasCan3;
    [SerializeField] GameObject winText;
    [SerializeField] GameObject failText;

    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] GasManager gasManager;

    [SerializeField] Button resetButton;
    [SerializeField] TutorialMenu tutorialMenu;

    [HideInInspector] public bool isGameFail = false;
    [HideInInspector] public bool isGameWin = false;

    public delegate void GameContinueDelegate();

    public static GameContinueDelegate onGameContinue;

    private void Awake()
    {
        onGameContinue += GameContinue;

        _instance = this;
    }

    private void GameContinue()
    {
        Time.timeScale = 1;
        resetButton.interactable = true;
        playerMovement.enabled = true;
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

        if (!(gasCan1 || gasCan2 || gasCan3))
        {
            WonGame();
        }
    }

    private void TryResetGame()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetGame();
        }
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void WonGame()
    {
        if (!isGameFail)
        {
            isGameWin = true;
            winText.SetActive(true);
            gasManager.CurrentGas = 100;
            gasManager.gasRemoveRate = 0;
        }
    }

    public void FailGame()
    {
        if (!isGameWin)
        {
            isGameFail = true;
            failText.SetActive(true);
            playerMovement.enabled = false;
        }
    }
}