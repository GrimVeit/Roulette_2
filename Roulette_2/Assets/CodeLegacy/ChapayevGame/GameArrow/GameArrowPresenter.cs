using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameArrowPresenter
{
    private readonly GameArrowModel model;
    private readonly GameArrowView view;

    public GameArrowPresenter(GameArrowModel model, GameArrowView view)
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
        model.OnRotateArrow += view.RotateArrow;
    }

    private void DeactivateEvents()
    {
        model.OnRotateArrow -= view.RotateArrow;
    }

    #region Input

    public void RotateDown()
    {
        model.RotateDown();
    }

    public void RotateUp()
    {
        model.RotateUp();
    }

    #endregion
}
