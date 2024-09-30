using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] CanvasGroup MainMenuCanvasGroup;
    [SerializeField] GameObject OptionsMenuUI;
    [SerializeField] Animator OptionsMenuAnimator;

    [SerializeField] Button OpenOptionsButton;
    [SerializeField] Button CloseButton;

    private void Awake()
    {
        OpenOptionsButton.onClick.AddListener(OnOpenOptions);
        CloseButton.onClick.AddListener(OnCloseOptions);
    }

    private void OnOpenOptions()
    {
        OptionsMenuUI.SetActive(true);
        MainMenuCanvasGroup.interactable = false;
    }

    private void OnCloseOptions()
    {
        OptionsMenuAnimator.Play("OptionsMenuClose");
    }
}
