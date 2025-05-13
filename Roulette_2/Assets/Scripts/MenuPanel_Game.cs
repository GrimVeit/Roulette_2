using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPanel_Game : MovePanel
{
    [SerializeField] private Button buttonMenu;

    public override void Initialize()
    {
        base.Initialize();

        buttonMenu.onClick.AddListener(() => OnClickToMenu?.Invoke());
    }

    public override void Dispose()
    {
        base.Dispose();

        buttonMenu.onClick.RemoveListener(() => OnClickToMenu?.Invoke());
    }

    #region Input

    public event Action OnClickToMenu;

    #endregion
}
