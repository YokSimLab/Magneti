using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


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

    [HideInInspector] public bool isGameFail = false;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
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
        if (!failText.activeSelf)
        {
            winText.SetActive(true);
        }
    }

    public void FailGame()
    {
        isGameFail = true;
        if (failText)
        {
            failText.SetActive(true);
            playerMovement.enabled = false;
        }
    }
}