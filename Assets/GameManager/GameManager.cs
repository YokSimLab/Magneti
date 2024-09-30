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

    [SerializeField] GameObject winText;
    [SerializeField] GameObject failText;

    [Range(1, 133420)]
    [SerializeField] public int seed = 0;

    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] GasManager gasManager;
    [SerializeField] GameObject Chunk;
    public GameObject chunkList;

    [SerializeField] Button resetButton;
    [SerializeField] TutorialMenu tutorialMenu;

    [HideInInspector] public bool isGameFail = false;

    public delegate void GameContinueDelegate();

    public static GameContinueDelegate onGameContinue;

    private void Awake()
    {
        _instance = this;

        onGameContinue += GameContinue;

        seed = Random.Range(1, 133420);

        GameObject InitialChunk = Instantiate(Chunk, new Vector3(0, 0, 0), new Quaternion(), chunkList.transform);
        InitialChunk.GetComponent<ChunkManager>().OnLoadChunk(InitialChunk.transform.position);
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

    public void FailGame()
    {
        //isGameFail = true;
        //failText.SetActive(true);
        //playerMovement.enabled = false;
    }
}