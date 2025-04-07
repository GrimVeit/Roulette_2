using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FooterPanel_Game : MovePanel
{
    [SerializeField] private Button buttonBet;

    public override void Initialize()
    {
        base.Initialize();

        buttonBet.onClick.AddListener(() => OnClickToOpenBet?.Invoke());
    }

    public override void Dispose()
    {
        base.Dispose();

        buttonBet.onClick.RemoveListener(() => OnClickToOpenBet?.Invoke());
    }

    #region Output

    public event Action OnClickToOpenBet;

    #endregion
}
