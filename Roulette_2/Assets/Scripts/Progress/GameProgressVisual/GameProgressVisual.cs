using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class GameProgressVisual
{
    public int Id => id;

    [SerializeField] private int id;
    [SerializeField] private Button buttonGame;

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI textGame;
    [SerializeField] private string textOpen;
    [SerializeField] private string textClose;

    [Header("Image")]
    [SerializeField] private Image imageGame;
    [SerializeField] private Color colorOpen;
    [SerializeField] private Color colorClose;

    public void Open()
    {
        buttonGame.enabled = true;

        textGame.text = textOpen;
        imageGame.color = colorOpen;
    }

    public void Close()
    {
        buttonGame.enabled = false;

        textGame.text = textClose;
        imageGame.color = colorClose;
    }
}
