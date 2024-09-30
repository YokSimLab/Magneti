using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Button playButton;

    private void Awake()
    {
        Application.targetFrameRate = 144;
        playButton.onClick.AddListener(OnPlayButtonClicked);
    }

    private void OnPlayButtonClicked()
    {
        //Yes, unity sucks, and this is the way to do it.
        SceneManager.LoadScene("EndlessScene");
    }
}
