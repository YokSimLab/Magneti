using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndScreenScoreDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI highScoreText;
    [SerializeField] TextMeshProUGUI highestScoreText;

    public void DisplayScores(int highScore, int highestScore)
    {
        highScoreText.SetText(highScore + "m");
        highestScoreText.SetText(highestScore + "m");
    }
}
