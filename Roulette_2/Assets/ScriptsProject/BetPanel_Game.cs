using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BetPanel_Game : MovePanel
{
    [SerializeField] private List<Button> buttonsExit = new List<Button>();

    public override void Initialize()
    {
        base.Initialize();

        buttonsExit.ForEach((button) => button.onClick.AddListener(() => OnClickToExit?.Invoke()));
    }

    public override void Dispose()
    {
        base.Dispose();

        buttonsExit.ForEach((button) => button.onClick.RemoveListener(() => OnClickToExit?.Invoke()));
    }

    #region Output

    public event Action OnClickToExit;

    #endregion
}
