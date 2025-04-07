using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipBotMovePresenter
{
    private readonly ChipBotMoveModel model;

    public ChipBotMovePresenter(ChipBotMoveModel model)
    {
        this.model = model;
    }

    public void Initialize()
    {

    }

    public void Dispose()
    {

    }

    #region Input

    public event Action OnDoMotion
    {
        add => model.OnDoMotion += value;
        remove => model.OnDoMotion -= value;
    }

    public event Action OnStoppedCurrentChip
    {
        add => model.OnStoppedCurrentChip += value;
        remove => model.OnStoppedCurrentChip -= value;
    }

    public event Action OnDestroyedCurrentChip
    {
        add => model.OnDestroyedCurrentChip += value;
        remove => model.OnDestroyedCurrentChip -= value;
    }

    public void ActivateMove()
    {
        model.ActivateMove();
    }

    #endregion
}
