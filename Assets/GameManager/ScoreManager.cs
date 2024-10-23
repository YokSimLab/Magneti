using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    private TextMeshProUGUI scoreText;
    
    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        int distance = gameManager.distanceFromCenter;
        scoreText.SetText(distance + "M");
    }
}
