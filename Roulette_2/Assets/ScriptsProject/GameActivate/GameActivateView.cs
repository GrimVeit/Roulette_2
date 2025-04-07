using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameActivateView : View
{
    [SerializeField] private Button buttonPlay;
    [SerializeField] private Image imageButton;
    [SerializeField] private Sprite spriteDeactivate, spriteActivate;

    public void Initialize()
    {
        buttonPlay.onClick.AddListener(() => OnPlayGame?.Invoke());
    }

    public void Dispose()
    {
        buttonPlay.onClick.RemoveListener(() => OnPlayGame?.Invoke());
    }

    #region Input

    public void Activate()
    {
        buttonPlay.enabled = true;
        imageButton.sprite = spriteActivate;
    }

    public void Deactivate()
    {
        buttonPlay.enabled = false;
        imageButton.sprite = spriteDeactivate;
    }

    #endregion

    #region Output

    public event Action OnPlayGame;

    #endregion
}
