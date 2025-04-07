using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardPanel_Menu : MovePanel
{
    [SerializeField] private Button buttonExit;

    public override void Initialize()
    {
        base.Initialize();

        buttonExit.onClick.AddListener(HandleClickToCancel);
    }

    public override void Dispose()
    {
        base.Dispose();

        buttonExit.onClick.RemoveListener(HandleClickToCancel);
    }

    #region Input

    public event Action OnClickToCancelFromLeaderboard;

    private void HandleClickToCancel()
    {
        OnClickToCancelFromLeaderboard?.Invoke();
    }

    #endregion
}
