using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketControlPresenter
{
    private readonly RocketControlModel model;
    private readonly RocketControlView view;

    public RocketControlPresenter(RocketControlModel model, RocketControlView view)
    {
        this.model = model;
        this.view = view;
    }

    public void Initialize()
    {
        ActivateEvents();

        view.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        view.Dispose();
    }

    private void ActivateEvents()
    {
        view.OnClickToLeft += model.MoveLeft;
        view.OnClickToRight += model.MoveRight;
    }

    private void DeactivateEvents()
    {
        view.OnClickToLeft -= model.MoveLeft;
        view.OnClickToRight -= model.MoveRight;
    }

    #region Input

    public event Action OnMoveLeft
    {
        add => model.OnMoveToLeft += value;
        remove => model.OnMoveToLeft -= value;
    }

    public event Action OnMoveRight
    {
        add => model.OnMoveToRight += value;
        remove => model.OnMoveToRight -= value;
    }

    #endregion
}
