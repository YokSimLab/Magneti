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
    [SerializeField] TMP_InputField seedInput;

    int seed = 0;

    private void Awake()
    {
        playButton.onClick.AddListener(OnPlayButtonClicked);
        seedInput.onValueChanged.AddListener(OnSeedInputChanged);

        seedInput.interactable = true;
        seedInput.characterValidation = TMP_InputField.CharacterValidation.Integer;
    }

    private void OnPlayButtonClicked()
    {
        if (seedInput.text == "")
        {
            SeedManager.Instance.isSeedOn = false;
        }
        else
        {
            SeedManager.Instance.isSeedOn = true;
            SeedManager.Instance.seed = int.Parse(seedInput.text);
        }

        //Yes, unity sucks, and this is the way to do it.
        SceneManager.LoadScene("EndlessScene");
    }

    private void OnSeedInputChanged(string newSeed)
    {
        if (newSeed == "" || newSeed == "-" || newSeed == "0")
        {
            seedInput.text = "";
            return;
        }

        int newSeedInt = int.Parse(newSeed);
        if (newSeedInt >= 1 && newSeedInt <= 133420)
        {
            seed = newSeedInt;
        }
        else
        {
            seedInput.text = seed.ToString();
        }
    }
}
