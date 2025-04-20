using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RouletteStatistic : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    public void SetData(Color color, int number)
    {
        text.color = color;
        text.text = number.ToString();
    }
}
