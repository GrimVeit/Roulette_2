using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BetView : View
{
    [SerializeField] private TextMeshProUGUI textWin;

    public void SetWin(int win)
    {
        textWin.text = win.ToString();
    }
}
