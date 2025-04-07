using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMovePresenter : IRocketMoveProvider
{
    private readonly RocketMoveModel _model;
    private readonly RocketMoveView _view;

    public RocketMovePresenter(RocketMoveModel model, RocketMoveView view)
    {
        _model = model;
        _view = view;
    }

    public void Initialize()
    {
        ActivateEvents();

        _view.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        _view.Dispose();
    }

    private void ActivateEvents()
    {
        _model.OnSetCourseRoute += _view.SetCourseNumber;
        _model.OnMoveToRight += _view.MoveRight;
        _model.OnMoveToLeft += _view.MoveLeft;
        _model.OnMoveToWinLeft += _view.MoveToWinLeft;
        _model.OnMoveToWinRight += _view.MoveToWinRight;

        _model.OnMoveToBase += _view.MoveToBase;
        _model.OnMoveToStart += _view.MoveToStart;
    }

    private void DeactivateEvents()
    {
        _model.OnSetCourseRoute -= _view.SetCourseNumber;
        _model.OnMoveToRight -= _view.MoveRight;
        _model.OnMoveToLeft -= _view.MoveLeft;
        _model.OnMoveToWinLeft -= _view.MoveToWinLeft;
        _model.OnMoveToWinRight -= _view.MoveToWinRight;

        _model.OnMoveToBase -= _view.MoveToBase;
        _model.OnMoveToStart -= _view.MoveToStart;
    }

    #region Input

    public void MoveLeft()
    {
        _model.MoveLeft();
    }

    public void MoveRight()
    {
        _model.MoveRight();
    }

    public void MoveLeftDouble()
    {
        _model.MoveLeftDouble();
    }

    public void MoveRightDouble()
    {
        _model.MoveRightDouble();
    }

    public void MoveToBase()
    {
        _model.MoveToBase();
    }

    public void MoveToStart()
    {
        _model.MoveToStart();
    }

    public void Restart()
    {
        _model.Restart();
    }
    #endregion

    #region Output

    public event Action<int> OnMoveToLeft
    {
        add => _model.OnMoveToLeft += value;
        remove => _model.OnMoveToLeft -= value;
    }

    public event Action<int> OnMoveToRight
    {
        add => _model.OnMoveToRight += value;
        remove => _model.OnMoveToRight -= value;
    }



    public event Action OnMoveToWinLeft
    {
        add => _model.OnMoveToWinLeft += value;
        remove => _model.OnMoveToWinLeft -= value;
    }

    public event Action OnMoveToWinRight
    {
        add => _model.OnMoveToWinRight += value;
        remove => _model.OnMoveToWinRight -= value;
    }




    public event Action OnPauseMoveToBase
    {
        add => _view.OnPauseMoveToBase += value;
        remove => _view.OnPauseMoveToBase -= value;
    }

    public event Action OnEndMoveToBase
    {
        add => _view.OnEndMoveToBase += value;
        remove => _view.OnEndMoveToBase -= value;
    }

    public event Action OnEndMoveToStart
    {
        add => _view.OnEndMoveToStart += value;
        remove => _view.OnEndMoveToStart -= value;
    }

    #endregion
}
