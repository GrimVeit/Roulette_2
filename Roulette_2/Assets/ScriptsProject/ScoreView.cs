using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreView : View
{
    [SerializeField] private TextMeshProUGUI textCurrentWinScore;
    [SerializeField] private TextMeshProUGUI textLastWinScore;

    public void SetCurrentWinScore(float score)
    {
        textCurrentWinScore.text = score.ToString();
    }

    public void SetLastWinScore(float score)
    {
        textLastWinScore.text = score.ToString();
    }
}
