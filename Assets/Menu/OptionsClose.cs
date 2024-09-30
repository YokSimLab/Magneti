using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsClose : MonoBehaviour
{
    [SerializeField] CanvasGroup MainMenuCanvasGroup;

    public void OnClose()
    {
        MainMenuCanvasGroup.interactable = true;
        gameObject.SetActive(false);
    }
}
