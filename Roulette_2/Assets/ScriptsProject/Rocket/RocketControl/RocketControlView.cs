using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class RocketControlView : View
{
    [SerializeField] private Button buttonLeft;
    [SerializeField] private Button buttonRight;

    public void Initialize()
    {
        buttonLeft.onClick.AddListener(HandleClickToLeft);
        buttonRight.onClick.AddListener(HandleClickToRight);
    }

    public void Dispose()
    {
        buttonLeft.onClick.RemoveListener(HandleClickToLeft);
        buttonRight.onClick.RemoveListener(HandleClickToRight);
    }

    #region Input

    public event Action OnClickToLeft;
    public event Action OnClickToRight;

    private void HandleClickToLeft()
    {
        OnClickToLeft?.Invoke();
    }

    private void HandleClickToRight()
    {
        OnClickToRight?.Invoke();
    }

    #endregion
}
