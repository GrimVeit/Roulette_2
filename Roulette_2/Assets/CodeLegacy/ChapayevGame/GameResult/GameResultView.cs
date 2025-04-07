using TMPro;
using UnityEngine;

public class GameResultView : View
{
    [SerializeField] private TextMeshProUGUI textWin;

    public void SetWin(int win)
    {
        textWin.text = win.ToString();
    }
}
