using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialMenu : MonoBehaviour
{
    [SerializeField] private GameObject allPages;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private GameObject endButton;

    private List<GameObject> childPages = new List<GameObject>();
    private int currentPage;

    private void Awake()
    {
        foreach (Transform child in allPages.transform)
        {
            childPages.Add(child.gameObject);
            child.gameObject.SetActive(false);
            endButton.SetActive(false);
        }
    }

    public void OnStartTutorial()
    {
        PlayerPrefs.SetInt("SeenTutorial", 0);
        currentPage = 0;

        gameObject.SetActive(true);
        continueButton.SetActive(true);
        childPages[currentPage].SetActive(true);
    }

    public void OnNextButton()
    {
        childPages[currentPage].SetActive(false);
        currentPage++;
        childPages[currentPage].SetActive(true);

        if (currentPage + 1 >= childPages.Count)
        {
            continueButton.SetActive(false);
            endButton.SetActive(true);
        }
    }

    public void OnEndButton()
    {
        gameObject.SetActive(false);
        childPages[currentPage].SetActive(false);
        endButton.SetActive(false);
        GameManager.onGameContinue?.Invoke();
    }
}