using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinMotionPresenter
{
    private readonly SpinMotionModel model;
    private readonly SpinMotionView view;

    public SpinMotionPresenter(SpinMotionModel model, SpinMotionView view)
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
        view.OnEndSpin += model.EndSpin;

        model.OnStartSpin += view.StartSpin;
    }

    private void DeactivateEvents()
    {
        view.OnEndSpin -= model.EndSpin;

        model.OnStartSpin -= view.StartSpin;
    }

    #region Input

    public event Action OnBotMotion
    {
        add => model.OnBotMotion += value;
        remove => model.OnBotMotion -= value;
    }

    public event Action OnPlayerMotion
    {
        add => model.OnPlayerMotion += value;
        remove => model.OnPlayerMotion -= value;
    }

    public void ActivateSpin()
    {
        model.StartSpin();
    }

    #endregion
}

