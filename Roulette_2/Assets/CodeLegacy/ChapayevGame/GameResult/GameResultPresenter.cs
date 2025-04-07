using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResultPresenter
{
    private readonly GameResultModel model;
    private readonly GameResultView view;

    public GameResultPresenter(GameResultModel model, GameResultView view)
    {
        this.model = model;
        this.view = view;
    }

    public void Initialize()
    {
        ActivateEvents();
    }

    public void Dispose()
    {
        DeactivateEvents();
    }

    private void ActivateEvents()
    {
        model.OnWin_Value += view.SetWin;
    }

    private void DeactivateEvents()
    {
        model.OnWin_Value -= view.SetWin;
    }

    #region Input

    public event Action OnWin
    {
        add => model.OnWin += value;
        remove => model.OnWin -= value;
    }

    public event Action OnLose
    {
        add => model.OnLose += value;
        remove => model.OnLose -= value;
    }

    public void AddPlayerChip(ChipMove chip)
    {
        model.AddPlayerChip();
    }

    public void AddBotChip(ChipMove chip)
    {
        model.AddBotChip();
    }

    public void RemovePlayerChip(ChipMove chip)
    {
        model.RemovePlayerChip();
    }

    public void RemoveBotChip(ChipMove chip)
    {
        model.RemoveBotChip();
    }

    public bool IsPlayerWin()
    {
        return model.IsPlayerWin();
    }

    #endregion
}