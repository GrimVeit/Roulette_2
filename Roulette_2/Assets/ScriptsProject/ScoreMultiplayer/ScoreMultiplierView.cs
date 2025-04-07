using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreMultiplierView : View
{
    [SerializeField] private TextMeshProUGUI scoreMultiplier;

    public void SetScoreMultiplier(float multipliyer)
    {
        scoreMultiplier.text = $"{multipliyer}";
    }
}
