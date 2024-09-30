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

    [SerializeField] GameObject winText;
    [SerializeField] GameObject failText;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] GasManager gasManager;
    [SerializeField] GameObject Chunk;

    [HideInInspector] public bool isGameFail = false;

    private void Awake()
    {
        _instance = this;

        GameObject InitialChunk = Instantiate(Chunk, new Vector3(0, 0, 0), new Quaternion());
        InitialChunk.GetComponent<ChunkManager>().OnLoadChunk(InitialChunk.transform.position);
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
        isGameFail = true;
        failText.SetActive(true);
        playerMovement.enabled = false;
    }
}