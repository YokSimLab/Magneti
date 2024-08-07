using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject gasCan1;
    [SerializeField] GameObject gasCan2;
    [SerializeField] GameObject gasCan3;
    [SerializeField] GameObject winText;
    [SerializeField] GameObject failText;
    [SerializeField] PlayerMovement playerMovement;


    private void Update()
    {
        ResetGame();

        if (!(gasCan1 || gasCan2 || gasCan3))
        {
            WonGame();
        }
    }

    private static void ResetGame()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
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
        if (failText)
        {
            failText.SetActive(true);
            playerMovement.enabled = false;
        }
    }
}